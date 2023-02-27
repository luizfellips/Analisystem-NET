using Analisystem.Models;

namespace Analisystem.Repositories
{
    public interface IProductRepository
    {
        List<ProductModel> getProducts();
        List<ProductModel> getUserProducts(int userId);
        ProductModel addProduct(ProductModel product);
        ProductModel getProductById(int id);
        bool removeProduct(ProductModel product);
        ProductModel updateProduct(ProductModel product);

    }
}
