using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebCompra.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public DateTime PedidoData { get; set; }
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Requer Nome")]
        [DisplayName("Nome")]
        [StringLength(160)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Requer Sobrenome")]
        [DisplayName("Sobrenome")]
        [StringLength(160)]
        public string Sobrenome { get; set; }

        [ScaffoldColumn(false)]
        public decimal Total { get; set; }

        [ScaffoldColumn(false)]
        public bool PedidoEnviado { get; set; }

        public List<DetalhePedido> DetalhePedido { get; set; }
    }
}