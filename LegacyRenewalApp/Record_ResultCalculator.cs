namespace LegacyRenewalApp;

public record Record_ResultCalculator(
    decimal BaseAmount,
    decimal DiscountAmount,
    decimal SupportFee,
    decimal PaymentFee,
    decimal TaxAmount,
    decimal FinalAmount,
    string Notes
    );