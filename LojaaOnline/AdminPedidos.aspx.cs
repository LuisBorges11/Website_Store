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
    public partial class AdminPedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Obter todos os pedidos ao carregar a página pela primeira vez
                string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("ObterTodosOsPedidos", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                repeaterPedidos.DataSource = dt;
                                repeaterPedidos.DataBind();
                            }
                            else
                            {
                                // Nenhum pedido encontrado, exibir mensagem
                                lblSemPedidos.Visible = true;
                            }
                        }
                    }
                }
            }
        }

        protected void Confirmarpedido(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Confirmar")
            {
                // Confirmar o pedido quando o botão de confirmação é clicado
                int idPedido = Convert.ToInt32(e.CommandArgument);
                int idUtilizador = Convert.ToInt32(Session["IDUtilizador"]);

                string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("AtualizarStatusPedidoParaConfirmado", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idPedido", idPedido);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Redirecionar de volta para a página de AdminPedidos após a confirmação
                Response.Redirect("AdminPedidos.aspx");
            }
        }

        protected void Eliminarpedido(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                // Eliminar o pedido quando o botão de eliminação é clicado
                int idPedido = Convert.ToInt32(e.CommandArgument);
                int idUtilizador = Convert.ToInt32(Session["IDUtilizador"]);

                string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("EliminarPedido", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idPedido", idPedido);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Redirecionar de volta para a página de AdminPedidos após a eliminação
                Response.Redirect("AdminPedidos.aspx");
            }
        }
    }
}