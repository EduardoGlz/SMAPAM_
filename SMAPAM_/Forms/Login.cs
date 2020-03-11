using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain.Models;
using Common.Cache;

namespace SMAPAM_.Forms
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

        }

        //Componentes para poder mover la pantalla
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        //Fin de componentes para mover la ventana

        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        //Método para minimizar la venta de Login
        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //Método para cerrar la ventana
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //Método 'Enter' donde se validará la entrada del texto para usuario y se mostrará la palabra 'Usuario'
        //en caso de no estar seleccionado el textbox ni ingresado cadena
        private void txtUser_Enter(object sender, EventArgs e)
        {
            if (txtUser.Text == "Usuario")
            {
                txtUser.Text = "";
                txtUser.ForeColor = Color.LightGray;
            }
        }
        //Método 'Enter' donde se validará la entrada del texto para la contraseña y se mostrará la palabra 'Contraseña'
        //en caso de no estar seleccionado el textbox ni ingresado cadena
        private void txtPass_Enter(object sender, EventArgs e)
        {
            if (txtPass.Text == "Contraseña")
            {
                txtPass.Text = "";
                txtPass.ForeColor = Color.LightGray;
                txtPass.UseSystemPasswordChar = true;
            }
        }
        //Método para regresar a la palabra Usuario en caso de salirse del textBox
        private void txtUser_Leave(object sender, EventArgs e)
        {
            if (txtUser.Text == "")
            {
                txtUser.Text = "Usuario";
                txtUser.ForeColor = Color.DimGray;
            }
        }
        //Método para regresar a la palabra Contraseña en caso de salirse del textBox
        private void txtPass_Leave(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                txtPass.Text = "Contraseña";
                txtPass.ForeColor = Color.DimGray;
                txtPass.UseSystemPasswordChar = false;
            }
        }
        //Método de Panel 'MouseDown' colocamos las propiedades para que el panel pueda moverse con el cursor
        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        //Método de botón 'Click' donde se validará el usuario y contraseña en la BD y permitirá el acceso al sistema
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUser.Text != "Usuario")
            {
                if (txtPass.Text != "Contraseña")
                {
                    UserModel user = new UserModel();
                    var validLogin = user.LoginUser(txtUser.Text, txtPass.Text);
                    if (validLogin == true)
                    {
                        Principal mainMenu = new Principal();
                        MessageBox.Show("Bienvenido: " + UserLoginCache.usuario);
                        mainMenu.Show();
                        mainMenu.FormClosed += Logout;
                        this.Hide();
                    }
                    else
                    {
                        //Mensajes y enfoques en caso de que un campo no coincida
                        msgError("Usuario o Contraseña incorrectos. \n Por favor inténtelo de nuevo");
                        txtPass.Text = "";
                        txtUser.Focus();
                    }
                }
                else msgError("Ingresa tu contraseña");
            }
            else msgError("Ingresa tu nombre de Usuario");
        }
        private void msgError(string msg)
        {
            lblErrorMessage.Text = "    " + msg;
            lblErrorMessage.Visible = true;
        }
        private void Logout(object sender, FormClosedEventArgs e)
        {
            txtPass.Text = "Contraseña";
            txtPass.UseSystemPasswordChar = false;
            txtUser.Text = "Usuario";
            lblErrorMessage.Visible = false;
            this.Show();
            //txtUser.Focus();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var recoverPassword = new RecoverPassword();
            recoverPassword.Show();
            this.Hide();
        }
    }
}
