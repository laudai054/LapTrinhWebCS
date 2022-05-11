using SV18T1021246.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021246.DataLayer
{
    public interface IAccountDAL
    {

        bool Login(string username, string password);
        bool ChangePassword(Account data);
        Account Get(int employeeID);

        Account GetUsername(string username);

    }
}
