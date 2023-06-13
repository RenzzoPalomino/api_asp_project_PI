using api_asp_project_PI.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Xml.Serialization;

//importando bibliotecas a utilizar
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

using System.Web;
using System.Threading.Tasks;


namespace api_asp_project_PI.Controllers
{

    [Route("[Controller]")]
    public class PedidoController : Controller
    {
        #region Cadena de Conexion
        string cadena = @"Server=tcp:asp-project-pi.database.windows.net,1433;Initial Catalog=bd_sis_superm_dswi;Persist Security Info=False;User ID=asp-pi-admin;Password=2423Project#Password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        #endregion

        #region metodos de acceso a datos

        IEnumerable<Pedido> getPedido()
        {
            List<Pedido> temporaltpd = new List<Pedido>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_listarPedidos", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporaltpd.Add(new Pedido()
                    {
                        idPedido = dr.GetInt32(0),
                        fpedido = dr.GetDateTime(1),
                        dni = dr.GetString(2),
                        nombre = dr.GetString(3),
                        email = dr.GetString(4),
                        total = dr.GetDecimal(5)

                    });
                }
                cn.Close();
            }
            return temporaltpd;
        }
        #endregion

        #region endpoints


        [HttpGet("pedidos")]
        public async Task<ActionResult<IEnumerable<Pedido>>> pedidos()
        {
            return Ok(await Task.Run(() => getPedido()));
        }

        #endregion
    }
        
}
