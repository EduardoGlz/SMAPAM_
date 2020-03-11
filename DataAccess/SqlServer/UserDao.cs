using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using Common.Cache;

namespace DataAccess.SqlServer
{
    //Dao del Usuario
    public class UserDao : ConexionSQL
    {
        //Validar Login del Usuario
        public bool Login(string user, string pass)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM usuarios WHERE usuario=@user and pass=@pass";
                    command.Parameters.AddWithValue("@user", user);
                    command.Parameters.AddWithValue("@pass", pass);
                    command.CommandType = CommandType.Text;
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserLoginCache.usuario = reader.GetString(0);
                            UserLoginCache.contrasenia = reader.GetString(1);
                        }
                        return true;
                    }
                    else
                        return false;
                }
            }
        }
        //Agregar Usuarios
        public bool agregarUsuario(string usuario, string contrasenia, string nombre, string apellidos)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO usuarios (usuario, contrasenia) " +
                "VALUES (@usuario, @contrasenia)";
                    command.Parameters.AddWithValue("usuario", usuario); //Email
                    command.Parameters.AddWithValue("contrasenia", contrasenia);
                    command.CommandType = CommandType.Text;
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserLoginCache.usuario = reader.GetString(0);
                            UserLoginCache.contrasenia = reader.GetString(1);
                        }
                        return true;
                    }
                    else
                        return false;
                }
            }

        }

        //Recuperar contraseña
        public string recoverPassword(string userRequesting)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select * from usuarios where usuario=@user";
                    command.Parameters.AddWithValue("@user", userRequesting);
                    command.CommandType = CommandType.Text;
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read() == true)
                    {
                        string userName = reader.GetString(0);
                        string userMail = reader.GetString(2);
                        string accountPassword = reader.GetString(1);


                        var mailService = new MailServices.SystemSupportMail();
                        mailService.sendMail(
                            subject: "SYSTEM: Solicitud recuperación de contraseña",
                            body: "Hola, " + userName + "\n Tu request para recuperar tu contraseña. \n" +
                            "tu contraseña actual es: " + accountPassword +
                            "\nSin embargo, te recomendamos cambiar tu contraseña  \n"+
                            "\ninmediatamentedespués de iniciar sesión en el sistema.",
                            recipientMail: new List<string> { userMail }

                            );
                        return "Hola, " + userName + "\n Tu request para recuperar tu contraseña. \n" +
                  "Por favor revisa tu correo: " + userMail +
                  "\nSin embargo, te recomendamos cambiar tu contraseña \n"+
                  "inmediatamente después de iniciar sesión en el sistema.";
                    }
                    else
                    {
                        return "Lo sentimos, no tiene una cuenta con \n"+
                            "este usuario o correo electrónico";
                    }
                }
            }
        }
        //Fin recuperar contraseña




    }
}
