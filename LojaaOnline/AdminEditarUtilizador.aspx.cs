using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LojaaOnline
{
    public partial class AdminEditarUtilizador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica se a página está a ser carregada pela primeira vez
            if (!IsPostBack)
            {
                // Carrega os detalhes do utilizador apenas no primeiro carregamento da página
                CarregarDetalhesUtilizador();
            }
        }

        private void CarregarDetalhesUtilizador()
        {
            // Verifica se o parâmetro idUtilizador foi passado na URL
            if (Request.QueryString["idUtilizador"] != null)
            {
                int idUtilizador;
                // Tenta converter o idUtilizador para um número inteiro
                if (int.TryParse(Request.QueryString["idUtilizador"], out idUtilizador))
                {
                    // Conexão à base de dados
                    string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        // Stored procedure para obter os detalhes do utilizador com base no idUtilizador
                        using (SqlCommand cmd = new SqlCommand("SELECT * FROM utilizadores WHERE cod = @IdUtilizador", con))
                        {
                            cmd.Parameters.AddWithValue("@IdUtilizador", idUtilizador);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Preenche os controles na página com os detalhes do utilizador
                                    lblIdUtilizador.Text = reader["cod"].ToString();
                                    txtNome.Text = reader["utilizador"].ToString();
                                    txtEmail.Text = reader["email"].ToString();
                                    ddlPerfil.SelectedValue = reader["cod_perfil"].ToString();
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void btnEditarUtilizador_Click(object sender, EventArgs e)
        {
            // Obtém os valores dos controles na página
            int idUtilizador = Convert.ToInt32(lblIdUtilizador.Text);
            string nomeUtilizador = txtNome.Text;
            string email = txtEmail.Text;
            int codPerfil = Convert.ToInt32(ddlPerfil.SelectedValue);

            // Atualiza as informações do utilizador
            AtualizarUtilizador(idUtilizador, nomeUtilizador, email, codPerfil);

            // Mostra uma mensagem de sucesso e redireciona para a página de administração de utilizadores após 3 segundos
            lbl_mensagem.Text = "Utilizador editado com sucesso!";
            string script = "<meta http-equiv='refresh' content='3;url=AdminUsers.aspx'>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script);
        }

        private void AtualizarUtilizador(int idUtilizador, string nomeUtilizador, string email, int codPerfil)
        {
            // Conexão à base de dados
            string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Chama um procedimento armazenado no banco de dados para atualizar o utilizador
                using (SqlCommand cmd = new SqlCommand("EditarUtilizador", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUtilizador", idUtilizador);
                    cmd.Parameters.AddWithValue("@NomeUtilizador", nomeUtilizador);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@CodPerfil", codPerfil);

                    // Executa a consulta
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void voltar_Click(object sender, EventArgs e)
        {
            // Redireciona de volta para a página de administração de utilizadores
            Response.Redirect("AdminUsers.aspx");
        }
    }
}