using System.ComponentModel.DataAnnotations;
namespace api_asp_project_PI.Models
{
    public class Pedido
    {
        public int idPedido { get; set; }
        public DateTime fpedido { get; set; }
        public string dni { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }

        public Decimal total { get; set; }

    }
}
