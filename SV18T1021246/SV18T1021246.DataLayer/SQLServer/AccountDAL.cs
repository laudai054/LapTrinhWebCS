using SV18T1021246.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021246.DataLayer.SQLServer
{
    public class AccountDAL : _BaseDAL, IAccountDAL
    {
        public AccountDAL(string connectionString) : base(connectionString)
        {
        }

        public bool ChangePassword(Account data)
        {
            bool result = false;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Employees 
                                        SET Password = @password
                                    WHERE EmployeeID = @employeeID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@password", data.Password);
                cmd.Parameters.AddWithValue("@employeeID", data.EmployeeID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }

            return result;
        }

        public bool Login(string username, string password)
        {
            bool result = false;
            
            using(SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT *
                                    FROM Employees
                                    WHERE 
                                        Email = @username AND Password = @password ";
                // Email = N'" + username + "' AND Password = N'" + password + "' ";
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                result = Convert.ToBoolean(cmd.ExecuteScalar());

                cn.Close();
            }

            return result;
        }
        public Account Get(int employeeID)
        {
            Account result = null;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Employees WHERE EmployeeID = @employeeID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@employeeID", employeeID);

                var dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    result = new Account()
                    {
                        EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                        Email = Convert.ToString(dbReader["Email"]),
                        Password = Convert.ToString(dbReader["Password"])
                    };
                }

                cn.Close();
            }

            return result;

        }

        public Account GetUsername(string username)
        {
            Account result = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Employees WHERE Email = @username";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@username", username);

                var dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    result = new Account()
                    {
                        EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                        Email = Convert.ToString(dbReader["Email"]),
                        Password = Convert.ToString(dbReader["Password"])
                    };
                }

                cn.Close();
            }

            return result;

        }
    }
}
