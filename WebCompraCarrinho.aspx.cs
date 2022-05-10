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
                if (compraTotal > 0)
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
    }
}