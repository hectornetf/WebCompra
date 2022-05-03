using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebCompra.Models
{
    public class CompraItem
    {
        [Key]
        public string ItemId { get; set; }
        public string CompraId { get; set; }
        public int Quantidade { get; set; }
        public System.DateTime DataCriada { get; set; }
        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }
    }
}