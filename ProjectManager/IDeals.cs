using System.Diagnostics;
using Measurements;

namespace ProjectManager;

public interface IDeals
{
    public DiscountResult GetDiscount(IList<(Measurement measurement, Resource resource) > itemsToCheck, Func<(Measurement, Resource), ResourceCost> getResourceCost);
}