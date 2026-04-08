using System;

namespace LegacyRenewalApp;

public class TotalTax : ITaxRate
{
    public (decimal taxAmount, decimal totalTax, string note) GetTotalTax(Customer customer, decimal taxBase)
    {
        
        decimal taxRate = customer.Country switch
        {
            "Poland" => 0.23m,
            "Germany" => 0.19m,
            "Czech Republic" => 0.21m,
            "Norway" => 0.25m,
            _ => 0.20m
        };
        decimal taxAmount = taxBase * taxRate;
        decimal totalTax = taxBase + taxAmount;
        string note = string.Empty;
        if (totalTax < 500m)
        {
            totalTax = 500m;
            note = "minimum invoice amount applied; ";
        }
        return (taxAmount, totalTax, note);

    }
}