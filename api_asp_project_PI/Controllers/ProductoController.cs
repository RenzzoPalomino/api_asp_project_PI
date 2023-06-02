﻿using api_asp_project_PI.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Xml.Serialization;

//importando bibliotecas a utilizar
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

using System.Web;


namespace api_asp_project_PI.Controllers
{

    [Route("[Controller]")]
    public class ProductoController : Controller
    {
        #region Cadena de Conexion
        string cadena = @"Server=tcp:asp-project-pi.database.windows.net,1433;Initial Catalog=bd_sis_superm_dswi;Persist Security Info=False;User ID=asp-pi-admin;Password=2423Project#Password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        #endregion

        #region metodos de acceso a datos

        IEnumerable<Producto> getProducto()
        {
            List<Producto> products = new List<Producto>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec api_listar_productos", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    products.Add(new Producto()
                    {
                        codProd = dr.GetInt32(0),
                        nomProd = dr.GetString(1),
                        preProd = dr.GetDecimal(2),
                        stockProd = dr.GetInt32(3),


                        nomCat = dr.GetString(4),
                        nomProv = dr.GetString(5)
                    });
                }
                cn.Close();
            }


            return products;
        }

        IEnumerable<Categoria> getCategoria()
        {
            List<Categoria> temporaltipcat = new List<Categoria>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("exec listar_tcat", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporaltipcat.Add(new Categoria()
                    {
                        codCat = dr.GetInt32(0),
                        nomCat = dr.GetString(1),
                    });
                }
            }
            return temporaltipcat;
        }

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

        IEnumerable<Producto> buscarXcategoria(string nomCat)
        {
            return getProducto().Where(c => c.nomCat == nomCat);
        }

        IEnumerable<Producto> buscarXproveedor(string nomProv)
        {
            return getProducto().Where(c => c.nomProv == nomProv);
        }

        IEnumerable<Producto> buscarXprecio(double minimo, double maximo)
        {
            return getProducto().Where(c => ((double)c.preProd) >= minimo && ((double)c.preProd) <= maximo);
        }

        #endregion

        #region endpoints
        [HttpGet("productos")]
        public async Task<ActionResult<IEnumerable<Producto>>> productos()
        {
            return Ok(await Task.Run(() => getProducto()));

        }


        [HttpGet("categorias")]
        public async Task<ActionResult<IEnumerable<Categoria>>> categorias()
        {
            return Ok(await Task.Run(() => getCategoria()));
        }

        [HttpGet("proveedores")]
        public async Task<ActionResult<IEnumerable<Categoria>>> proveedores()
        {
            return Ok(await Task.Run(() => getProveedor()));
        }
        
        /*---*/
        [HttpGet("buscarXcategoria/{nomCat}")]
        public async Task<ActionResult<Producto>> busquedaCategoria(string nomCat)
        {
            nomCat = nomCat.ToUpper();
            nomCat = nomCat.Replace("%20"," "); //para los espaciados generados en la uri
            nomCat = nomCat.Replace("%26","&"); //para el caracter especial "&"
            nomCat = nomCat.Replace("%3A", ":");
            nomCat = nomCat.Replace("%2C", ",");
            nomCat = nomCat.Replace("%3B", ";");
            nomCat = nomCat.Replace("%2B", "+");
            nomCat = nomCat.Replace("%2A", "*");

            nomCat = nomCat.Replace("%23", "#");
            nomCat = nomCat.Replace("%25", "%");
            nomCat = nomCat.Replace("%21", "!");

            nomCat = nomCat.Replace("%24", "$");
            nomCat = nomCat.Replace("%27", "'");
            nomCat = nomCat.Replace("%28", "(");

            nomCat = nomCat.Replace("%29", ")");
            nomCat = nomCat.Replace("%2D", "-");
            nomCat = nomCat.Replace("%5F", "_");
            return Ok(await Task.Run(() => buscarXcategoria(nomCat)));
        }
        

        [HttpGet("buscarXproveedor/{nomProv}")]
        public async Task<ActionResult<Producto>> busquedaProveedor(string nomProv)
        {

            nomProv = nomProv.ToUpper();
            nomProv = nomProv.Replace("%20", " "); //para los espaciados generados en la uri
            nomProv = nomProv.Replace("%26", "&"); //para el caracter especial "&"
            nomProv = nomProv.Replace("%3A", ":");
            nomProv = nomProv.Replace("%2C", ",");
            nomProv = nomProv.Replace("%3B", ";");
            nomProv = nomProv.Replace("%2B", "+");
            nomProv = nomProv.Replace("%2A", "*");

            nomProv = nomProv.Replace("%23", "#");
            nomProv = nomProv.Replace("%25", "%");
            nomProv = nomProv.Replace("%21", "!");

            nomProv = nomProv.Replace("%24", "$");
            nomProv = nomProv.Replace("%27", "'");
            nomProv = nomProv.Replace("%28", "(");

            nomProv = nomProv.Replace("%29", ")");
            nomProv = nomProv.Replace("%2D", "-");
            nomProv = nomProv.Replace("%5F", "_");

            return Ok(await Task.Run(() => buscarXproveedor(nomProv)));
        }


        [HttpGet("buscarXprecio/{minimo}/{maximo}")]
        public async Task<ActionResult<Producto>> busquedaPrecio(double minimo, double maximo)
        {
            return Ok(await Task.Run(()=> buscarXprecio(minimo,maximo)));
        }
        #endregion

        
        
    }
}
