using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LojaaOnline
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica se é a primeira vez que a página está a ser carregada
            if (!IsPostBack)
            {
                // Chama o método para carregar os produtos em destaque apenas quando a página é carregada pela primeira vez
                CarregarProdutosEmDestaque();
            }
        }

        // Método para carregar os produtos em destaque na página
        protected void CarregarProdutosEmDestaque()
        {
            // String de conexão do arquivo de configuração
            string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

            // Instrução 'using' para garantir que os recursos são liberados corretamente
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Comando SQL para chamar a stored procedure 'ObterProdutosEmDestaqueAleatorios'
                using (SqlCommand cmd = new SqlCommand("ObterProdutosEmDestaqueAleatorios", con))
                {
                    // Define o tipo do comando como stored procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Abre a conexão com a base de dados
                    con.Open();

                    // Executa o comando SQL para obter um leitor de dados
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Objeto repeater como a fonte de dados e faz o binding dos dados
                    repeaterProdutosDestaque.DataSource = reader;
                    repeaterProdutosDestaque.DataBind();
                }
            }
        }
    }
}