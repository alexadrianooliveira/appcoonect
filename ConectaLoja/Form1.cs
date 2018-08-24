using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.NetworkInformation;
using System.Media;

namespace ConectaLoja
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public bool CheckConnection()
        {
            bool retorno = false;
            try
            {
                //Verifica se tem internet;
                bool bb = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

                if (bb)
                {
                    //Verifica se a url do servidor está acessivel
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                    HttpWebResponse response = (HttpWebResponse)req.GetResponse();

                    if (response.StatusCode == HttpStatusCode.OK)
                        retorno = true;

                    response.Close();
                }
            }
            catch (Exception)
            {

            }
            return retorno;
        }




        //private string url = "https://api.tabletcloud.com.br/";
        private string url = Utils.Constantes.URL;
        bool lojaLigada = false;
      

        delegate void RecebePedidoCallback(string msg);
        void RecebePedido(string msg)
        {
            if (InvokeRequired)
            {
                RecebePedidoCallback callback = RecebePedido;
                Invoke(callback, msg);
            }
            else
            {
                ShowInTaskbar = true;
                notifyIcon1.Visible = false;
                WindowState = FormWindowState.Normal;
                listResultado.Items.Add("Novo Pedido:" + msg);
            }
        }

        protected void CarregaArquivos(List<Models.PedidoItem> lista)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = lista;
        }

        void RecebePedidoCompleto(string msg)
        {
            if (InvokeRequired)
            {
                RecebePedidoCallback callback = RecebePedidoCompleto;
                Invoke(callback, msg);
            }
            else
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Models.Pedido pedido = serializer.Deserialize<Models.Pedido>(msg);

                ShowInTaskbar = true;
                notifyIcon1.Visible = false;
                WindowState = FormWindowState.Normal;
                listResultado.Items.Add("Cliente:" + pedido.Cliente.CPF + " - " + pedido.Cliente.Nome);
                listResultado.Items.Add("*************************************");

                //foreach (Itens item in pedido.itens)
                //{
                //    listResultado.Items.Add(item.codigo + " - " + item.produto + " - " + item.valor);
                //}
                //listResultado.Items.Add("*************************************");
                //listResultado.Items.Add("VALOR TOTAL" + pedido.ValorTotal);
                CarregaArquivos(pedido.Itens);
                playSimpleSound();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listResultado.Items.Clear();

            int codloja = Convert.ToInt32(txtCodLoja.Text);
            //string nomeLoja = txtNomeLoja.Text;

            listResultado.Items.Add("Estabelendo conexão com o servidor.");
            if (CheckConnection() == false)
            {
                listResultado.Items.Add("Você está sem internet, por favor verifique sua conexão");
                timer1.Stop();
            }
            else
            {
                listResultado.Items.Add("URL: " + url);
                var hubConnection = new HubConnection(url);
                var chat = hubConnection.CreateHubProxy("LojaHub");

                listResultado.Items.Add("Fazendo tentativa de ligar a loja...");
                hubConnection.Start().Wait();

                listResultado.Items.Add("Servidor iniciado, aguardando para ligar a loja...");
                chat.Invoke("AbrirLoja", codloja, "Loja Demo");

                listResultado.Items.Add("Loja funcionando: " + DateTime.Now);

                chat.On("acionaloja", (pedido) =>
                {
                    RecebePedido(pedido);
                });

                chat.On("acionalojaPedido", (pedido) =>
                {
                    RecebePedidoCompleto(pedido);
                });

                lblStatus.Text = "Online";
                lblStatus.ForeColor = Color.Green;
                button1.Visible = false;
                btnDesligar.Visible = true;

                timer1.Start();
            }

            //playSimpleSound();

            //IrParaTarefas();
        }

        private void playSimpleSound()
        {
            try
            {
                string pasta = Application.StartupPath + "\\Resources\\interfone.wav";
                SoundPlayer simpleSound = new SoundPlayer(pasta);
                simpleSound.Play();
            }
            catch (Exception)
            { }
        }

        protected void IrParaTarefas()
        {
            notifyIcon1.BalloonTipText = "Fique tranquilo avisaremos se alguem fizer um pedido";
            notifyIcon1.BalloonTipTitle = "Loja inicializada";

            ShowIcon = false;
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(1000);

            WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            lojaLigada = true;
            //contextMenuStrip1.Show();

            //EscutaPedido();
        }

        public void Desligar()
        {
            listResultado.Items.Clear();

            int codloja = Convert.ToInt32(txtCodLoja.Text);
            //string nomeLoja = txtNomeLoja.Text;

            if (CheckConnection())
            {

                listResultado.Items.Add("Estabelendo conexão com o servidor.");
                var hubConnection = new HubConnection(url);
                var chat = hubConnection.CreateHubProxy("LojaHub");

                listResultado.Items.Add("Fazendo tentativa de desligar a loja...");
                hubConnection.Start().Wait();

                listResultado.Items.Add("Servidor iniciado, aguardando para desligar a loja...");
                chat.Invoke("FecharLoja", codloja);

                listResultado.Items.Add("Loja Fechada");
                hubConnection.Stop();
                hubConnection.Dispose();
            }
            lojaLigada = false;

            lblStatus.Text = "Offline";
            lblStatus.ForeColor = Color.DarkRed;
            button1.Visible = true;
            btnDesligar.Visible = false;
            timer1.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Desligar();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Desligar();
        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowInTaskbar = true;
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized && lojaLigada)
            {
                this.ShowInTaskbar = false;
                ShowIcon = false;
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(1000);
            }
            //WindowState = FormWindowState.Minimized;
            //this.ShowInTaskbar = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //EscutaPedido();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtCodLoja.Text = Logar.codLoja.ToString();   
        }

        private void btnDesligar_Click(object sender, EventArgs e)
        {
            Desligar();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           bool ligado = CheckConnection();
            if (!ligado)
            {
                Desligar();
                listResultado.Items.Add("A loja foi fechada pois você perdeu a conexão com a internet: " + DateTime.Now.ToString());
            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized && lojaLigada)
            {
                this.ShowInTaskbar = false;
                ShowIcon = false;
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(1000);
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
      
    }
}
