/*
 * 
 * ESTE CONTROLADOR NO  SE ENCUENTRA HABILITADO
 * 
 */
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
using Newtonsoft.Json;

namespace api_asp_project_PI.Controllers
{

    [Route("[Controller]")]
    public class EcommerceController : Controller
    {
        #region Cadena de Conexion
        string cadena = @"Server=tcp:asp-project-pi.database.windows.net,1433;Initial Catalog=bd_sis_superm_dswi;Persist Security Info=False;User ID=asp-pi-admin;Password=2423Project#Password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        #endregion

        #region metodos de acceso a datos


        string sellPedido(Cliente reg)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    //Pedido
                    SqlCommand cmd = new SqlCommand("usp_agrega_pedido", cn, tr);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idpedido", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@dni", reg.dni);
                    cmd.Parameters.AddWithValue("@nombre", reg.nomCliente);
                    cmd.Parameters.AddWithValue("@email", reg.email);
                    cmd.ExecuteNonQuery();

                    int idpedido = (int)cmd.Parameters["@idpedido"].Value;

                    //Detalle
                    List<Registro> temporal = JsonConvert.DeserializeObject<List<Registro>>(HttpContext.Session.GetString("canasta"));
                    foreach (Registro it in temporal)
                    {
                        cmd = new SqlCommand("exec usp_agrega_detalle @idpedido,@idproducto,@cantidad,@precio", cn, tr);
                        cmd.Parameters.AddWithValue("@idpedido", idpedido);
                        cmd.Parameters.AddWithValue("@idproducto", it.codProd);
                        cmd.Parameters.AddWithValue("@cantidad", it.cantidad);
                        cmd.Parameters.AddWithValue("@precio", it.preProd);
                        cmd.ExecuteNonQuery();
                    }

                    //Stock
                    foreach (Registro it in temporal)
                    {
                        cmd = new SqlCommand("exec usp_actualiza_stock @idproducto,@cant", cn, tr);

                        cmd.Parameters.AddWithValue("@idproducto", it.codProd);
                        cmd.Parameters.AddWithValue("@cant", it.cantidad);
                        cmd.ExecuteNonQuery();
                    }

                    tr.Commit();
                    mensaje = $"Se ha registrado el pedido de numero {idpedido}";
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message;
                    tr.Rollback();
                }
                finally
                {
                    cn.Close();
                }
            }
            return mensaje;
        }

        #endregion

        #region endpoints



        //[HttpPost("Venta")]
        //public async Task<ActionResult<string>> ventaPedido(Cliente reg)
        //{
        //    return Ok(await Task.Run(() => sellPedido(reg)));
        //}
        #endregion
    }
        
}
