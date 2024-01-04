using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;

namespace LojaaOnline
{
    public partial class Recuperarpw : System.Web.UI.Page
    {
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
            if (tb_nova_pw.Text.Length < 6)
                args.IsValid = false;
            else if (maiusculas.Matches(tb_nova_pw.Text).Count < 1)
                args.IsValid = false;
            else if (minusculas.Matches(tb_nova_pw.Text).Count < 1)
                args.IsValid = false;
            else if (numeros.Matches(tb_nova_pw.Text).Count < 1)
                args.IsValid = false;
            else if (especial.Matches(tb_nova_pw.Text).Count < 1)
                args.IsValid = false;
            else if (pelica.Matches(tb_nova_pw.Text).Count < 0)
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        // Método chamado no carregamento da página
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Método chamado ao clicar no botão de recuperação
        protected void btn_active_Click(object sender, EventArgs e)
        {
            // Obtém a string de conexão do arquivo de configuração
            string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("recuperarPW", con))
                {
                    // Configuração do comando para chamar a stored procedure
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", tb_email_recuperar.Text);
                    cmd.Parameters.AddWithValue("@pass", " ");
                    cmd.Parameters.AddWithValue("@func", 1);

                    // Parâmetro de retorno da stored procedure
                    SqlParameter valor = new SqlParameter
                    {
                        ParameterName = "@retorno",
                        Direction = ParameterDirection.Output,
                        SqlDbType = SqlDbType.Int
                    };

                    cmd.Parameters.Add(valor);

                    // Execução do comando
                    cmd.ExecuteNonQuery();

                    // Obtém o valor de retorno
                    int resposta = Convert.ToInt32(cmd.Parameters["@retorno"].Value);

                    lbl_mensagem_recuperar.Text = resposta.ToString();

                    if (resposta == 1)
                    {
                        lbl_mensagem_recuperar.Text = "Link de recuperação enviado para o seu email!!";

                        // Configuração do envio do email
                        MailMessage mail = new MailMessage();
                        SmtpClient servidor = new SmtpClient();

                        string smtpUtilizador = ConfigurationManager.AppSettings["SMTP_USER"];
                        string smtpPassword = ConfigurationManager.AppSettings["SMTP_PASS"];

                        servidor.Host = ConfigurationManager.AppSettings["SMTP_HOST"];
                        servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);

                        string siteURL = ConfigurationManager.AppSettings["SiteURL"];

                        // Configuração do remetente e destinatário
                        mail.From = new MailAddress(smtpUtilizador);
                        mail.To.Add(new MailAddress(tb_email_recuperar.Text));

                        mail.Subject = "Ação de utilizador - Recuperação da palavra-passe";
                        mail.IsBodyHtml = true;

                        // Corpo do email com link para a página de recuperação
                        mail.Body = $"Para recuperar a palavra-passe clique <a href='https://localhost:44366/Recuperarpw.aspx?email=" + EncryptString(tb_email_recuperar.Text) + "'>aqui</a>";

                        // Configuração das credenciais SMTP
                        servidor.Credentials = new NetworkCredential(smtpUtilizador, smtpPassword);
                        servidor.EnableSsl = true;

                        // Envio do email
                        servidor.Send(mail);
                    }
                }
            }
        }

        // Método chamado ao clicar no botão de guardar (concluir recuperação)
        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            // Validação da página
            Page.Validate();

            if (Page.IsValid)
            {
                // Obtém a string de conexão do arquivo de configuração
                string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    int resposta = 0;
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("recuperarPW", con))
                    {
                        // Configuração do comando para chamar a stored procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@email", DecryptString(Request.QueryString["email"]));
                        cmd.Parameters.AddWithValue("@pass", EncryptString(tb_nova_pw.Text));
                        cmd.Parameters.AddWithValue("@func", 2);

                        // Parâmetro de retorno da stored procedure
                        SqlParameter valor = new SqlParameter
                        {
                            ParameterName = "@retorno",
                            Direction = ParameterDirection.Output,
                            SqlDbType = SqlDbType.Int
                        };

                        cmd.Parameters.Add(valor);

                        // Execução do comando
                        cmd.ExecuteNonQuery();

                        resposta = Convert.ToInt32(cmd.Parameters["@retorno"].Value);
                    }
                }
            }

            // Exibe uma mensagem de sucesso e redireciona após 3 segundos
            lbl_mensagem_recuperar1.Text = "Palavra-passe alterada com sucesso!!";
            string script = "<meta http-equiv='refresh' content='3;url=Login.aspx'>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script);
        }

        // Método para encriptar uma string
        public static string EncryptString(string Message)
        {
            string Passphrase = "atec";
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(Message);

            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string
            string enc = Convert.ToBase64String(Results);
            enc = enc.Replace("+", "KKK");
            enc = enc.Replace("/", "JJJ");
            enc = enc.Replace("\\", "III");
            return enc;
        }

        // Método para desencriptar uma string
        public static string DecryptString(string Message)
        {
            string Passphrase = "atec";
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            Message = Message.Replace("KKK", "+");
            Message = Message.Replace("JJJ", "/");
            Message = Message.Replace("III", "\\");

            byte[] DataToDecrypt = Convert.FromBase64String(Message);

            // Step 5. Attempt to decrypt the string
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the decrypted string in UTF8 format
            return UTF8.GetString(Results);
        }
    }
}