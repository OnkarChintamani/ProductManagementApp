using ProductManagementApp.Data;

namespace ProductManagementApp.Models
{
    public class DataStore
    {
        ApplicationDbContext db;
        public DataStore(ApplicationDbContext db)
        {
            this.db = db;
        }
        public List<Product> GetProducts()
        {
            return db.Products.ToList();
        }
        public Product GetProductById(int id)
        {
            var model = db.Products.Where(x => x.Id == id).SingleOrDefault();
            return model;
        }
        public int AddProduct(Product product)
        {
            int result = 0;
            db.Products.Add(product);
            result = db.SaveChanges();
            return result;
        }

        public int EditProduct(Product product)
        {
            int result = 0;
            var model = db.Products.Where(x => x.Id == product.Id).SingleOrDefault();
            if (model != null)
            {
                model.Id = product.Id;
                model.Name = product.Name;
                model.Price = product.Price;
                model.Description = product.Description;
                result = db.SaveChanges();

            }
            return result;
        }
        public int DeleteProduct(int id)
        {
            int result = 0;
            var model = db.Products.Where(x => x.Id == id).SingleOrDefault();
            if (model != null)
            {
                // remove from dbSet
                db.Products.Remove(model);
                result = db.SaveChanges();
            }
            return result;
        }
    }
}
