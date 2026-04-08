namespace LegacyRenewalApp;

public interface IPaymenTypetFee
{
    public (decimal paymentFee, string note) GetPaymentFee(string paymentMethod, decimal subtotalAfterDiscount_supportFee);
}