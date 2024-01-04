using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LojaaOnline
{
    public partial class AdminEditarProduto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica se a página está a ser carregada pela primeira vez
            if (!IsPostBack)
            {
                // Carrega os detalhes do produto apenas no primeiro carregamento da página
                CarregarDetalhesProduto();
            }
        }

        private void CarregarDetalhesProduto()
        {
            // Verifica se o parâmetro idProduto foi passado na URL
            if (Request.QueryString["idProduto"] != null)
            {
                int idProduto;
                // Tenta converter o idProduto para um número inteiro
                if (int.TryParse(Request.QueryString["idProduto"], out idProduto))
                {
                    // Conexão à base de dados
                    string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        // Stored procedure para obter os detalhes do produto com base no idProduto
                        using (SqlCommand cmd = new SqlCommand("SELECT * FROM Produtos WHERE cod_produto = @IdProduto", con))
                        {
                            cmd.Parameters.AddWithValue("@IdProduto", idProduto);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Preenche os controles na página com os detalhes do produto
                                    lblIdProduto.Text = reader["cod_produto"].ToString();
                                    txtNomeProduto.Text = reader["nome_produto"].ToString();
                                    txtStock.Text = reader["stock"].ToString();
                                    txtDescricaoProduto.Text = reader["descricao_produto"].ToString();
                                    txtPrecoProduto.Text = reader["preco_produto"].ToString();
                                    ddlCodTipo.SelectedValue = reader["cod_tipo"].ToString();
                                }
                            }
                        }
                    }
                }             
            }
        }

        protected void SalvarProduto_Click(object sender, EventArgs e)
        {
            // Obtém os valores dos controles na página
            int idProduto = Convert.ToInt32(lblIdProduto.Text);
            string nomeProduto = txtNomeProduto.Text;
            int stock = Convert.ToInt32(txtStock.Text);
            string descricaoProduto = txtDescricaoProduto.Text;
            decimal precoProduto = Convert.ToDecimal(txtPrecoProduto.Text);
            int codTipo = Convert.ToInt32(ddlCodTipo.SelectedValue);

            // Atualiza as informações do produto
            AtualizarProduto(idProduto, nomeProduto, stock, descricaoProduto, precoProduto, codTipo);

            // Mostra uma mensagem de sucesso e redireciona para a página de administração de produtos após 3 segundos
            lbl_mensagem.Text = "Produto editado com sucesso!";
            string script = "<meta http-equiv='refresh' content='3;url=AdminProdutos.aspx'>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script);
        }

        private void AtualizarProduto(int idProduto, string nomeProduto, int stock, string descricaoProduto, decimal precoProduto, int codTipo)
        {
            // Conexão à base de dados
            string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Chama um procedimento armazenado na base de dados para atualizar o produto
                using (SqlCommand cmd = new SqlCommand("EditarProduto", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProduto", idProduto);
                    cmd.Parameters.AddWithValue("@NomeProduto", nomeProduto);
                    cmd.Parameters.AddWithValue("@Stock", stock);
                    cmd.Parameters.AddWithValue("@DescricaoProduto", descricaoProduto);
                    cmd.Parameters.AddWithValue("@PrecoProduto", precoProduto);
                    cmd.Parameters.AddWithValue("@CodTipo", codTipo);

                    // Executa a consulta
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void voltar_Click(object sender, EventArgs e)
        {
            // Redireciona de volta para a página de administração de produtos
            Response.Redirect("AdminProdutos.aspx");
        }
    }
}