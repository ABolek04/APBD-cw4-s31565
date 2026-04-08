using System;

namespace LegacyRenewalApp;

public class PaymentTypeFee : IPaymenTypetFee
{
    public (decimal paymentFee, string note) GetPaymentFee(string paymentMethod, decimal subtotalAfterDiscount_supportFee)
    {
        return paymentMethod switch
        {
            "CARD" => (subtotalAfterDiscount_supportFee * 0.02m, "card payment fee; "),
            "BANK_TRANSFER" => (subtotalAfterDiscount_supportFee * 0.01m, "bank transfer fee; "),
            "PAYPAL" => (subtotalAfterDiscount_supportFee * 0.035m, "paypal fee; "),
            "INVOICE" => (0m, "invoice payment; "),
            _ => throw new ArgumentException("Unsupported payment method")
        };
    }
}