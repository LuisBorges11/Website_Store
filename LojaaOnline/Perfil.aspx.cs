using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace LojaaOnline
{
    public partial class Perfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Conectar à base de dados utilizando a string de conexão no arquivo Web.config
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

            // Criar um comando SQL armazenado para obter detalhes do utilizador
            SqlCommand myCommand = new SqlCommand();
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "detalhes_util";

            // Associar o comando à conexão
            myCommand.Connection = myConn;

            // Definir o parâmetro do utilizador com base na sessão
            myCommand.Parameters.AddWithValue("@util", Session["Util"]);

            // Abrir a conexão com a base de dados
            myConn.Open();

            // Executar o comando e obter um leitor de dados
            SqlDataReader dr = myCommand.ExecuteReader();

            // Verificar se há dados no leitor
            if (dr.Read())
            {
                // Preencher as etiquetas HTML com os detalhes do utilizador
                lbl_nome.Text = dr["utilizador"].ToString();
                lbl_email.Text = dr["email"].ToString();
                lbl_id.Text = dr["cod"].ToString();
            }

            // Fechar a conexão com a base de dados
            myConn.Close();
        }

        // Redirecionar para a página principal ao clicar no botão "Voltar"
        protected void btn_voltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Main.aspx");
        }

        // Redirecionar para a página de alteração de palavra-passe ao clicar no LinkButton "Alterar Palavra Passe"
        protected void lb_alterarPW_Click(object sender, EventArgs e)
        {
            Response.Redirect("Alterar_pw.aspx");
        }
    }
}