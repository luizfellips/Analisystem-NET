using Analisystem.Models;

namespace Analisystem.Repositories
{
    public interface IProductRepository
    {
        List<ProductModel> getProducts();
        ProductModel addProduct(ProductModel product);
        ProductModel getProductById(int id);
        ProductModel GetProductByNameOrId(string name, int id);
        bool removeProduct(ProductModel product);
        ProductModel updateProduct(ProductModel product);

    }
}
