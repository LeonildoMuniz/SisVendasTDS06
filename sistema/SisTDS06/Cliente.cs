using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace SisTDS06
{
    class Cliente
    {
        public string nome { get; set; }
        public string celular { get; set; }
        public DateTime dt_nascimento { get; set; }
        public string cep { get; set; }
        public string endereco { get; set; }
        public string cidade { get; set; }
        public string bairro { get; set; }
        public string email { get; set; }
        public string cpf { get; set; }

        public List<Cliente> listacliente()
        {
            List<Cliente> l = new List<Cliente>();
            SqlConnection con = ClassConecta.ObterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM cliente";
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Cliente c = new Cliente();
                c.nome = dr["nome"].ToString();
                c.celular = dr["celular"].ToString();
                c.dt_nascimento = Convert.ToDateTime(dr["dt_nascimento"]);
                c.cep = dr["cep"].ToString();
                c.endereco = dr["endereco"].ToString();
                c.cidade = dr["cidade"].ToString();
                c.bairro = dr["bairro"].ToString();
                c.email = dr["email"].ToString();
                c.cpf = dr["cpf"].ToString();
                l.Add(c);
            }
            return l;
        }

        public void Inserir(string nome, string celular, DateTime dt_nascimento, string cep, string endereco, string cidade, string bairro, string email, string cpf)
        {
            SqlConnection con = ClassConecta.ObterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO cliente(nome,celular,dt_nascimento,cep,endereco,cidade,bairro,email,cpf) VALUES ('" + nome + "','" + celular + "',Convert(DateTime,'" + dt_nascimento + "',103),'" + cep + "','" + endereco + "','" + cidade + "','" + bairro + "','" + email + "','"+ cpf +"')";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            ClassConecta.FecharConexao();
        }

        public void Localiza(string cpf)
        {
            SqlConnection con = ClassConecta.ObterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM cliente WHERE cpf='" + cpf + "'";
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                nome = dr["nome"].ToString();
                celular = dr["celular"].ToString();
                dt_nascimento = Convert.ToDateTime(dr["dt_nascimento"]);
                cep = dr["cep"].ToString();
                endereco = dr["endereco"].ToString();
                cidade = dr["bairro"].ToString();
                bairro = dr["bairro"].ToString();
                email = dr["email"].ToString();
                cpf = dr["cpf"].ToString();
            }
        }

        public void Atualizar(string nome, string celular, DateTime dt_nascimento, string cep, string endereco, string cidade, string bairro, string email, string cpf)
        {
            SqlConnection con = ClassConecta.ObterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE cliente SET nome='" + nome + "',celular='" + celular + "',dt_nascimento=Convert(DateTime,'" + dt_nascimento + "',103),cep='" + cep + "',endereco='" + endereco + "',cidade='" + cidade + "',bairro='" + bairro + "',email='" + email + "' WHERE cpf = '" + cpf + "'";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            ClassConecta.FecharConexao();
        }

        public void Exclui(string cpf)
        {
            SqlConnection con = ClassConecta.ObterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "DELETE FROM cliente WHERE cpf='" + cpf + "'";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            ClassConecta.FecharConexao();
        }
    }
}

