//using Swashbuckle.AspNetCore.Annotations;

namespace api_asp_project_PI.Models
{
    
    public class Proveedor
    {
       
        public int codprov { get; set; }
        public string razons { get; set; }
        public string rucprov { get; set; }
        public int telefprov { get; set; }

        public Proveedor()
        {
            razons = "";
            rucprov = "";

        }
    }
}