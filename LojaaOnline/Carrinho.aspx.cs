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
    public partial class Carrinho : System.Web.UI.Page
    {
        decimal totalCarrinho = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Adicionando manipulador de eventos para o evento de ligação de dados do Repeater
            repeaterDetalhesProduto.ItemDataBound += new RepeaterItemEventHandler(repeaterDetalhesProduto_ItemDataBound);

            // Verifica se a página está A ser carregada pela primeira vez
            if (!IsPostBack)
            {
                // Verifica se o ID do utilizador está presente na sessão
                if (Session["IDUtilizador"] != null)
                {
                    int idUtilizador;
                    // Tenta converter o ID do utilizador para um valor inteiro
                    if (int.TryParse(Session["IDUtilizador"].ToString(), out idUtilizador))
                    {
                        // Obtém a string de conexão do arquivo de configuração
                        string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

                        // Usa uma instrução "using" para garantir que os recursos são liberados corretamente
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            // Usa uma instrução "using" para garantir que os recursos são liberados corretamente
                            using (SqlCommand cmd = new SqlCommand("ProdutosCarrinho", con))
                            {
                                // Especifica que o comando é um procedimento armazenado
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@cod_utilizador", idUtilizador);

                                // Abre a conexão
                                con.Open();
                                // Executa o comando e obtém um leitor de dados
                                SqlDataReader dr = cmd.ExecuteReader();

                                // Verifica se há linhas no leitor de dados
                                if (dr.HasRows)
                                {
                                    // Liga os dados ao Repeater se houver linhas
                                    repeaterDetalhesProduto.DataSource = dr;
                                    repeaterDetalhesProduto.DataBind();
                                }
                                else
                                {
                                    // Exibe uma mensagem se não houver produtos no carrinho
                                    lbl_mensagem.Text = "Ainda sem produtos no carrinho!";
                                }
                            }
                        }
                    }
                }
                else
                {
                    // Exibe uma mensagem se o utilizador não estiver autenticado
                    lbl_mensagem.Text = "Precisa de fazer login para ver o seu carrinho!";
                }

                // Exibe o total do carrinho em euros
                lblTotalCarrinho.Text = $"{totalCarrinho:C}";
            }
        }

        protected void EliminarProduto(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                // Obtém o ID do carrinho a partir do argumento do comando
                int idCarrinho = Convert.ToInt32(e.CommandArgument);

                // Obtém a string de conexão do arquivo de configuração
                string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

                // Usa uma instrução "using" para garantir que os recursos são liberados corretamente
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Usa uma instrução "using" para garantir que os recursos são liberados corretamente
                    using (SqlCommand cmd = new SqlCommand("RemoverProdutoDoCarrinho", con))
                    {
                        // Especifica que o comando é um procedimento armazenado
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_carrinho", idCarrinho);

                        // Executa o comando para remover o produto do carrinho
                        cmd.ExecuteNonQuery();
                    }
                }

                // Redireciona de volta para a página do carrinho após a remoção do produto
                Response.Redirect("Carrinho.aspx");
                // Exibe uma mensagem indicando que o produto foi removido com sucesso
                lblProdutoEliminado.Visible = true;
                lblProdutoEliminado.Text = "Produto eliminado com sucesso!";                                            
            }
        }

        protected void repeaterDetalhesProduto_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Obtém os valores das colunas relevantes do Repeater
                decimal precoProduto = Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "preco_produto"));
                int quantidade = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "quantidade"));
                int codPerfil = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "cod_perfil"));

                if (codPerfil == 2)
                {
                    // Aplica desconto de 20% se o perfil for 2
                    decimal desconto = precoProduto * 0.2m;
                    decimal precoComDesconto = precoProduto - desconto;

                    // Calcula o preço total do produto com desconto com base na quantidade atual
                    decimal totalProdutoComDesconto = precoComDesconto * quantidade;

                    // Obtém referências aos controles de rótulo no Repeater
                    Label lblTotalProduto = (Label)e.Item.FindControl("lblTotalProduto");
                    Label lblprecoproduto = (Label)e.Item.FindControl("lblprecoproduto");

                    // Exibe o preço total do produto com desconto e o preço com desconto formatado
                    lblTotalProduto.Text = $"Total: {totalProdutoComDesconto:C}";
                    lblprecoproduto.Text = $"<del>{precoProduto:C}</del><br/>{precoComDesconto:C}<br/>";

                    // Atualiza o total do carrinho com desconto
                    totalCarrinho += totalProdutoComDesconto;
                }
                else
                {
                    // Se não for perfil 2, não há desconto
                    // Calcula o preço total do produto sem desconto com base na quantidade atual
                    decimal totalProdutoSemDesconto = precoProduto * quantidade;

                    // Obtém referências aos controles de rótulo no Repeater
                    Label lblTotalProduto = (Label)e.Item.FindControl("lblTotalProduto");
                    Label lblprecoproduto = (Label)e.Item.FindControl("lblprecoproduto");

                    // Exibe o preço total do produto sem desconto e o preço sem desconto formatado
                    lblTotalProduto.Text = $"{totalProdutoSemDesconto:C}";
                    lblprecoproduto.Text = $"{precoProduto:C}";

                    // Atualiza o total do carrinho sem desconto
                    totalCarrinho += totalProdutoSemDesconto;
                }
            }
        }

        protected void AumentarQuantidade(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Aumentar")
            {
                // Obtém o ID do carrinho a partir do argumento do comando
                int idCarrinho = Convert.ToInt32(e.CommandArgument);
                int idUtilizador = Convert.ToInt32(Session["IDUtilizador"]);

                // Obtém a string de conexão do arquivo de configuração
                string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

                // Usa uma instrução "using" para garantir que os recursos são liberados corretamente
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Usa uma instrução "using" para garantir que os recursos são liberados corretamente
                    using (SqlCommand cmd = new SqlCommand("AumentarQuantidadeNoCarrinho", con))
                    {
                        // Especifica que o comando é um procedimento armazenado
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_carrinho", idCarrinho);

                        // Executa o comando para aumentar a quantidade do produto no carrinho
                        cmd.ExecuteNonQuery();
                    }
                }

                // Redireciona de volta para a página do carrinho após aumentar a quantidade
                Response.Redirect("Carrinho.aspx");
            }
        }

        protected void DiminuirQuantidade(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Diminuir")
            {
                // Obtém o ID do carrinho a partir do argumento do comando
                int idCarrinho = Convert.ToInt32(e.CommandArgument);
                int idUtilizador = Convert.ToInt32(Session["IDUtilizador"]);

                // Obtém a string de conexão do arquivo de configuração
                string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

                // Usa uma instrução "using" para garantir que os recursos são liberados corretamente
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Usa uma instrução "using" para garantir que os recursos são liberados corretamente
                    using (SqlCommand cmd = new SqlCommand("DiminuirQuantidadeNoCarrinho", con))
                    {
                        // Especifica que o comando é um procedimento armazenado
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_carrinho", idCarrinho);

                        // Executa o comando para diminuir a quantidade do produto no carrinho
                        cmd.ExecuteNonQuery();
                    }
                }

                // Redireciona de volta para a página do carrinho após diminuir a quantidade
                Response.Redirect("Carrinho.aspx");
            }
        }

        protected void btn_checkout_Click(object sender, EventArgs e)
        {
            // Redireciona para a página de checkout ao clicar no botão de checkout
            Response.Redirect("CheckOut.aspx");
        }
    }
}