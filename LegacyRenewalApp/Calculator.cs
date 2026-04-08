namespace LegacyRenewalApp;

public class Calculator : ICalculator
{
    private readonly IDiscount _discountCalculator;
    private readonly ISupportFee _supportFee;
    private readonly IPaymenTypetFee _paymenTypetFee;
    private readonly ITaxRate _taxRate;

    public Calculator()
    {
        _discountCalculator = new DiscountCalculator();
        _supportFee = new SupportFee();
        _paymenTypetFee = new PaymentTypeFee();
        _taxRate = new TotalTax();
    }

    public Record_ResultCalculator Calculate(
        Customer customer,
        SubscriptionPlan plan,
        int seatCount,
        string paymentMethod,
        bool useLoyaltyPoints,
        bool includePremiumSupport
    )
    {
        string notes = string.Empty;
        
        decimal baseAmount = (plan.MonthlyPricePerSeat * seatCount * 12m) + plan.SetupFee;
        
        var discountResult =
            _discountCalculator.GetDiscountAmount(customer, baseAmount, plan, seatCount, useLoyaltyPoints);
        
        decimal subtotalAfterDiscount = baseAmount - discountResult.DiscountAmount;
        notes += discountResult.Note;

        if (subtotalAfterDiscount < 300m)
        {
            subtotalAfterDiscount = 300m;
            notes += "minimum discounted subtotal applied; ";
        }

        var supportFeeResult = _supportFee.GetSupportFee(plan.Code, includePremiumSupport);
        decimal supportFee = supportFeeResult.feeAmount;
        notes += supportFeeResult.note;

        decimal subtotalAfterDiscount_supportFee = subtotalAfterDiscount + supportFee;
        var paymentFeeResult = _paymenTypetFee.GetPaymentFee(paymentMethod, subtotalAfterDiscount_supportFee);
        decimal paymentFee = paymentFeeResult.paymentFee;
        notes += paymentFeeResult.note;

        decimal taxBase = subtotalAfterDiscount + supportFee + paymentFee;
        var taxResult = _taxRate.GetTotalTax(customer, taxBase);
        decimal taxAmount = taxResult.taxAmount;
        decimal finalAmount = taxResult.totalTax;
        notes += taxResult.note;

        return new Record_ResultCalculator(
            BaseAmount: baseAmount,
            DiscountAmount: discountResult.DiscountAmount,
            SupportFee: supportFee,
            PaymentFee: paymentFee,
            TaxAmount: taxAmount,
            FinalAmount: finalAmount,
            Notes: notes
        );
    }
}