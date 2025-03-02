namespace P2FixAnAppDotNetCode9.Models.Repositories
{
    public interface IProductRepository
    {
        Product[] GetAllProducts();

        void UpdateProductStocks(int productId, int quantityToRemove);
    }
}
