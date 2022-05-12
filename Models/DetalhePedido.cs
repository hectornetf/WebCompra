using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCompra.Models
{
    public class DetalhePedido
    {
        public int DetalhePedidoId { get; set; }
        public int PedidoId { get; set; }
        public string Usuario { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public double? PrecoUnidade { get; set; }
    }
}