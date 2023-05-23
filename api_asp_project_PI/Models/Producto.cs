using System.ComponentModel.DataAnnotations;
namespace api_asp_project_PI.Models
{
    public class Producto
    {
        [Display(Name = "Código")] public Int32 codProd { get; set; }
        [Display(Name = "Producto")] public string nomProd { get; set; }
        [Display(Name = "Precio")] public decimal preProd { get; set; }
        [Display(Name = "Stock")] public Int32 stockProd { get; set; }
        [Display(Name = "Cod. Categoría")] public Int32 codCat { get; set; }
        [Display(Name = "Cod. Proveedor")] public Int32 codProv { get; set; }

        [Display(Name = "Categoría")] public string nomCat { get; set; }

        [Display(Name = "Proveedor")] public string nomProv { get; set; }

        public Producto()
        {
            nomProd = "";
            nomCat = "";
            nomProv = "";
        }
    }
}
