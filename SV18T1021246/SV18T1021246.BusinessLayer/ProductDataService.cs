using SV18T1021246.DataLayer;
using SV18T1021246.DomainModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021246.BusinessLayer
{
    public static class ProductDataService
    {
        private static readonly IProductDAL productDB;
        private static readonly IProductAttributeDAL productAttributeDB;

        static ProductDataService()
        {
            string provider = ConfigurationManager.ConnectionStrings["DB"].ProviderName;
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            switch (provider)
            {
                case "SQLServer":
                    productDB = new DataLayer.SQLServer.ProductDAL(connectionString);
                    productAttributeDB = new DataLayer.SQLServer.ProductAttributeDAL(connectionString);
                    break;
                default:
                    break;
            }
        }

        public static List<Product> ListOfProducts(int page, int pageSize, string searchValue, out int rowCount, int categoryID, int supplierID)
        {
            if (pageSize < 0)
                pageSize = 0;
            rowCount = productDB.Count(searchValue);
            return productDB.List(page, pageSize, searchValue, categoryID, supplierID).ToList();
        }


        public static Product GetProduct(int productID)
        {
            return productDB.Get(productID);
        }

        public static int AddProduct(Product data)
        {
            return productDB.Add(data);
        }

        public static bool UpdateProduct(Product data)
        {
            return productDB.Update(data);
        }

        public static bool DeleteProduct(int productID)
        {
            if (productDB.InUsed(productID))
                return false;
            return productDB.Delete(productID);
        }

        public static bool InUsedProduct(int productID)
        {
            return productDB.InUsed(productID);
        }

        public static ProductAttribute GetProductAttribute(int attributeID)
        {
            return productAttributeDB.Get(attributeID);
        }

        public static List<ProductAttribute> ListOfProductAttribute()
        {
            return productAttributeDB.List().ToList();
        }
        public static int AddProductAttribute(ProductAttribute data)
        {
            return productAttributeDB.Add(data);
        }
        public static bool UpdateProductAttribute(ProductAttribute data)
        {
            return productAttributeDB.Update(data);
        }
        public static bool DeleteProductAttribute(int attributeID)
        {
            return productAttributeDB.Delete(attributeID);
        }
    }

}
