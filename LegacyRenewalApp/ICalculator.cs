namespace LegacyRenewalApp;

public interface ICalculator
{
    public Record_ResultCalculator Calculate(
        Customer customer,
        SubscriptionPlan plan,
        int seatCount,
        string paymentMethod,
        bool useLoyaltyPoints,
        bool includePremiumSupport
        );
}