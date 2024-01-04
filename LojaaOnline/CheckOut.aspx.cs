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
    public partial class CheckOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Torna o botão de ver pedidos invisível por padrão
            btn_pedidos.Visible = false;

            // Verifica se é a primeira vez que a página é carregada
            if (!IsPostBack)
            {
                // Verifica se há um utilizador autenticado
                if (Session["IDUtilizador"] != null)
                {
                    int idUtilizador;

                    // Tenta converter o ID do utilizador para um inteiro
                    if (int.TryParse(Session["IDUtilizador"].ToString(), out idUtilizador))
                    {
                        // Obtém a string de conexão do arquivo de configuração
                        string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

                        // Estabelece uma conexão com a base de dados
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            // Executa um procedimento armazenado para calcular o total do carrinho
                            using (SqlCommand cmd = new SqlCommand("CalcularTotalCarrinho", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@id_utilizador", idUtilizador);

                                con.Open();
                                SqlDataReader dr = cmd.ExecuteReader();

                                // Se houver linhas no resultado
                                if (dr.HasRows)
                                {
                                    dr.Read();

                                    // Verifica se o campo TotalProdutos não é nulo
                                    if (dr["TotalProdutos"] != DBNull.Value)
                                    {
                                        int totalProdutos = Convert.ToInt32(dr["TotalProdutos"]);
                                        decimal totalCarrinho = Convert.ToDecimal(dr["TotalCarrinho"]);
                                        lblTotalProdutos.Text = totalProdutos.ToString();
                                        lblTotalCarrinho.Text = totalCarrinho.ToString("C");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // Método para inserir um pedido na base de dados
        private void InserirPedido(int idUtilizador, string nome, string morada, string telemovel, string codigoPostal)
        {
            // Obtém a string de conexão do arquivo de configuração
            string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

            // Estabelece uma conexão com a base de dados
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Executa um procedimento armazenado para inserir o pedido
                using (SqlCommand cmd = new SqlCommand("InserirPedido", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDUtilizador", idUtilizador);
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    cmd.Parameters.AddWithValue("@Morada", morada);
                    cmd.Parameters.AddWithValue("@Telemovel", telemovel);
                    cmd.Parameters.AddWithValue("@CodigoPostal", codigoPostal);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Evento de clique no botão de checkout
        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            // Obtém os dados do formulário
            string nome = txtNome.Text;
            string morada = txtMorada.Text;
            string telemovel = txtTelemovel.Text;
            string codigoPostal = txtCodigoPostal.Text;

            // Verifica se os campos obrigatórios foram preenchidos
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(morada) || string.IsNullOrEmpty(telemovel))
            {
                lbl_mensagemerro.Text = "Por favor, preencha todos os campos obrigatórios.";
                lbl_mensagemerro.Visible = true;
            }
            else
            {
                // Verifica se há um utilizador autenticado
                if (Session["IDUtilizador"] != null)
                {
                    int idUtilizador = Convert.ToInt32(Session["IDUtilizador"]);

                    int temCarrinho;
                    string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

                    // Verifica se o utilizador tem produtos no carrinho
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("VerificarCarrinhoDoUtilizador", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@IDUtilizador", idUtilizador);
                            con.Open();
                            temCarrinho = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }

                    // Se o utilizador tiver produtos no carrinho
                    if (temCarrinho == 1)
                    {
                        // Insere o pedido na base de dados
                        InserirPedido(idUtilizador, nome, morada, telemovel, codigoPostal);
                        lblMensagem.Text = "Pedido efetuado com sucesso. Aguardando confirmação.";
                        lblMensagem.Visible = true;
                        lbl_mensagemerro.Visible = false;
                        divFormularioEntrega.Visible = false;
                        btnCheckout.Visible = false;
                        btn_voltar.Visible = false;
                        btn_pedidos.Visible = true;
                    }
                    else
                    {
                        lbl_mensagemerro.Text = "Não pode fazer o checkout com o carrinho vazio.";
                        lbl_mensagemerro.Visible = true;
                    }
                }
                else
                {
                    lbl_mensagemerro.Text = "Precisa de fazer login para fazer um pedido.";
                    lbl_mensagemerro.Visible = true;
                }
            }
        }

        // Evento de clique no botão "Voltar"
        protected void btn_voltar_Click(object sender, EventArgs e)
        {
            // Redireciona para a página anterior
            Response.Redirect("Carrinho.aspx");
        }

        // Evento de clique no botão "Ver Pedidos"
        protected void btn_pedidos_Click(object sender, EventArgs e)
        {
            // Redireciona para a página de visualização de pedidos
            Response.Redirect("Pedidos.aspx");
        }
    }
}