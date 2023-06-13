namespace api_asp_project_PI.Models
{
    public class Cliente
    {
        public Int32 idCliente { get; set; }
        public string nomCliente { get; set; }
        public string apeCliente { get; set; }
        public Int32 edadCliente { get; set; }
        public Int32 cel { get; set; }
        public Int32 numDoc { get; set; }
        public Int32 tpdoc { get; set; }
        public string nomtpd { get; set; }



        /*---------------*/
        public string dni { get; set; }
        public string email { get; set; }


        public Cliente()
        {
            nomCliente = "";
            apeCliente = "";
            nomtpd = "";
        }
    }
}