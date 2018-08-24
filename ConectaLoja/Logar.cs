using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConectaLoja
{
    public partial class Logar : Form
    {
        public static int codLoja = 0;
        public Logar()
        {
            InitializeComponent();
            txtSenha.Text = "*";
            txtSenha.isPassword = true;           
        }

        private void LogarLoja(string user,string pass)
        {
            BLL.Security valida = new BLL.Security();
            int retorno = valida.checkStore(user, pass);

            if (retorno == 0)
                MessageBox.Show("Dados incorretos digite novamente", "Ops", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                codLoja = retorno;
                Form1 f = new Form1();
                f.Show();
                this.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = txtEmail.Text;
            string pass = txtSenha.Text;

            LogarLoja(user, pass);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtSenha_Enter(object sender, EventArgs e)
        {
            txtSenha.Text = "";
        }
    }
}
