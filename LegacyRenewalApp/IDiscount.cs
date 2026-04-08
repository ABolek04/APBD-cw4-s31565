namespace LegacyRenewalApp;

public interface IDiscount
{
     public (decimal DiscountAmount,string Note) GetDiscountAmount(Customer customer, decimal baseAmount, SubscriptionPlan plan);
}