namespace LegacyRenewalApp;

public interface ISupportFee
{
    public (decimal feeAmount, string note) GetSupportFee(string planCode, bool includePremiumSupport);
}