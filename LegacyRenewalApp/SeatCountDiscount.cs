namespace LegacyRenewalApp;

public class SeatCountDiscount : IDiscount
{
    public (decimal DiscountAmount, string Note) GetDiscountAmount(Customer customer, decimal baseAmount,
        SubscriptionPlan plan, int seatCount)
    {
        if (seatCount >= 50)
        {
            return (baseAmount * 0.12m, "large team discount; ");
        }

        if (seatCount >= 20)
        {
            return (baseAmount * 0.08m, "medium team discount; ");
        }

        if (seatCount >= 10)
        {
            return (baseAmount * 0.04m, "small team discount; ");
        }
        return (0.0m, string.Empty);
    }
}