using Analisystem.Data;
using Analisystem.Models;

namespace Analisystem.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ProductRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ProductModel addProduct(ProductModel product)
        {
            product.RegisteredDate = DateTime.Now;
            _databaseContext.Products.Add(product);
            _databaseContext.SaveChanges();
            return product;
        }

        public ProductModel getProductById(int id)
        {
            return _databaseContext.Products.FirstOrDefault(x => x.Id == id);
        }

        public ProductModel GetProductByNameOrId(string name, int id)
        {
            return _databaseContext.Products.FirstOrDefault(x => x.Id == id || x.Name == name);
        }

        public List<ProductModel> getProducts()
        {
            return _databaseContext.Products.ToList();
        }

        public bool removeProduct(ProductModel product)
        {
            try
            {
                _databaseContext.Remove(product);
                _databaseContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ProductModel updateProduct(ProductModel product)
        {
            ProductModel productDB = getProductById(product.Id);
            if (productDB == null) throw new Exception("An error occurred while updating the user.");

            productDB.Name = product.Name;
            productDB.Price = product.Price;
            productDB.Provider = product.Provider;
            productDB.Purpose = product.Purpose;
            productDB.ProviderNumber = product.ProviderNumber;
            productDB.Quantity = product.Quantity;
            productDB.LastUpdated = DateTime.Now;

            _databaseContext.Products.Update(productDB);
            _databaseContext.SaveChanges();
            return productDB;
        }
    }
}
