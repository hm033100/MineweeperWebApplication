using MineweeperWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineweeperWebApplication.Service
{
    public class UserService
    {

        UserDAO userDAO = new UserDAO();

        public UserService() { }

        public bool LoginUser(User user)
        {
            return userDAO.authenticateUser(user);
        }

        public bool RegisterUser(User user)
        {
            if (userDAO.authenticateUser(user) == true)
            {
                return false;
            } 
            else
            {
                return userDAO.createUser(user);
            }
        }

    }
}
