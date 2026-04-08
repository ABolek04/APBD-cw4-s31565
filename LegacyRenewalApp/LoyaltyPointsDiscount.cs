namespace LegacyRenewalApp;

public class LoyaltyPointsDiscount : IDiscount
{
    public (decimal DiscountAmount, string Note) GetDiscountAmount(Customer customer, decimal baseAmount, SubscriptionPlan plan,
        int seatCount, bool usePoints)
    {
        if (usePoints && customer.LoyaltyPoints > 0)
        {
            int pointsToUse = customer.LoyaltyPoints > 200 ? 200 : customer.LoyaltyPoints;
            return (pointsToUse, $"loyalty points used: {pointsToUse}; ");
        }
        return (0.0m, string.Empty);
    }
}