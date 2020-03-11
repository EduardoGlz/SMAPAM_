using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.SqlServer;

namespace Domain.Models
{
    public class UserModel
    {
        UserDao userDao = new UserDao();
        public bool LoginUser(string user, string pass)
        {
            return userDao.Login(user, pass);
        }

        public bool insertar(string usuario, string contrasenia, string nombre, string apellidos)
        {
            return userDao.agregarUsuario(usuario, contrasenia, nombre, apellidos);
        }

        public string recoverPassword(string userRequesting)
        {
            return userDao.recoverPassword(userRequesting);
        }
    }
}
