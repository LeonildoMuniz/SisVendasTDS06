using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace SisTDS06
{
    public partial class FormProduto : Form
    {
        public FormProduto()
        {
            InitializeComponent();
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {


            try
            {
                Produto p = new Produto();
                p.Localiza(Convert.ToInt32(txtId.Text));
                txtNome.Text = p.nome;
                txtQuantidade.Text = p.quantidade.ToString();
                txtValor.Text = p.valor.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                Produto p = new Produto();
                p.Inserir(txtNome.Text,Convert.ToInt32(txtQuantidade.Text),txtValor.Text);
                MessageBox.Show("Produto cadastrado com sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Produto> produtos = p.listaProduto();
                dgvProduto.DataSource = produtos;
                txtId.Text = "";
                txtNome.Text = "";
                txtQuantidade.Text = "";
                txtValor.Text = "";
                txtId.Focus();
                ClassConecta.FecharConexao();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void FormProduto_Load(object sender, EventArgs e)
        {
            Produto p = new Produto();
            List<Produto> produtos = p.listaProduto();
            dgvProduto.DataSource = produtos;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                Produto p = new Produto();
                p.Atualizar(Convert.ToInt32(txtId.Text),txtNome.Text, Convert.ToInt32(txtQuantidade.Text), Convert.ToDouble(txtValor.Text));
                MessageBox.Show("Produto Atualizado com sucesso!", "Atualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Produto> produtos = p.listaProduto();
                dgvProduto.DataSource = produtos;
                txtId.Text = "";
                txtNome.Text = "";
                txtQuantidade.Text = "";
                txtValor.Text = "";
                txtId.Focus();
                ClassConecta.FecharConexao();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                Produto p = new Produto();
                p.Exclui(Convert.ToInt32(txtId.Text));
                MessageBox.Show("Produto excluido com sucesso!", "Exclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Produto> produtos = p.listaProduto();
                dgvProduto.DataSource = produtos;
                txtId.Text = "";
                txtNome.Text = "";
                txtQuantidade.Text = "";
                txtValor.Text = "";
                txtId.Focus();
                ClassConecta.FecharConexao();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnLimpaCampos_Click(object sender, EventArgs e)
        {
            Produto p = new Produto();
            List<Produto> produtos = p.listaProduto();
            dgvProduto.DataSource = produtos;
            txtId.Text = "";
            txtNome.Text = "";
            txtQuantidade.Text = "";
            txtValor.Text = "";
            txtId.Focus();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
