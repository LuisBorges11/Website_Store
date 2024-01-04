using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace LojaaOnline
{
    public partial class ativacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Obter o nome de utilizador descriptografando o parâmetro da consulta
            string user = DecryptString(Request.QueryString["utilizador"]);

            // Mostra uma mensagem a indicar que a conta foi ativada com sucesso
            lbl_mensagem.Text = $"{user}, a sua conta foi ativada com sucesso!!";

            // Conexao a base de dados e chamar uma stored procedure para ativar a conta
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);
            SqlCommand myCommand = new SqlCommand();
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "ativar_conta";
            myCommand.Connection = myConn;
            myCommand.Parameters.AddWithValue("@util", user);

            // Abre a conexão, executa a stored procedure e fecha a conexão
            myConn.Open();
            myCommand.ExecuteNonQuery();
            myConn.Close();
        }

        // Método para descriptografar uma string
        public static string DecryptString(string Message)
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

           
            Message = Message.Replace("KKK", "+");
            Message = Message.Replace("JJJ", "/");
            Message = Message.Replace("III", "\\");

            byte[] DataToDecrypt = Convert.FromBase64String(Message);

            try
            {
         
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
           
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

       
            return UTF8.GetString(Results);
        }
    }
}