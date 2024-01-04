using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace LojaaOnline
{
    public partial class Produtos : System.Web.UI.Page
    {
        private const int ProdutosPorPagina = 9;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Carregamento dos produtos ao carregar a página
                string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);

                string query = "SELECT [cod_produto], [nome_produto], [stock], [descricao_produto], [preco_produto], [cod_tipo], [imagem_produto] FROM [LojaOnline].[dbo].[produtos]";

                SqlCommand command = new SqlCommand(query, con);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                reptProduct.DataSource = reader;
                reptProduct.DataBind();
                con.Close();

                // Atualização dos produtos com base na página atual
                AtualizarProdutos();
            }
        }

        protected void btnOrdenar_Click(object sender, EventArgs e)
        {
            // Ordenar produtos com base nos critérios selecionados
            string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            string criterioOrdenacao = ddlCriterioOrdenacao.SelectedValue;
            string ordem = ddlOrdem.SelectedValue;

            string query = $"SELECT [cod_produto], [nome_produto], [stock], [descricao_produto], [preco_produto], [cod_tipo], [imagem_produto] FROM [LojaOnline].[dbo].[produtos] ORDER BY {criterioOrdenacao} {ordem}";

            SqlCommand command = new SqlCommand(query, con);

            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            reptProduct.DataSource = reader;
            reptProduct.DataBind();
            con.Close();
        }

        protected void btn_adicionarcarrinho_Click(object sender, EventArgs e)
        {
            // Adicionar produto ao carrinho
            if (Session["Util"] == null)
            {
                lbl_mensagem.Text = "Precisa de fazer login para adicionar produtos ao seu carrinho!";
            }
            else
            {
                Button btn = (Button)sender;
                string idProduto = btn.CommandArgument;

                string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);

                using (SqlCommand cmd = new SqlCommand("sp_AdicionarProdutoNoCarrinho", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cod_produto", idProduto);
                    cmd.Parameters.AddWithValue("@cod", Session["IDUtilizador"]);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                lbl_mensagem_sucesso.Text = "Produto adicionado ao carrinho !";
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            // Pesquisar produtos por nome
            string nomeProduto = txtPesquisa.Text;

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ProcurarProdutos", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NomeProduto", nomeProduto);

                        connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reptProduct.DataSource = reader;
                                reptProduct.DataBind();
                            }
                            else
                            {
                                reptProduct.DataSource = null;
                                reptProduct.DataBind();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lbl_mensagem.Text = $"Erro: {ex.Message}";
            }
        }

        protected void ddlTipoProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Filtrar produtos por tipo
            int tipoProduto = Convert.ToInt32(ddlTipoProduto.SelectedValue);

            string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ListarProdutosPorTipo", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TipoSelecionado", tipoProduto);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reptProduct.DataSource = reader;
                                reptProduct.DataBind();
                            }
                            else
                            {
                                reptProduct.DataSource = null;
                                reptProduct.DataBind();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lbl_mensagem.Text = $"Erro: {ex.Message}";
                    }
                }
            }
        }

        protected void AtualizarProdutos()
        {
            // Atualizar a exibição dos produtos na página com base na paginação
            int totalProdutos = ObterTotalProdutos();
            int totalPaginas = (int)Math.Ceiling((double)totalProdutos / ProdutosPorPagina);

            DataTable dt = ObterProdutosPaginados();

            reptProduct.DataSource = dt;
            reptProduct.DataBind();

            AtualizarControlesPaginacao(totalPaginas);
        }

        private int ObterTotalProdutos()
        {
            // Obter o total de produtos na base de dados
            int total = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [LojaOnline].[dbo].[produtos]", con))
                {
                    con.Open();
                    total = (int)cmd.ExecuteScalar();
                }
            }

            return total;
        }

        private DataTable ObterProdutosPaginados()
        {
            // Obter os produtos de acordo com a página atual
            DataTable dt = new DataTable();

            int paginaAtual = ObterPaginaAtual();
            int indiceInicial = (paginaAtual - 1) * ProdutosPorPagina + 1;
            int indiceFinal = indiceInicial + ProdutosPorPagina - 1;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString))
            {
                string query = $"SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY [nome_produto]) AS RowNum, * FROM [LojaOnline].[dbo].[produtos]) AS Sub WHERE RowNum BETWEEN {indiceInicial} AND {indiceFinal}";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }

        private int ObterPaginaAtual()
        {
            // Obter a página atual a partir da QueryString
            if (!string.IsNullOrEmpty(Request.QueryString["pagina"]))
            {
                return Convert.ToInt32(Request.QueryString["pagina"]);
            }
            return 1;
        }

        private void AtualizarControlesPaginacao(int totalPaginas)
        {
            // Atualizar os controlos de paginação
            rptPager.DataSource = Enumerable.Range(1, totalPaginas);
            rptPager.DataBind();
        }

        protected void lnkPagina_Click(object sender, EventArgs e)
        {
            // Redirecionar para a página selecionada
            int paginaSelecionada = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            Response.Redirect($"Produtos.aspx?pagina={paginaSelecionada}");
        }

        protected void lnkAnterior_Click(object sender, EventArgs e)
        {
            // Navegar para a página anterior
            int paginaAtual = ObterPaginaAtual();
            if (paginaAtual > 1)
            {
                Response.Redirect($"Produtos.aspx?pagina={paginaAtual - 1}");
            }
        }

        protected void lnkProxima_Click(object sender, EventArgs e)
        {
            // Navegar para a próxima página
            int paginaAtual = ObterPaginaAtual();
            int totalPaginas = (int)Math.Ceiling((double)ObterTotalProdutos() / ProdutosPorPagina);

            if (paginaAtual < totalPaginas)
            {
                Response.Redirect($"Produtos.aspx?pagina={paginaAtual + 1}");
            }
        }
    }
}



