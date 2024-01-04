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
    public partial class Alterar_pw : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btn_alterar_Click(object sender, EventArgs e)
        {
            // Estabelece uma conexão com a base de dados
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

            // Configura um comando SQL armazenado no procedimento armazenado "alterarPW"
            SqlCommand myCommand = new SqlCommand();
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "alterarPW";
            myCommand.Connection = myConn;

            // Parâmetros do comando SQL
            myCommand.Parameters.AddWithValue("@util", Session["util"]);
            myCommand.Parameters.AddWithValue("@pw_atual", EncryptString(tb_pw_atual.Text));
            myCommand.Parameters.AddWithValue("@pw_nova", EncryptString(tb_pw_nova.Text));

            // Parâmetro de retorno
            SqlParameter valor = new SqlParameter();
            valor.ParameterName = "@retorno";
            valor.Direction = ParameterDirection.Output;
            valor.SqlDbType = SqlDbType.Int;
            myCommand.Parameters.Add(valor);

            // Abre a conexão e executa o comando SQL
            myConn.Open();
            myCommand.ExecuteNonQuery();

            // Obtém o valor do parâmetro de retorno
            int resposta = Convert.ToInt32(myCommand.Parameters["@retorno"].Value);

            // Fecha a conexão
            myConn.Close();

            // Mostra a mensagem com base no resultado
            if (resposta == 0)
                lbl_mensagem.Text = "Utilizador e/ou palavra-passe errados !!!";
            else
                lbl_mensagem.Text = "Palavra-passe alterada com sucesso!!!";
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

        protected void btn_voltar_Click(object sender, EventArgs e)
        {
            // Redireciona para a página "Perfil.aspx"
            Response.Redirect("Perfil.aspx");
        }
    }
}