namespace LegacyRenewalApp;

public class LoyaltyDiscount : IDiscount
{
    public (decimal DiscountAmount, string Note) GetDiscountAmount(Customer customer, decimal baseAmount,
        SubscriptionPlan plan, int seatCount)
    {
        if (customer.YearsWithCompany >= 5)
        {
            return (baseAmount * 0.07m, "long-term loyalty discount; ");
        }
        if (customer.YearsWithCompany >= 2)
        {
            return (baseAmount * 0.03m, "basic loyalty discount; ");
        }
        return (0.0m, string.Empty);
    }
}