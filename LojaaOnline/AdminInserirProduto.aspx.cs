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
    public partial class AdminInserirProduto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_inserir_Click(object sender, EventArgs e)
        {
            // Obter os detalhes da imagem a ser inserida
            Stream imgStream = FileUpload1.PostedFile.InputStream;
            int imgTamanho = FileUpload1.PostedFile.ContentLength;
            string contenttype = FileUpload1.PostedFile.ContentType;
            byte[] imgBinary = new byte[imgTamanho];

            // Ler os bytes da imagem
            imgStream.Read(imgBinary, 0, imgTamanho);
            try
            {
                // Verificar se todos os campos obrigatórios estão preenchidos
                if (string.IsNullOrWhiteSpace(tb_nome_produto.Text) ||
                    string.IsNullOrWhiteSpace(tb_stock.Text) ||
                    string.IsNullOrWhiteSpace(tb_descricao_produto.Text) ||
                    string.IsNullOrWhiteSpace(tb_preco.Text))
                {
                    // Se algum campo obrigatório estiver em branco, definir a mensagem de erro
                    lbl_mensagem_erro.Text = "Todos os campos são obrigatórios. Por favor, preencha todos os campos.";
                    return; // Sair da função para evitar a execução da inserção do produto
                }

                

                // Verificar se o valor digitado para o stock é um número válido e maior que zero
                if (!int.TryParse(tb_stock.Text, out int stock) || stock <= 0)
                {
                    lbl_mensagem_erro.Text = "O valor do stock deve ser um número válido e maior que zero.";
                    return; // Sair da função para evitar a execução da inserção do produto
                }


                // Verificar se o valor digitado para o stock é um número válido
                if (int.TryParse(tb_stock.Text, out stock))
                {
                    // Configurar a conexão com a base de dados
                    SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);
                    SqlCommand myCommand = new SqlCommand();
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandText = "inserir_produto";
                    myCommand.Connection = myConn;

                    // Adicionar parâmetros para a stored procedure
                    myCommand.Parameters.AddWithValue("@nome_produto", tb_nome_produto.Text);
                    myCommand.Parameters.AddWithValue("@stock", stock);
                    myCommand.Parameters.AddWithValue("@descricao", tb_descricao_produto.Text);
                    myCommand.Parameters.AddWithValue("@preco", tb_preco.Text);
                    myCommand.Parameters.AddWithValue("@ct", contenttype);
                    myCommand.Parameters.AddWithValue("@binarios", imgBinary);
                    myCommand.Parameters.AddWithValue("@codTipo", ddl_tipo.SelectedValue);

                    // Abrir a conexão com a base de dados e executar a stored procedure
                    myConn.Open();
                    myCommand.ExecuteNonQuery();
                    myConn.Close();

                    // Limpar os campos do formulário após a inserção bem-sucedida
                    tb_nome_produto.Text = "";
                    tb_stock.Text = "";
                    ddl_tipo.SelectedIndex = 0;
                    tb_descricao_produto.Text = "";
                    tb_preco.Text = "";

                    // Exibir mensagem de sucesso
                    lbl_mensagem_erro.Visible = false;
                    lbl_mensagem.Text = "Produto inserido com sucesso!!";
                }               
            }
            catch (Exception ex)
            {
                // Se ocorrer uma exceção, definir a mensagem de erro
                lbl_mensagem_erro.Text = $"Erro ao inserir produto: {ex.Message}";
            }
        }
    }
}