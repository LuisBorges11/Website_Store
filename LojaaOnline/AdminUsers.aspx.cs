using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LojaaOnline
{
    public partial class AdminUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // string de conexão do arquivo de configuração
            string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

            // Cria uma conexão com a base de dados
            SqlConnection con = new SqlConnection(connectionString);

            // Query para obter os utilizadores
            string query = "SELECT TOP (1000) [cod], [utilizador], [palavra_passe], [email], [ativo], [data], [cod_perfil] FROM [LojaOnline].[dbo].[utilizadores]";

            // Cria um comando SQL
            SqlCommand command = new SqlCommand(query, con);

            // Abre a conexão com a base de dados
            con.Open();

            // Executa a consulta SQL e vincula os resultados ao Repeater
            SqlDataReader reader = command.ExecuteReader();
            RepeaterUsers.DataSource = reader;
            RepeaterUsers.DataBind();

            // Fecha a conexão com a base de dados
            con.Close();
        }

        protected void EliminarUtilizador(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                // Obtém o código do utilizador a ser eliminado
                int cod = Convert.ToInt32(e.CommandArgument);

                // Obtém a string de conexão do arquivo de configuração
                string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

                // Cria uma nova conexão com a base de dados
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Abre a conexão
                    con.Open();

                    // Cria um comando SQL para chamar a stored procedure "EliminarUtilizador"
                    using (SqlCommand cmd = new SqlCommand("EliminarUtilizador", con))
                    {
                        // Define o tipo de comando como stored procedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Adiciona o parâmetro para o código do utilizador
                        cmd.Parameters.AddWithValue("@Cod", cod);

                        // Executa o comando SQL
                        cmd.ExecuteNonQuery();
                    }
                }

                // Redireciona para a página AdminUsers após a exclusão
                Response.Redirect("AdminUsers.aspx");
            }
        }

        protected void EditarUtilizador(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                // Obtém o ID do utilizador a ser editado
                int idUtilizador = Convert.ToInt32(e.CommandArgument);

                // Redireciona para a página editar utilizador com o ID correspondente
                Response.Redirect($"AdminEditarUtilizador.aspx?idUtilizador={idUtilizador}");
            }
        }
    }
}