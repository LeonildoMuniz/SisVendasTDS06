using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SisTDS06
{
    public partial class FormCliente : Form
    {
        public FormCliente()
        {
            InitializeComponent();
        }

        private void FormCliente_Load(object sender, EventArgs e)
        {
             Cliente cli = new Cliente();
             List<Cliente> cliente = cli.listacliente();
             dgvUsuario.DataSource = cliente;
        }

        private void btnCep_Click(object sender, EventArgs e)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://viacep.com.br/ws/" + txtCep.Text + "/json");
            request.AllowAutoRedirect = false;
            HttpWebResponse ChecaServidor = (HttpWebResponse)request.GetResponse();
            if (ChecaServidor.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("Servidor Indisponível!");
                return; //Sai da rotina e para e codificação
            }
            using (Stream webStream = ChecaServidor.GetResponseStream())
            {
                if (webStream != null)
                {
                    using (StreamReader responseReader = new StreamReader(webStream))
                    {
                        string response = responseReader.ReadToEnd();
                        response = Regex.Replace(response, "[{},]", string.Empty);
                        response = response.Replace("\"", "");

                        String[] substrings = response.Split('\n');

                        int cont = 0;
                        foreach (var substring in substrings)
                        {
                            if (cont == 1)
                            {
                                string[] valor = substring.Split(":".ToCharArray());
                                if (valor[0] == "  erro")
                                {
                                    MessageBox.Show("CEP não encontrado!");
                                    txtCep.Focus();
                                    return;
                                }
                            }

                            //Endereço
                            if (cont == 2)
                            {
                                string[] valor = substring.Split(":".ToCharArray());
                                txtEndereco.Text = valor[1];
                            }

                            //Bairro
                            if (cont == 4)
                            {
                                string[] valor = substring.Split(":".ToCharArray());
                                txtBairro.Text = valor[1];
                            }

                            //Cidade
                            if (cont == 5)
                            {
                                string[] valor = substring.Split(":".ToCharArray());
                                txtCidade.Text = valor[1];
                            }
                            cont++;
                        }
                    }
                }
            }
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            Cliente c = new Cliente();
            c.Localiza(txtCPF.Text);
            txtNome.Text = c.nome;
            txtEndereco.Text = c.endereco;
            txtEmail.Text = c.email;
            txtBairro.Text = c.bairro;
            txtCelular.Text = c.celular;
            txtCidade.Text = c.cidade;
            txtCep.Text = c.cep;

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {

            try
            {
                Cliente cli = new Cliente();
                cli.Inserir(txtNome.Text, txtCelular.Text, dtpDtNascimento.Value, txtCep.Text, txtEndereco.Text, txtCidade.Text, txtBairro.Text, txtEmail.Text, txtCPF.Text);
                List<Cliente> cliente = cli.listacliente();
                MessageBox.Show("Cliente cadastrado com sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvUsuario.DataSource = cliente;
                txtNome.Text = "";
                txtCelular.Text = "";
                this.dtpDtNascimento.Value = DateTime.Now.Date;
                txtCep.Text = "";
                txtEndereco.Text = "";
                txtCidade.Text = "";
                txtBairro.Text = "";
                txtEmail.Text = "";
                txtCPF.Text = "";
                ClassConecta.FecharConexao();
            }
            catch (Exception er)
            {

                MessageBox.Show(er.Message);
            }
            
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            try
            {
                Cliente cli = new Cliente();
                cli.Atualizar(txtNome.Text, txtCelular.Text, dtpDtNascimento.Value, txtCep.Text, txtEndereco.Text, txtCidade.Text, txtBairro.Text, txtEmail.Text, txtCPF.Text);
                List<Cliente> cliente = cli.listacliente();
                MessageBox.Show("Cliente atualizado com sucesso!", "Edição", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvUsuario.DataSource = cliente;
                txtNome.Text = "";
                txtCelular.Text = "";
                this.dtpDtNascimento.Value = DateTime.Now.Date;
                txtCep.Text = "";
                txtEndereco.Text = "";
                txtCidade.Text = "";
                txtBairro.Text = "";
                txtEmail.Text = "";
                txtCPF.Text = "";
                ClassConecta.FecharConexao();
            }
            catch (Exception er)
            {

                MessageBox.Show(er.Message);
            }
            
           
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

            try
            {
                Cliente cli = new Cliente();
                cli.Exclui(txtCPF.Text);
                List<Cliente> cliente = cli.listacliente();
                MessageBox.Show("Usuário excluído com sucesso!", "Exclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvUsuario.DataSource = cliente;
                txtNome.Text = "";
                txtCelular.Text = "";
                this.dtpDtNascimento.Value = DateTime.Now.Date;
                txtCep.Text = "";
                txtEndereco.Text = "";
                txtCidade.Text = "";
                txtBairro.Text = "";
                txtEmail.Text = "";
                txtCPF.Text = "";
                ClassConecta.FecharConexao();
            }
            catch (Exception er)
            {

                MessageBox.Show(er.Message);
            }

        }

        private void btnLimpaCampos_Click(object sender, EventArgs e)
        {

            txtNome.Text = "";
            txtCelular.Text = "";
            this.dtpDtNascimento.Value = DateTime.Now.Date;
            txtCep.Text = "";
            txtEndereco.Text = "";
            txtCidade.Text = "";
            txtBairro.Text = "";
            txtEmail.Text = "";
            txtCPF.Text = "";
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
