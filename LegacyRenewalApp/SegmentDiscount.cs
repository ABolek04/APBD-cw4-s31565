namespace LegacyRenewalApp;

public class SegmentDiscount : IDiscount
{
    public (decimal DiscountAmount, string Note) GetDiscountAmount(Customer customer, decimal baseAmount,
        SubscriptionPlan plan, int seatCount, bool usePoints)
    {
        return customer.Segment switch
        {
            "Standard" => (0.0m, "standard plan; "),
            "Silver" => (baseAmount * 0.05m,"silver discount; "),
            "Gold" => (baseAmount * 0.10m,"gold discount; "),
            "Platinum" => (baseAmount * 0.15m,"platinum discount; "),
            "Education" when plan.IsEducationEligible => (baseAmount * 0.20m,"education discount; "),
            _ => (0.0m,string.Empty)
        };
    }
}