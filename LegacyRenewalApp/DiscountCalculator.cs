using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace LegacyRenewalApp;

public class DiscountCalculator : IDiscount
{
    private readonly IEnumerable<IDiscount> _discounts;

    public DiscountCalculator()
    {
        _discounts = new List<IDiscount>
        {
            new SegmentDiscount(),
            new LoyaltyDiscount(),
            new SeatCountDiscount(),
            new LoyaltyPointsDiscount(),
        };
    }

    public (decimal DiscountAmount, string Note) GetDiscountAmount(Customer customer, decimal baseAmount,
        SubscriptionPlan plan,
        int seatCount, bool usePoints)
    {
        decimal totalDiscount = 0.0m;
        string notes = string.Empty;
        foreach (var discount in _discounts)
        {
            var result = discount.GetDiscountAmount(customer, baseAmount, plan, seatCount, usePoints);
            if (result.DiscountAmount > 0)
            {
                totalDiscount += result.DiscountAmount;
                notes += result.Note;
            }
        }
        return (totalDiscount, notes);
    }
}