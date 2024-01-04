using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace LojaaOnline
{
    public partial class AdminInserirUtilizador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnInserirUtilizador_Click(object sender, EventArgs e)
        {
            // Obter valores dos campos do formulário
            string utilizador = tb_utilizador.Text;
            string email = tb_email.Text;
            string palavraPasse = tb_pw.Text;
            int codPerfil = Convert.ToInt32(ddlPerfil.SelectedValue);

            // Chamar o método para inserir utilizador na base de dados
            InserirUtilizador(utilizador, email, palavraPasse, codPerfil);

            // Limpar os campos do formulário após a inserção bem-sucedida
            LimparCampos();
        }

        private void InserirUtilizador(string utilizador, string email, string palavraPasse, int codPerfil)
        {
            try
            {
                // Verificar se os campos obrigatórios não estão em branco
                if (string.IsNullOrWhiteSpace(utilizador) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(palavraPasse))
                {
                    // Se algum campo estiver em branco, definir a mensagem de erro
                    lbl_mensagem_erro.Text = "Por favor, preencha todos os campos obrigatórios.";
                    return; // Sair da função para evitar a execução da stored procedure
                }

                // Verificar se o formato do endereço de email é válido
                if (!IsValidEmail(email))
                {
                    // Se o formato do email não for válido, definir a mensagem de erro
                    lbl_mensagem_erro.Text = "Por favor, insira um endereço de email válido.";
                    return; // Sair da função para evitar a execução da stored procedure
                }

                // Obter a string de conexão do arquivo de configuração
                string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

                // Usar blocos 'using' para garantir a liberação adequada dos recursos
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Abrir a conexão com a base de dados
                    con.Open();

                    // Criar um comando SQL para chamar a stored procedure
                    using (SqlCommand cmd = new SqlCommand("inserir_utilizador", con))
                    {
                        // Especificar que é uma stored procedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Adicionar parâmetros para a stored procedure
                        cmd.Parameters.AddWithValue("@utilizador", utilizador);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@palavra_passe", palavraPasse);
                        cmd.Parameters.AddWithValue("@ativo", true);
                        cmd.Parameters.AddWithValue("@data", DateTime.Now);
                        cmd.Parameters.AddWithValue("@cod_perfil", codPerfil);

                        // Executar a stored procedure
                        cmd.ExecuteNonQuery();

                        lbl_mensagem_erro.Visible = false;
                        lbl_mensagem.Text = "Utilizador inserido com sucesso!!";
                    }
                }
            }
            catch (Exception ex)
            {
                // Se ocorrer uma exceção, definir a mensagem de erro
                lbl_mensagem_erro.Text = $"Erro ao inserir utilizador: {ex.Message}";
            }
        }

        private bool IsValidEmail(string email)
        {
            // Padrão de expressão regular para validar endereços de email
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
        }

        private void LimparCampos()
        {
            // Limpar os campos do formulário
            tb_utilizador.Text = string.Empty;
            tb_email.Text = string.Empty;
            tb_pw.Text = string.Empty;
            ddlPerfil.SelectedIndex = 0; // Reiniciar a seleção do DropDownList
        }
    }
}