namespace P2FixAnAppDotNetCode9.Models.Repositories
{
    public interface IProductRepository
    {
        /// <summary>
        /// Retourne le produit par son Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Product? GetProductById(int id);
        
        Product[] GetAllProducts();

        void UpdateProductStocks(int productId, int quantityToRemove);
    }
}
