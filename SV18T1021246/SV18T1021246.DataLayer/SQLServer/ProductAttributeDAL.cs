using SV18T1021246.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021246.DataLayer.SQLServer
{
    public class ProductAttributeDAL : _BaseDAL, IProductAttributeDAL
    {
        public ProductAttributeDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(ProductAttribute data)
        {
            int result = 0;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO ProductAttributes(AttributeName, AttributeValue, DisplayOrder)
                                    VALUES(@attributeName, @attributeValue, @displayOrder)
                                    SELECT SCOPE_IDENTITY(); ";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@attributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@attributeValue", data.AttributeValue);
                cmd.Parameters.AddWithValue("@displayOrder", data.DisplayOrder);


                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }

            return result;
        }

        public bool Delete(int attributeID)
        {
            bool result = false;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM ProductAttributes WHERE AttributeID = @attributeID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@attributeID", attributeID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }

            return result;
        }

        public ProductAttribute Get(int attributeID)
        {
            ProductAttribute result = null;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM ProductAttributes WHERE AttributeID = @attributeID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@attributeID", attributeID);

                var dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    result = new ProductAttribute()
                    {
                        AttributeID = Convert.ToInt32(dbReader["AttributeID"]),
                        ProductID = Convert.ToInt32(dbReader["ProductID"]),
                        AttributeName = Convert.ToString(dbReader["AttributeName"]),
                        AttributeValue = Convert.ToString(dbReader["AttributeValue"]),
                        DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"])
                    };
                }

                cn.Close();
            }

            return result;
        }

        public IList<ProductAttribute> List()
        {
            List<ProductAttribute> data = new List<ProductAttribute>();

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM ProductAttributes", cn);
                cmd.CommandType = System.Data.CommandType.Text;

                var dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new ProductAttribute()
                    {
                        AttributeID = Convert.ToInt32(dbReader["AttributeID"]),
                        ProductID = Convert.ToInt32(dbReader["ProductID"]),
                        AttributeName = Convert.ToString(dbReader["AttributeName"]),
                        AttributeValue = Convert.ToString(dbReader["AttributeValue"]),
                        DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"])
                    });
                }

                cn.Close();
            }

            return data;
        }

        public bool Update(ProductAttribute data)
        {
            bool result = false;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE ProductAttributes 
                                        SET AttributeName = @attributeName,
                                        AttributeValue = @attributeValue,
                                        DisplayOrder = @displayOrder
                                    WHERE AttributeID = @attributeID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@attributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@attributeValue", data.AttributeValue);
                cmd.Parameters.AddWithValue("@displayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@attributeID", data.AttributeID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }

            return result;
        }
    }
}
