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

namespace WebCompra
{
    public partial class WebCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=PedidoCompra.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            CompraLista.DataBind();
            CompraLista.RenderControl(htmlTextWriter);
            CompraLista.HeaderRow.Style.Add("width", "10%");
            CompraLista.HeaderRow.Style.Add("font-size", "15px");
            CompraLista.Style.Add("text-decoration", "none");
            CompraLista.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
            CompraLista.Style.Add("font-size", "8px");
            StringReader sr = new StringReader(stringWriter.ToString());
            Document doc = new Document(PageSize.A2, 7f, 7f, 7f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(doc);
            PdfWriter.GetInstance(doc, Response.OutputStream);
            doc.Open();

            Paragraph titulo = new Paragraph("\n");
            titulo.Font = new Font(Font.FontFamily.COURIER, 40);
            titulo.Alignment = Element.ALIGN_CENTER;
            titulo.Add("Pedido de Compra \n");
            doc.Add(titulo);

            Paragraph paragrafo = new Paragraph(" ", new Font(Font.NORMAL, 20));
            string conteudo = "Solicitação de compra dos seguintes items \n \n ";
            paragrafo.Alignment = Element.ALIGN_LEFT;
            paragrafo.Add(conteudo);
            doc.Add(paragrafo);




            htmlparser.Parse(sr);
            doc.Close();
            Response.Write(doc);
            Response.End();

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

    }
}