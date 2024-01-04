using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace LojaaOnline
{
    public partial class Regiistrar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }


        // Função para validar a força da palavra-passe durante a recuperação
        protected void ValidatePassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // Expressões regulares para verificar a presença de maiúsculas, minúsculas, números, caracteres especiais e plicas
            Regex maiusculas = new Regex("[A-Z]");
            Regex minusculas = new Regex("[a-z]");
            Regex numeros = new Regex("[0-9]");
            Regex especial = new Regex("[^a-zA-Z0-9]");
            Regex pelica = new Regex("'");

            // Condições para validar a força da palavra-passe
            if (tb_pw.Text.Length < 6)
                args.IsValid = false;
            else if (maiusculas.Matches(tb_pw.Text).Count < 1)
                args.IsValid = false;
            else if (minusculas.Matches(tb_pw.Text).Count < 1)
                args.IsValid = false;
            else if (numeros.Matches(tb_pw.Text).Count < 1)
                args.IsValid = false;
            else if (especial.Matches(tb_pw.Text).Count < 1)
                args.IsValid = false;
            else if (pelica.Matches(tb_pw.Text).Count < 0)
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        protected void btn_registrar_Click(object sender, EventArgs e)
        {

            // Verifica se todos os campos estão preenchidos
            if (string.IsNullOrWhiteSpace(tb_nome.Text) ||
            string.IsNullOrWhiteSpace(tb_pw.Text) ||
            string.IsNullOrWhiteSpace(tb_email.Text))
            {
                lbl_mensagem.Text = "Todos os campos são obrigatórios.";
                return;
            }

            // Validação da página
            Page.Validate();         

            if (Page.IsValid)
            {                   

                // Conexão a base de dados
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

                // Comando SQL para chamar a stored procedure "inserirumutilizador"
                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "inserirumutilizador";
                myCommand.Connection = myConn;

                // Parâmetros da stored procedure
                myCommand.Parameters.AddWithValue("@util", tb_nome.Text);
                myCommand.Parameters.AddWithValue("@pw", EncryptString(tb_pw.Text));
                myCommand.Parameters.AddWithValue("@email", tb_email.Text);
                myCommand.Parameters.AddWithValue("@codPerfil", ddl_perfil.SelectedValue);

                // Parâmetro de saída para obter o resultado da stored procedure
                SqlParameter valor = new SqlParameter();
                valor.ParameterName = "@retorno";
                valor.Direction = ParameterDirection.Output;
                valor.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(valor);

                // Abre a conexão e executa o comando
                myConn.Open();
                myCommand.ExecuteNonQuery();

                // resposta da stored procedure
                int resposta = Convert.ToInt32(myCommand.Parameters["@retorno"].Value);

                // Fecha a conexão
                myConn.Close();
            
                // Verifica a resposta e mostra a mensagem apropriada
                if (resposta == 1)
                {
                    lbl_mensagem.Text = "Utilizador inserido com sucesso! (Precisa ativar a sua conta por email)";

                    // Configuração para o envio do email
                    String smtpUtilizador = ConfigurationManager.AppSettings["SMTP_USER"];
                    String smtpPassword = ConfigurationManager.AppSettings["SMTP_PASS"];

                    MailMessage mail = new MailMessage();
                    SmtpClient servidor = new SmtpClient();

                    mail.From = new MailAddress(smtpUtilizador);
                    mail.To.Add(new MailAddress(tb_email.Text));
                    mail.Subject = "Registo de utilizador - Ativação de conta";
                    mail.IsBodyHtml = true;

                    // Corpo do email com link da ativação da conta
                    mail.Body = "Utilizador criado com sucesso!!, para ativar a sua conta clique <a href='https://localhost:44366/ativacao.aspx?utilizador=" + EncryptString(tb_nome.Text) + "'>aqui!</a>";

                    // Configuração do servidor SMTP
                    servidor.Host = ConfigurationManager.AppSettings["SMTP_HOST"];
                    servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);
                    servidor.Credentials = new NetworkCredential(smtpUtilizador, smtpPassword);
                    servidor.EnableSsl = true;

                    // Envia o email
                    servidor.Send(mail);
                }
                else
                {
                    lbl_mensagem.Text = "Utilizador e/ou endereço de email já inseridos!";
                }
            }
        }

        protected void btn_voltar_Click(object sender, EventArgs e)
        {
            // Redireciona para a página de login
            Response.Redirect("Logiin.aspx");
        }

        
        public static string EncryptString(string Message)
        {
            string Passphrase = "atec";
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            byte[] DataToEncrypt = UTF8.GetBytes(Message);

            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            string enc = Convert.ToBase64String(Results);
            enc = enc.Replace("+", "KKK");
            enc = enc.Replace("/", "JJJ");
            enc = enc.Replace("\\", "III");
            return enc;
        }
    }
}