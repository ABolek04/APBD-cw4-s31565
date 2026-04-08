using System;

namespace LegacyRenewalApp;

public class DefaultVerification : IDefaultVerification
{
    public void Verify(Customer customer, string planCode, int seatCount, string paymentMethod)
    {
        if (customer.Id <= 0)
        {
            throw new ArgumentException("Customer id must be positive");
        }

        if (string.IsNullOrWhiteSpace(planCode))
        {
            throw new ArgumentException("Plan code is required");
        }

        if (seatCount <= 0)
        {
            throw new ArgumentException("Seat count must be positive");
        }

        if (string.IsNullOrWhiteSpace(paymentMethod))
        {
            throw new ArgumentException("Payment method is required");
        }

        if (!customer.IsActive)
        {
            throw new InvalidOperationException("Inactive customers cannot renew subscriptions");
        }
    }
}