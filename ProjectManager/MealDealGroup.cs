using Measurements;
using PrimativeExtensions;

namespace ProjectManager;

public class MealDealGroup
{
    private List<(Measurement, Resource)> products;
    public string Name { get; }
    public string Description { get; }
    public int NumberToAdd { get; }

    public MealDealGroup(string name, string description, int numberToAdd)
    {
        this.Name = name;
        this.Description = description;
        this.NumberToAdd = numberToAdd;
        products = new List<(Measurement, Resource)>();
    }

    public Validation<MealDealGroup> AddResource(Measurement quantity, Resource resource)
    {
        products.Add((quantity, resource));
        return this;
    }

    public Validation<MealDealGroup> AddResource((Measurement quantity, Resource resource) resource)
    {
        products.Add( resource);
        return this;
    }


    private bool ItemIsInGroup(Measurement quantity, Resource resource)
    {
        return products.Any(x => x.Item2 == resource && x.Item1 == quantity);
    }
    
      
    

    public IList<ResourceCost> GetItemsToUse(
        IEnumerable<(Measurement Quantity, Resource Resource) > itemsToCheck, Func<(Measurement, Resource), ResourceCost> getResourceCost)
    {
        var productsWithPrices = this.products.Select(x => getResourceCost((x.Item1, x.Item2))).ToList();
        var itemsToUse = new List<ResourceCost>();
        var minimumQtyForBucket = productsWithPrices.Where(x => x.Quantity.IsInt()) .ToDictionary(x => x.Resource, x => x.Quantity.GetQty().ToInt());
        var itemsToCheckGrouped = itemsToCheck.GroupByResourceForType(minimumQtyForBucket).ToList();
        
        
        var itemsInGroup = 
            itemsToCheckGrouped
                .Where(x => ItemIsInGroup(x.Quantity , x.Resource ))
                .ToList();
        
        
        
        if (itemsInGroup.Count() < NumberToAdd)
        {
            return itemsToUse;
        }
        var itemsInGroupByCost =  itemsInGroup.Select(x => productsWithPrices.First(y => y.Resource == x.Resource && y.Quantity == x.Quantity)).ToList();

        return
            itemsInGroupByCost
                .OrderByDescending(x => x.Cost )
                .Take(NumberToAdd)
                .ToList();
    }

    public IList<(Measurement Quantity, Resource Resource) > GetItems()
    {
        return products ;
    }
    
    // override object.Equals
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (MealDealGroup) obj;
        return this.Name == other.Name && this.Description == other.Description && this.NumberToAdd == other.NumberToAdd;
    }
    public override int GetHashCode()
    {
        return (Name, Description, NumberToAdd).GetHashCode();
    }
    
    public static bool operator ==(MealDealGroup lhs, MealDealGroup rhs)
    {
        if (ReferenceEquals(lhs, rhs))
        {
            return true;
        }

        if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
        {
            return false;
        }

        return lhs.Equals(rhs);
    }
    
    public static bool operator !=(MealDealGroup lhs, MealDealGroup rhs)
    {
        return !(lhs == rhs);
    }
}