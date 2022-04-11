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
    /// <summary>
    /// các chức năng nghiệp vụ liên quan đến dữ liệu chung
    /// </summary>
    public static class CommonDataService
    {
        private static ICategoryDAL categoryDB;
        /// <summary>
        /// constuctor
        /// </summary>
        static CommonDataService()
        {
            string provider = ConfigurationManager.ConnectionStrings["DB"].ProviderName;
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            switch (provider)
            {
                case "SQLServer":
                    categoryDB = new DataLayer.SQLServer.CategoryDAL(connectionString);
                    break;
                default:
                    categoryDB = new DataLayer.FakeDB.CategoryDAL();
                    break;
            }
        }

        /// <summary>
        /// lấy danh sách các loại hàng
        /// </summary>
        /// <returns></returns>
        public static List<Category> ListOfCategories()
        {
            return categoryDB.List().ToList();
        }
    }
}
