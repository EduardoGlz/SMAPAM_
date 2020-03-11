using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain.Models;

namespace SMAPAM_.Forms
{
    public partial class RecoverPassword : Form
    {
        public RecoverPassword()
        {
            InitializeComponent();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {

        }

        private void RecoverPassword_Load(object sender, EventArgs e)
        {

        }
        //
        //private void btnEnviar_Click_1(object sender, EventArgs e)
        //{
         //   var user = new UserModel();
         //   var result = user.recoverPassword(txtUserRequest.Text);
         //   label2.Text = result;
        //}

        private void txtUserRequest_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnEnviar_Click_2(object sender, EventArgs e)
        {
            var user = new UserModel();
            var result = user.recoverPassword(txtUserRequest.Text);
            label2.Text = result;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            MessageBox.Show("Regresará al inicio de sesión");
            this.Close();
            log.Show();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
