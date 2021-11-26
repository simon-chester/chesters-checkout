namespace ChestersCheckout.Core.Services.Abstractions
{
    public interface IProductRepositoryService
    {
        int? GetPrice(string productName);
        bool IsValidProduct(string productName);
    }
}