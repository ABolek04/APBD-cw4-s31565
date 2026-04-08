using System;

namespace LegacyRenewalApp
{
    public class SubscriptionRenewalService
    {
        private readonly IDefaultVerification _defaultVerification;
        private readonly ICalculator _calculator;

        public SubscriptionRenewalService()
        {
            _defaultVerification = new DefaultVerification();
            _calculator = new Calculator();
        }
        
        public RenewalInvoice CreateRenewalInvoice(
            int customerId,
            string planCode,
            int seatCount,
            string paymentMethod,
            bool includePremiumSupport,
            bool useLoyaltyPoints)
        {
            string normalizedPlanCode = planCode.Trim().ToUpperInvariant();
            string normalizedPaymentMethod = paymentMethod.Trim().ToUpperInvariant();

            var customerRepository = new CustomerRepository();
            var planRepository = new SubscriptionPlanRepository();

            var customer = customerRepository.GetById(customerId);
            var plan = planRepository.GetByCode(normalizedPlanCode);
            
            _defaultVerification.Verify(customer,planCode, seatCount, paymentMethod);

            var calculatorResult = _calculator.Calculate(
                customer,
                plan,
                seatCount,
                normalizedPaymentMethod,
                includePremiumSupport,
                useLoyaltyPoints);

            var invoice = new RenewalInvoice
            {
                InvoiceNumber = $"INV-{DateTime.UtcNow:yyyyMMdd}-{customerId}-{normalizedPlanCode}",
                CustomerName = customer.FullName,
                PlanCode = normalizedPlanCode,
                PaymentMethod = normalizedPaymentMethod,
                SeatCount = seatCount,
                BaseAmount = Math.Round(calculatorResult.BaseAmount, 2, MidpointRounding.AwayFromZero),
                DiscountAmount = Math.Round(calculatorResult.DiscountAmount ,2, MidpointRounding.AwayFromZero),
                SupportFee = Math.Round(calculatorResult.SupportFee, 2, MidpointRounding.AwayFromZero),
                PaymentFee = Math.Round(calculatorResult.PaymentFee, 2, MidpointRounding.AwayFromZero),
                TaxAmount = Math.Round(calculatorResult.TaxAmount, 2, MidpointRounding.AwayFromZero),
                FinalAmount = Math.Round(calculatorResult.FinalAmount, 2, MidpointRounding.AwayFromZero),
                Notes = calculatorResult.Notes.Trim(),
                GeneratedAt = DateTime.UtcNow
            };

            LegacyBillingGateway.SaveInvoice(invoice);

            if (!string.IsNullOrWhiteSpace(customer.Email))
            {
                string subject = "Subscription renewal invoice";
                string body =
                    $"Hello {customer.FullName}, your renewal for plan {normalizedPlanCode} " +
                    $"has been prepared. Final amount: {invoice.FinalAmount:F2}.";

                LegacyBillingGateway.SendEmail(customer.Email, subject, body);
            }

            return invoice;
        }
    }
}
