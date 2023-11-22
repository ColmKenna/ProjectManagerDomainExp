using System.Collections.ObjectModel;
using Measurements;
using PrimativeExtensions;

namespace ProjectManager;

public class Cart
{
    public ProjectOwner ProjectOwner { get; private set; }
    private IDealsRepository dealsRepository;
    private readonly IResourceRepository resourceRepository;

    public IList<(Measurement measurement, Resource resource)> Items { get; private set; } =
        new List<(Measurement measurement, Resource resource)>();

    public Cart(ProjectOwner projectOwner, IDealsRepository dealsRepository, IResourceRepository resourceRepository)
    {
        ProjectOwner = projectOwner;
        this.dealsRepository = dealsRepository;
        this.resourceRepository = resourceRepository;
    }


    public static Cart Create(ProjectOwner projectOwner, IDealsRepository dealsRepository,
        IResourceRepository resourceRepository)
    {
        return new Cart(projectOwner, dealsRepository, resourceRepository);
    }

    public Cart AddItem(Measurement measurement, Resource resource)
    {
        Items.Add((measurement, resource));
        return this;
    }

    public Validation<CartTotal> CalculateTotal(DateTime date)
    {
        var cartTotal = new CartTotal();


        var resourceCosts = resourceRepository.GetResourceCosts();
        Validation<decimal> total = 0m;
        foreach (var item in Items)
        {
            var cost = GetCostFor(item.resource, item.measurement, date);
            if (cost.IsFailure)
            {
                return Validation<CartTotal>.Fail(cost.ErrorMessage);
            }

            cartTotal.AddLine(new CartLine()
            {
                Measurement = item.measurement,
                Resource = item.resource,
                Cost = cost.Value,
                UsedForDiscount = false
            });
            total += cost.Value;
        }


        var deals = dealsRepository.GetDealsByResourceProvider(ProjectOwner, date);
        var itemsToCheck = Items.GroupByResource().ToList();
        foreach (var deal in deals)
        {
            var discount = deal.GetDiscount(itemsToCheck);
            cartTotal.AddDiscount(discount);
            foreach (var usedForDiscount in discount.ItemsUsedForDiscount)
            {
                var valueTuple = itemsToCheck.First(x => x.Quantity.HasSameMeasurementTypeAs(usedForDiscount.Measurement) && x.Resource == usedForDiscount.Resource);
                itemsToCheck.Remove(valueTuple);
                valueTuple.Quantity -= usedForDiscount.Measurement;
                if (valueTuple.Quantity.GetQty() > 0)
                {
                    itemsToCheck.Add(valueTuple);
                }
            }
        }
        return Validation<CartTotal>.Success(cartTotal);
    }

    private Validation<decimal> GetCostFor(Resource resource, Measurement measurement, DateTime date)
    {
        var resourceCosts = resourceRepository.GetResourceCosts();
        var resourceCost = resourceCosts.FirstOrDefault(rc => rc.Resource == resource);
        if (resourceCost == null)
        {
            return Validation<decimal>.Fail($"No cost found for resource {resource.Name}");
        }

        return resourceCost.GetCostFor(measurement, date).Value;
    }
}

public class CartTotal
{
    public CartTotal()
    {
        discounts = new List<DiscountResult>();
        items = new List<CartLine>();
    }

    public decimal TotalBeforeDiscounts => Items.Sum(x => x.Cost);
    public decimal TotalDiscounts => Discounts.Sum(x => x.Discount);
    public decimal TotalAfterDiscounts => TotalBeforeDiscounts - TotalDiscounts;

    private IList<DiscountResult> discounts;
    public ReadOnlyCollection<DiscountResult> Discounts => new ReadOnlyCollection<DiscountResult>(discounts);

    //public IList<(Measurement measurement, Resource resource)> Items { get; private set; }
    public IReadOnlyList<CartLine> Items => items.ToList();


    private IList<CartLine> items;

    public void AddDiscount(DiscountResult discountResult)
    {
        discounts.Add(discountResult);
        UpdateItemsToSetWhichOnesAreUsedInDiscounts(discountResult); 
    }
    
    public void AddLine(CartLine cartLine)
    {
        items.Add(cartLine);
    }

    public CartTotal UpdateItemsToSetWhichOnesAreUsedInDiscounts(DiscountResult discountToUse)
    {
        foreach (var item in discountToUse.ItemsUsedForDiscount)
        {
            if (item.Measurement.IsInt())
            {
                var cartLines = Items.Where(x => x.Measurement.IsInt() && x.Resource == item.Resource).OrderByDescending(x => x.Measurement.GetQty().ToInt()).ToList();
                var currentQty = item.Measurement.GetQty().ToInt();
                foreach (var cartLine in cartLines)
                {
                    cartLine.UsedForDiscount = true;

                    var measurementQuantity = cartLine.Measurement.GetQty().ToInt();
                    if (measurementQuantity >= currentQty)
                    {
                        currentQty = 0;
                    }
                    else
                    {
                        currentQty -= measurementQuantity;
                    }

                    if (currentQty == 0)
                    {
                        break;
                    }
                }
                
            }
            else
            {
                var cartLine = Items.FirstOrDefault(x => x.Measurement == item.Measurement && x.Resource == item.Resource);
                if (cartLine != null)
                {
                    cartLine.UsedForDiscount = true;
                }
            }
        }
        return this;
    }
}

public class CartLine
{
    public Measurement Measurement { get; set; }
    public Resource Resource { get; set; }
    public decimal Cost { get; set; }
    public bool UsedForDiscount { get; set; }
}