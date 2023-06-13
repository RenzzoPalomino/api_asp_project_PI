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
    public class CategoriaController : Controller
    {
        #region Cadena de Conexion
        string cadena = @"Server=tcp:asp-project-pi.database.windows.net,1433;Initial Catalog=bd_sis_superm_dswi;Persist Security Info=False;User ID=asp-pi-admin;Password=2423Project#Password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        #endregion

        #region metodos de acceso a datos

        IEnumerable<Categoria> getCategoria()
        {
            List<Categoria> categoria = new List<Categoria>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("listar_tcatprueba", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    categoria.Add(new Categoria()
                    {
                        codCat = dr.GetInt32(0),
                        nomCat = dr.GetString(1),
                    });
                }
            }
            return categoria;
        }
        IEnumerable<Categoria> buscarPorCodCat(int codCat)
        {
            return getCategoria().Where(c => c.codCat == codCat);
        }

        string addCategoria(Categoria reg)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_insertar_tcategoria", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nomcat", reg.nomCat.ToUpper());
                    cmd.ExecuteNonQuery();
                    mensaje = $"Se ha registrado el tipo de categoria {reg.nomCat}";
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message;
                }
                finally
                {
                    cn.Close();
                }
            }
            return mensaje;
        }

        String updateCategoria(Categoria reg)
        {
            String mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_actualiza_tcategoria", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codcat", reg.codCat);
                    cmd.Parameters.AddWithValue("@nomcat", reg.nomCat.ToUpper());
                    cmd.ExecuteNonQuery();
                    mensaje = $"Se ha actualizado el tipo de categoria {reg.nomCat}";
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message;
                }
                finally { cn.Close(); }
            }

            return mensaje;
        }

        String deleteCategoria(int codcat)
        {
            String mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_eliminar_categoria", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codcat", codcat);
                    cmd.ExecuteNonQuery();

                    mensaje = $"Se ha eliminado la categoria";
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


        [HttpGet("categorias")]
        public async Task<ActionResult<IEnumerable<Categoria>>> proveedores()
        {
            return Ok(await Task.Run(() => getCategoria()));
        }

        [HttpGet("buscarCategoria")]
        public async Task<ActionResult<Proveedor>> proveedoresPorCodCat(int codCat)
        {
            return Ok(await Task.Run(() => buscarPorCodCat(codCat)));
        }



        [HttpPost("agregar")]
        public async Task<ActionResult<string>> agregarProveedor(Categoria reg)
        {
            return Ok(await Task.Run(() => addCategoria(reg)));
        }
        
        
        [HttpPut("actualizar")]
        public async Task<ActionResult<string>> actualizarproveedor(Categoria reg)
        {
            return Ok(await Task.Run(() => updateCategoria(reg)));
        }

        [HttpDelete("eliminar")]
        public async Task<ActionResult<string>> eliminarproveedor(int codCat)
        {
            return Ok(await Task.Run(() => deleteCategoria(codCat)));
        }


        #endregion
    }
        
}
