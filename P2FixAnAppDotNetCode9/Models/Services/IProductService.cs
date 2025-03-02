namespace P2FixAnAppDotNetCode9.Models.Services
{
    public interface IProductService
    {
        Product[] GetAllProducts();
        Product GetProductById(int id);
        void UpdateProductQuantities(Cart cart);
    }
}
