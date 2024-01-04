using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net.Mail;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net;

namespace LojaaOnline
{
    public partial class Pedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica se a página está a ser carregada pela primeira vez
            if (!IsPostBack)
            {
                int idUtilizador;

                // Verifica se a sessão contém o ID do utilizador e se é um número válido
                if (Session["IDUtilizador"] != null && int.TryParse(Session["IDUtilizador"].ToString(), out idUtilizador))
                {
                    // String de conexão do arquivo de configuração
                    string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

                    // Cria uma conexão com a base de dados utilizando a string de conexão
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        // Executa uma consulta para obter os pedidos do utilizador
                        using (SqlCommand cmd = new SqlCommand("SELECT * FROM pedidos WHERE cod = @idUtilizador", con))
                        {
                            cmd.Parameters.AddWithValue("@idUtilizador", idUtilizador);

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();

                                // Preenche o DataTable com os resultados da consulta
                                da.Fill(dt);

                                // Associa o DataTable ao Repeater para exibição dos pedidos
                                repeaterPedidos.DataSource = dt;
                                repeaterPedidos.DataBind();
                            }
                        }
                    }
                }
                // Mostra uma mensagem de erro caso o utilizador não tiver pedidos
                else
                {
                    lbl_mensagem.Text = "Sem pedidos!";
                }
            }
        }

        // Método para obter o nome do produto com base no código do produto
        protected string GetNomeProduto(object codProduto)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT nome_produto FROM produtos WHERE cod_produto = @cod_produto", con))
                {
                    cmd.Parameters.AddWithValue("@cod_produto", codProduto);

                    con.Open();
                    string nomeProduto = cmd.ExecuteScalar() as string;

                    return nomeProduto;
                }
            }
        }

        // Método para obter o estilo CSS com base no status do pedido
        protected string GetStatusStyle(object statusPedido)
        {
            if (statusPedido != null)
            {
                string status = statusPedido.ToString();

                if (status == "Pendente")
                {
                    return "color: orange;";
                }
                else if (status == "Confirmado")
                {
                    return "color: green;";
                }
            }

            return string.Empty;
        }

        // Método para verificar se o status do pedido é "Confirmado"
        protected bool IsStatusConfirmado(object status)
        {
            if (status != null)
            {
                return status.ToString() == "Confirmado";
            }

            return false;
        }

        // Manipula o evento de comando do botão "PDF" para gerar e enviar o PDF por email
        protected void GerarPDF_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "GerarPDF")
            {
                int idPedido = Convert.ToInt32(e.CommandArgument);

                // Cria uma conexão com a base de dados utilizando a string de conexão
                using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString))
                {
                    myConn.Open();

                    string status = GetStatusPedido(idPedido);

                    if (status == "Confirmado")
                    {
                        // Gera o PDF do pedido
                        byte[] pdfData = GerarPDFPedido(idPedido, myConn);

                        // Envia o PDF por email
                        EnviarPDFPorEmail(pdfData, idPedido);
                    }
                    else
                    {
                        lbl_mensagem.Text = "O pedido ainda não está confirmado para gerar um PDF.";
                    }
                }
            }
        }

        // Obtém o status do pedido com base no ID do pedido
        private string GetStatusPedido(int idPedido)
        {
            string status = string.Empty;

            string connectionString = ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT StatusPedido FROM pedidos WHERE id_pedido = @idPedido", con))
                {
                    cmd.Parameters.AddWithValue("@idPedido", idPedido);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            status = reader["StatusPedido"].ToString();
                        }
                    }
                }
            }

            return status;
        }

        // Gera o PDF do pedido com base no ID do pedido e na conexão da base de dados
        private byte[] GerarPDFPedido(int idPedido, SqlConnection myConn)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document document = new Document())
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, ms);

                    // Configurações do documento PDF
                    document.AddTitle("Detalhes do Pedido");
                    document.AddAuthor("CLA store");

                    document.Open();

                    // cabeçalho ao PDF
                    PdfPTable headerTable = new PdfPTable(1);
                    headerTable.WidthPercentage = 100;
                    PdfPCell headerCell = new PdfPCell(new Phrase("CLA store", new Font(Font.FontFamily.HELVETICA, 24, Font.BOLD)));
                    headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    headerCell.Border = PdfPCell.NO_BORDER;
                    headerTable.AddCell(headerCell);
                    document.Add(headerTable);

                    // espaço
                    document.Add(new Paragraph("\n"));

                    // tabela com detalhes do pedido
                    PdfPTable table = new PdfPTable(2);
                    table.WidthPercentage = 100;
                    table.DefaultCell.Border = PdfPCell.BOTTOM_BORDER;

                    // célula com o título "Detalhes do Pedido"
                    PdfPCell cell = new PdfPCell(new Phrase("Detalhes do Pedido", new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD)));
                    cell.Colspan = 2;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Border = PdfPCell.NO_BORDER;
                    table.AddCell(cell);

                    // célula com o ID do Pedido
                    cell = new PdfPCell(new Phrase("ID do Pedido: " + idPedido, new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL)));
                    cell.Colspan = 2;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Border = PdfPCell.NO_BORDER;
                    table.AddCell(cell);

                    // espaço
                    document.Add(new Paragraph("\n"));

                    // detalhes do pedido à tabela
                    using (SqlCommand cmd = new SqlCommand("ObterDetalhesPedido", myConn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idPedido", idPedido);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            foreach (DataRow row in dt.Rows)
                            {
                                // informações sobre a data do pedido, morada, telemóvel e código postal                              
                                table.AddCell("Data do Pedido:");
                                table.AddCell(row["datapedido"]?.ToString());

                                table.AddCell("Morada:");
                                table.AddCell(row["morada"]?.ToString());

                                table.AddCell("Telemovel:");
                                table.AddCell(row["telemovel"]?.ToString());

                                table.AddCell("Código Postal:");
                                table.AddCell(row["codigopostal"]?.ToString());
                            }
                        }
                    }

                    // Adiciona a tabela ao documento PDF
                    document.Add(table);

                    // espaço
                    document.Add(new Paragraph("\n"));

                    // tabela com detalhes do produto
                    PdfPTable table2 = new PdfPTable(2);
                    table2.WidthPercentage = 100;
                    table2.DefaultCell.Border = PdfPCell.BOTTOM_BORDER;

                    // célula com o título "Detalhes do produto"
                    PdfPCell cell2 = new PdfPCell(new Phrase("Produto", new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD)));
                    cell2.Colspan = 2;
                    cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell2.Border = PdfPCell.NO_BORDER;
                    table2.AddCell(cell2);                 

                    // espaço
                    document.Add(new Paragraph("\n"));

                    // detalhes do produto à tabela
                    using (SqlCommand cmd = new SqlCommand("ObterDetalhesPedido", myConn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idPedido", idPedido);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            foreach (DataRow row in dt.Rows)
                            {
                                // informações sobre o produto, quantidade e preço total
                                table2.AddCell("Nome do Produto:");
                                table2.AddCell(row["nome_produto"]?.ToString());

                                table2.AddCell("Quantidade:");
                                table2.AddCell(row["quantidade"]?.ToString());

                                table2.AddCell("Preço Total:");
                                table2.AddCell(row["precototal"]?.ToString() + "€");                              
                            }
                        }
                    }

                    // Adiciona a tabela ao documento PDF
                    document.Add(table2);

                    // espaço
                    document.Add(new Paragraph("\n"));

                    // Adiciona assinatura ao PDF
                    PdfPTable signatureTable = new PdfPTable(1);
                    signatureTable.WidthPercentage = 100;
                    PdfPCell signatureCell = new PdfPCell(new Phrase("__________________________\nAssinatura", new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL)));
                    signatureCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    signatureCell.Border = PdfPCell.NO_BORDER;
                    signatureTable.AddCell(signatureCell);
                    document.Add(signatureTable);

                    // Fecha o documento PDF
                    document.Close();
                    writer.Close();
                }

                // Retorna os dados do PDF em formato de array de bytes
                return ms.ToArray();
            }
        }

        // Envia o PDF por email
        private void EnviarPDFPorEmail(byte[] pdfData, int idPedido)
        {
            // Obtém o endereço de email do destinatário da sessão
            string destinatario = Session["Email"]?.ToString();
            string assunto = "Pedido #" + idPedido.ToString();
            string corpoEmail = "Por favor, encontre em anexo o PDF do seu pedido.";

            // Verifica se o endereço de email é válido
            if (string.IsNullOrEmpty(destinatario))
            {
                lbl_mensagem.Text = "Endereço de e-mail do destinatário não encontrado na sessão.";
                return;
            }

            // Obtém as credenciais do servidor SMTP do arquivo de configuração
            string smtpUtilizador = ConfigurationManager.AppSettings["SMTP_USER"];
            string smtpPassword = ConfigurationManager.AppSettings["SMTP_PASS"];

            // Configurações do servidor SMTP
            using (SmtpClient servidor = new SmtpClient())
            {
                servidor.Host = ConfigurationManager.AppSettings["SMTP_HOST"];
                servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);

                // Cria a mensagem de email
                using (MailMessage mailMessage = new MailMessage(smtpUtilizador, destinatario))
                {
                    mailMessage.Subject = assunto;
                    mailMessage.Body = corpoEmail;

                    try
                    {
                        // Converte os dados do PDF para um MemoryStream
                        MemoryStream ms = new MemoryStream(pdfData);

                        // Adiciona o PDF como anexo ao email
                        mailMessage.Attachments.Add(new Attachment(ms, "Pedido" + idPedido + ".pdf"));

                        // credenciais e SSL para o servidor SMTP
                        servidor.Credentials = new NetworkCredential(smtpUtilizador, smtpPassword);
                        servidor.EnableSsl = true;

                        // Envia o email
                        servidor.Send(mailMessage);
                        lbl_mensagem2.Text = "E-mail enviado com sucesso!";
                    }
                    catch (Exception ex)
                    {
                        // Mostra uma mensagem de erro em caso de falha no envio do email
                        lbl_mensagem.Text = "Erro ao enviar o e-mail: " + ex.ToString();
                    }
                }
            }
        }
    }
}