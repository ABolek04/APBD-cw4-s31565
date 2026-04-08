namespace LegacyRenewalApp;

public class SupportFee : ISupportFee
{
    public (decimal feeAmount, string note) GetSupportFee(string planCode, bool includePremiumSupport)
    {
        if (!includePremiumSupport)
        {
            return (0m, string.Empty);
        }
        return planCode switch
        {
            "START" => (250m, "premium support included; "),
            "PRO" => (400m, "premium support included; "),
            "ENTERPRISE" => (700m, "premium support included; "),
            _ => (0m, string.Empty)
        };
    }
}