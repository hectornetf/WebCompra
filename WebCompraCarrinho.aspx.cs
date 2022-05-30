using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCompra.Logic;
using WebCompra.Models;
using System.Collections;
using System.Web.ModelBinding;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Net.Mail;
using System.Net;

namespace WebCompra
{
    public partial class WebCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)

            {

                CompraLista.DataBind();

            }

            using (WebCompraAct userWebCompra = new WebCompraAct())
            {

                decimal compraTotal = 0;
                compraTotal = userWebCompra.GetTotal();
                if(compraTotal > 0)
                {
                    //Total
                    lblTotal.Text = String.Format("{0:c}", compraTotal);
                }
                else
                {
                    LabelTotalText.Text = "";
                    lblTotal.Text = "";
                    WebCompraTitulo.InnerText = "Carrinho Compra Esta Vazio";
                    AtualizarBtn.Visible = false;
                    CheckoutBtn.Visible = false;
                }
            }  
        }


        public List<CompraItem> GetCompraItems()
        {
            WebCompraAct act = new WebCompraAct();
            return act.GetCompraItems();
        }

        public List<CompraItem> AtualizarCompraItems()
        {
            using (WebCompraAct usersWebCompra = new WebCompraAct())
            {
                String compraId = usersWebCompra.GetCompraId();
                WebCompraAct.AtualizarWebCompra[] atualizarWebCompras = new WebCompraAct.AtualizarWebCompra[CompraLista.Rows.Count];
                for (int i = 0; i < CompraLista.Rows.Count; i++)
                {
                    IOrderedDictionary rowValues = new OrderedDictionary();
                    rowValues = GetValues(CompraLista.Rows[i]);
                    atualizarWebCompras[i].ProdutoId = Convert.ToInt32(rowValues["ProdutoID"]);

                    CheckBox cbRemove = new CheckBox();
                    cbRemove = (CheckBox)CompraLista.Rows[i].FindControl("Remove");
                    atualizarWebCompras[i].RemoveItem = cbRemove.Checked;

                    TextBox quantidadeTextBox = new TextBox();
                    quantidadeTextBox = (TextBox)CompraLista.Rows[i].FindControl("PuxarQuantidade");
                    atualizarWebCompras[i].PuxarQuantidade = Convert.ToInt16(quantidadeTextBox.Text.ToString());
                }
                usersWebCompra.AtualizarAtualizarWebCompraDatabase(compraId, atualizarWebCompras);
                CompraLista.DataBind();
                lblTotal.Text = String.Format("{0:c}", usersWebCompra.GetTotal());
                return usersWebCompra.GetCompraItems();
            }
        }

        public static IOrderedDictionary GetValues(GridViewRow row)
        {
            IOrderedDictionary values = new OrderedDictionary();
            foreach (DataControlFieldCell cell in row.Cells)
                    {
                if (cell.Visible)
                {
                    //Extrair valores da celula
                    cell.ContainingField.ExtractValuesFromCell(values, cell, row.RowState, true);
                }
            }
            return values;
        }

        protected void AtualizarBtn_Click(object sender, EventArgs e)
        {
            AtualizarCompraItems();
        }

        protected void Enviar_Click(object sender, EventArgs e)
        {

            MemoryStream memoryStream = new MemoryStream();
            try
            {
                //Gerar pdf
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=PedidoCompra.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter stringWriter = new StringWriter();
                HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
                //CompraLista.DataBind();
                CompraLista.RenderControl(htmlTextWriter);
    
                //CompraLista.HeaderRow.Style.Add("width", "2%");
                //CompraLista.HeaderRow.Style.Add("font-size", "5px");
                //CompraLista.Style.Add("text-decoration", "none");
                //CompraLista.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                //CompraLista.Style.Add("font-size", "5px");

                //float[] ColumnWidths = new float[] { 10, 0, 10, 0 };

                //PdfPTable table = new PdfPTable(ColumnWidths.Length);
                //table.SetWidths(ColumnWidths);

                StringReader sr = new StringReader(stringWriter.ToString());
                Document doc = new Document(PageSize.A4, 5f, 5f, 5f, 0f);             
                string caminho = @"C:\Compras\" + "PedidoCompra.pdf";
                HTMLWorker htmlparser = new HTMLWorker(doc);
                PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));
                PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
                doc.AddCreationDate();
                doc.Open();


                //Editar conteudo do documento

                Paragraph datacp = new Paragraph(new Phrase(DateTime.Now.ToString()));
                datacp.Alignment = Element.ALIGN_CENTER;
                doc.Add(new Paragraph(datacp));

                string simg = "https://media-exp1.licdn.com/dms/image/C4D0BAQHt4isDyzifxQ/company-logo_200_200/0/1629836908866?e=2147483647&v=beta&t=9O5edIIrvPSrt8cqB3zMmj9NdbwzZa6uddtjdbgkU2g";
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(simg);
                img.ScaleAbsolute(50, 50);
                doc.Add(img);

                Paragraph titulo = new Paragraph();
                titulo.Font = new Font(Font.FontFamily.COURIER, 30);
                titulo.Alignment = Element.ALIGN_CENTER;
                titulo.Add("Pedido de Compra");
                doc.Add(titulo);

                Paragraph paragrafo = new Paragraph(" ", new Font(Font.NORMAL, 15));
                string conteudo = "Solicitação de compra dos seguintes items \n \n ";
                paragrafo.Alignment = Element.ALIGN_LEFT;
                paragrafo.Add(conteudo);
                doc.Add(paragrafo);




                htmlparser.Parse(sr);
                doc.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();


                //Envio de email
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();
                mail.From = new MailAddress("email@a.com.br");
                mail.To.Add("email2@a.com.br");
                mail.Subject = "Pedido Compra";
                mail.Body = "Segue solicitação compra";
                mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "PedidoCompra.pdf"));
                mail.IsBodyHtml = true;
                SmtpServer.UseDefaultCredentials = false;
                NetworkCredential NetworkCred = new NetworkCredential("email@a.com.br", "senha");
                SmtpServer.Credentials = NetworkCred;
                SmtpServer.EnableSsl = true;
                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.Send(mail);

                Response.Write(doc);
                Session.Clear();
                Session.Abandon();
                Response.Redirect("Default.aspx");
                Response.End();
            }
            catch
            {
                memoryStream.Dispose();
                throw;

            }

            using (Logic.WebCompraAct usersWebCompraAct =
              new Logic.WebCompraAct())
            {
                usersWebCompraAct.CarrinhoVazio();
            }

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

    }
}
