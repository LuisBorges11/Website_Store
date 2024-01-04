using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;

namespace LojaaOnline
{
    // Classe Produto para representar informações dos produtos
    public class Produto
    {
        public string Nome { get; set; }
        public int Stock { get; set; }
    }

    public partial class AdminMain : System.Web.UI.Page
    {
        // Método chamado quando a página é carregada pela primeira vez
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica se é uma solicitação de postback (retorna true se a página foi submetida de volta ao servidor)
            if (!IsPostBack)
            {
                // Atualiza as estatísticas apenas se não for um postback
                AtualizarEstatisticas();
            }
        }

        // Método para atualizar as estatísticas mostradas na página
        private void AtualizarEstatisticas()
        {
            // Obtém a string de conexão do arquivo de configuração
            string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

            // Conta o número de utilizadores e atualiza a Label1 com o resultado
            int numeroUtilizadores = ContarRegistros("SELECT COUNT(*) FROM utilizadores", connectionString);
            Label1.Text = $"{numeroUtilizadores}";

            // Conta o número de pedidos e atualiza a Label2 com o resultado
            int numeroPedidos = ContarRegistros("SELECT COUNT(*) FROM pedidos", connectionString);
            Label2.Text = $"{numeroPedidos}";

            // Conta o número de produtos e atualiza a Label3 com o resultado
            int numeroProdutos = ContarRegistros("SELECT COUNT(*) FROM produtos", connectionString);
            Label3.Text = $"{numeroProdutos}";
        }

        // Método para contar o número de registros em uma tabela na base de dados
        private int ContarRegistros(string query, string connectionString)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        // Método que retorna uma lista de produtos ordenados por stock
        protected List<Produto> ListaDeProdutosOrdenadosPorStock()
        {
            List<Produto> produtos = new List<Produto>();
            string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT nome_produto, stock FROM produtos ORDER BY stock ASC", con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Lê os resultados e cria objetos Produto correspondentes
                    while (reader.Read())
                    {
                        Produto produto = new Produto
                        {
                            Nome = reader["nome_produto"].ToString(),
                            Stock = Convert.ToInt32(reader["stock"])
                        };
                        produtos.Add(produto);
                    }
                }
            }

            return produtos;
        }
    }
}