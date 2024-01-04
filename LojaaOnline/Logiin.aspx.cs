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

namespace LojaaOnline
{
    public partial class Logiin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btn_registrar_Click(object sender, EventArgs e)
        {
            // Redireciona para a página de registro
            Response.Redirect("Regiistrar.aspx");
        }

        protected void btn_entrar_Click(object sender, EventArgs e)
        {
            // Conexão com a base de dados
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

            // Comando SQL
            SqlCommand myCommand = new SqlCommand();
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "login";
            myCommand.Connection = myConn;

            // Parâmetros do comando
            myCommand.Parameters.AddWithValue("@util", tb_nome.Text);
            myCommand.Parameters.AddWithValue("@pw", EncryptString(tb_pw.Text));

            // Parâmetros de saída
            SqlParameter valor = new SqlParameter();
            valor.ParameterName = "@retorno";
            valor.Direction = ParameterDirection.Output;
            valor.SqlDbType = SqlDbType.Int;
            myCommand.Parameters.Add(valor);

            SqlParameter valor2 = new SqlParameter();
            valor2.ParameterName = "@retorno_perfil";
            valor2.Direction = ParameterDirection.Output;
            valor2.SqlDbType = SqlDbType.VarChar;
            valor2.Size = 50;
            myCommand.Parameters.Add(valor2);

            SqlParameter codUtilizador = new SqlParameter("@cod_utilizador", SqlDbType.Int);
            codUtilizador.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(codUtilizador);

            // Abre a conexão e executa o comando
            myConn.Open();
            myCommand.ExecuteNonQuery();

            // Obtém os resultados dos parâmetros de saída
            int resposta = Convert.ToInt32(myCommand.Parameters["@retorno"].Value);
            string resposta_perfil = myCommand.Parameters["@retorno_perfil"].Value.ToString();

            myConn.Close();

            // Lógica de autenticação
            if (resposta == 1)
            {
                int idUtilizador = Convert.ToInt32(myCommand.Parameters["@cod_utilizador"].Value);
                Session["IDUtilizador"] = idUtilizador;

                // Obtém o email do utilizador
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT email FROM utilizadores WHERE cod = @cod_utilizador", con))
                    {
                        cmd.Parameters.AddWithValue("@cod_utilizador", idUtilizador);
                        Session["Email"] = cmd.ExecuteScalar().ToString();
                    }
                }

                // Redireciona com base no perfil
                if (resposta_perfil == "Administrador")
                {
                    Session["logado"] = "sim";
                    Session["Util"] = tb_nome.Text;
                    Response.Redirect("AdminMain.aspx");
                }
                else if (resposta_perfil == "Revenda" || resposta_perfil == "Cliente")
                {
                    Session["logado"] = "sim";
                    Session["Util"] = tb_nome.Text;
                    Response.Redirect("Main.aspx");
                }
                else
                {
                    lbl_mensagem.Text = "Perfil desconhecido";
                }

                Session["perfil"] = resposta_perfil;
                Session["logado"] = "sim";
            }
            else if (resposta == 2)
            {
                lbl_mensagem.Text = "Utilizador inativo";
            }
            else
            {
                lbl_mensagem.Text = "Utilizador e/ou palavra-passe errados";
            }
        }

        // Método para criptografar uma string
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