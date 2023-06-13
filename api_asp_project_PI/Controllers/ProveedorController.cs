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
    public class ProveedorController : Controller
    {
        #region Cadena de Conexion
        string cadena = @"Server=tcp:asp-project-pi.database.windows.net,1433;Initial Catalog=bd_sis_superm_dswi;Persist Security Info=False;User ID=asp-pi-admin;Password=2423Project#Password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        #endregion

        #region metodos de acceso a datos

        IEnumerable<Proveedor> getProveedor()
        {
            List<Proveedor> proveedor = new List<Proveedor>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_listar_proveedores", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    proveedor.Add(new Proveedor()
                    {
                        codprov = dr.GetInt32(0),
                        razons = dr.GetString(1),
                        rucprov = dr.GetString(2),
                        telefprov = dr.GetInt32(3)
                    });
                }
                cn.Close();
            }
            return proveedor;
        }
        IEnumerable<Proveedor> buscarCodigo(int codprov)
        {
            return getProveedor().Where(c => c.codprov == codprov);
        }

        string addProveedor(Proveedor reg)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_insertar_proveedores", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@razons", reg.razons.ToUpper());
                    cmd.Parameters.AddWithValue("@rucprov", reg.rucprov);
                    cmd.Parameters.AddWithValue("@telefprov", reg.telefprov);
                    cmd.ExecuteNonQuery();

                    mensaje = $"Se ha registrado el Proveedor {reg.razons}.";
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message;
                }
                finally { cn.Close(); }
            }
            return mensaje;
        }

        String updateProveedor(Proveedor reg)
        {
            String mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_actualizar_proveedores", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codprov", reg.codprov);
                    cmd.Parameters.AddWithValue("@razons", reg.razons.ToUpper());
                    cmd.Parameters.AddWithValue("@rucprov", reg.rucprov);
                    cmd.Parameters.AddWithValue("@telefprov", reg.telefprov);

                    cmd.ExecuteNonQuery();

                    mensaje = $"Se ha actualizado el Proveedor {reg.razons}";
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message;
                }
                finally { cn.Close(); }
            }

            return mensaje;
        }

        String deleteProveedor(int codprov)
        {
            String mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_eliminar_proveedores", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codprov", codprov);
                    cmd.ExecuteNonQuery();

                    mensaje = $"Se ha eliminado el registro";
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message;
                }
                finally { cn.Close(); }
            }
            return mensaje;
        }


        #endregion

        #region endpoints


        [HttpGet("proveedores")]
        public async Task<ActionResult<IEnumerable<Proveedor>>> proveedores()
        {
            return Ok(await Task.Run(() => getProveedor()));
        }

        [HttpGet("buscarProveedor")]
        public async Task<ActionResult<Proveedor>> proveedoresPorCodigo(int codprov)
        {
            return Ok(await Task.Run(() => buscarCodigo(codprov)));
        }



        [HttpPost("agregar")]
        public async Task<ActionResult<string>> agregarProveedor(Proveedor reg)
        {
            return Ok(await Task.Run(() => addProveedor(reg)));
        }
        
        
        [HttpPut("actualizar")]
        public async Task<ActionResult<string>> actualizarproveedor(Proveedor reg)
        {
            return Ok(await Task.Run(() => updateProveedor(reg)));
        }

        [HttpDelete("eliminar")]
        public async Task<ActionResult<string>> eliminarproveedor(int codprov)
        {
            return Ok(await Task.Run(() => deleteProveedor(codprov)));
        }


        #endregion
    }
        
}
