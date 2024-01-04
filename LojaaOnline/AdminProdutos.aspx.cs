using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LojaaOnline
{
    public partial class AdminProdutos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Carregar todos os produtos ao carregar a página
            string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            string query = "SELECT TOP (1000) [cod_produto], [nome_produto], [stock], [descricao_produto], [preco_produto], [imagem_produto] FROM [LojaOnline].[dbo].[produtos]";

            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            RepeaterProducts.DataSource = reader;
            RepeaterProducts.DataBind();
            con.Close();
        }

        protected void Eliminarproduto(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                // Eliminar o produto quando o botão de eliminação é clicado
                int codProduto = Convert.ToInt32(e.CommandArgument);

                string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Chamar procedimento armazenado para eliminar o produto
                    using (SqlCommand cmd = new SqlCommand("EliminarProduto", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodProduto", codProduto);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Redirecionar de volta para a página de AdminProdutos após a eliminação
                Response.Redirect("AdminProdutos.aspx");
            }
        }

        protected void EditarProduto(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                // Obter o ID do produto a partir do argumento do comando e redirecionar para a página de edição
                int idProduto = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"AdminEditarProduto.aspx?idProduto={idProduto}");
            }
        }
    }
}