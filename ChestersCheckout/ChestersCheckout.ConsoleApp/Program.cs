using ChestersCheckout.Core.Services;

Console.WriteLine("Please enter products, seperated by commas");

var userProducts = Console.ReadLine() ?? "";

var productRepository = new StaticProductRepositoryService();
var basketBuilder = new BasketBuilderService(productRepository);
var basket = basketBuilder.BuildBasket(userProducts.Split(',', StringSplitOptions.RemoveEmptyEntries));
var totalCost = new BasketCostCalculatorService(productRepository).CalculateTotalCost(basket);

Console.WriteLine($"Total cost: {(decimal)totalCost / 100:C}");

Console.ReadLine();