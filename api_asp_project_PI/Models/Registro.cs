using System.ComponentModel.DataAnnotations;
namespace api_asp_project_PI.Models
{
    public class Registro
    {
        public Int32 codProd { get; set; }
        public string nomProd { get; set; }
        public string nomCat { get; set; }
        public decimal preProd { get; set; }
        public int cantidad { get; set; }
        public decimal monto { get { return preProd * cantidad; } }
    }
}
