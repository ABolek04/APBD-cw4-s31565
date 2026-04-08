namespace LegacyRenewalApp;

public interface ITaxRate
{
    public (decimal taxAmount,decimal totalTax,string note) GetTotalTax(Customer customer, decimal taxBase);
}