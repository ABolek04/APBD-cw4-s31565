using System;

namespace LegacyRenewalApp;

public interface IDefaultVerification
{
    public void Verify(Customer customer, string planCode, int seatCount, string paymentMethod);
}