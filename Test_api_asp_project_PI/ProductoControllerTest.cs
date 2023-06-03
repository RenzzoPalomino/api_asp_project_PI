using api_asp_project_PI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using api_asp_project_PI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace Test_api_asp_project_PI
{
    public class ProductoControllerTest
    {


        [SetUp]
        public void Setup()
        {
             
        }


        # region PARA LA OBTENCION PLANA DE DATOS
        [Test]
        public async Task ProductosTest()  //evalua si el api respectivo a Productos tiene contenido
        {
            // Arrange
            var httpClient = new HttpClient();
            var url = "https://api-deploy-pi-2423.azurewebsites.net/Producto/productos";

            // Act
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var productos = await response.Content.ReadFromJsonAsync<IEnumerable<Producto>>();

            // Assert
            Assert.IsNotNull(productos, "La lista de productos no debe ser nula.");
            Assert.IsTrue(productos.Any(), "La lista de productos no debe estar vacía.");
        }

        [Test]
        public async Task CategoriasTest()   //evalua si el api respectivo a Categorias tiene contenido
        {
            // Arrange
            var httpClient = new HttpClient();
            var url = "https://api-deploy-pi-2423.azurewebsites.net/Producto/categorias";

            // Act
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var categorias = await response.Content.ReadFromJsonAsync<IEnumerable<Producto>>();

            // Assert
            Assert.IsNotNull(categorias, "La lista de categorias no debe ser nula.");
            Assert.IsTrue(categorias.Any(), "La lista de categorias no debe estar vacía.");
        }


        [Test]
        public async Task ProveedoresTest()   //Evalúa si el api respectivo a Proveedores tiene contenido
        {
            // Arrange
            var httpClient = new HttpClient();
            var url = "https://api-deploy-pi-2423.azurewebsites.net/Producto/proveedores";

            // Act
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var proveedores = await response.Content.ReadFromJsonAsync<IEnumerable<Producto>>();

            // Assert
            Assert.IsNotNull(proveedores, "La lista de categorias no debe ser nula.");
            Assert.IsTrue(proveedores.Any(), "La lista de categorias no debe estar vacía.");
        }
        #endregion

        #region  PARA LA BUSQUEDA DE CATEGORIAS
        //categoria existente
        [Test]
        public async Task Categoria_Existe_Test()   //evalua si el parametro ingresado EXISTE en el contenido de la api
        {
            // Arrange
            var httpClient = new HttpClient();
            var categoria = "bebidas";
            var url = $"https://api-deploy-pi-2423.azurewebsites.net/Producto/buscarxcategoria/{categoria}";

            // Act
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var productos = await response.Content.ReadFromJsonAsync<IEnumerable<Producto>>();

            // Assert
            Assert.IsNotNull(productos, "La lista de productos no debe ser nula.");
            // Asegurarse de que los productos devueltos corresponden a la categoría buscada
            Assert.True(productos.Any(p => p.nomCat.Equals(categoria, StringComparison.OrdinalIgnoreCase)), "Todos los productos deben tener la categoría buscada.");
        }

        //categoria inexistente
        [Test]
        public async Task Categoria_NO_Existe_Test()    //evalua si el parametro ingresado NO EXISTE en el contenido de la api
        {
            // Arrange
            var httpClient = new HttpClient();
            var categoria = "CATEGORIA NO EXISTE";
            var url = $"https://api-deploy-pi-2423.azurewebsites.net/Producto/buscarxcategoria/{categoria}";

            // Act
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var productos = await response.Content.ReadFromJsonAsync<IEnumerable<Producto>>();

            // Assert
            Assert.IsNotNull(productos, "La lista de productos no debe ser nula.");
            Assert.IsFalse(productos.Any(), "La lista de productos debe estar vacía para una categoría inexistente.");
        }

        //categoria en minusculas
        [Test]
        public async Task Categoria_Existe_minusculas_Test()   //evalua si el parametro ingresado en minusculas EXISTE en el contenido de la api
        {
            // Arrange
            var httpClient = new HttpClient();
            var categoria = "condimentos";
            var url = $"https://api-deploy-pi-2423.azurewebsites.net/Producto/buscarxcategoria/{categoria}";

            // Act
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var productos = await response.Content.ReadFromJsonAsync<IEnumerable<Producto>>();

            // Assert
            Assert.IsNotNull(productos, "La lista de productos no debe ser nula.");
            // Asegurarse de que los productos devueltos corresponden a la categoría buscada
            Assert.True(productos.Any(p => p.nomCat.Equals(categoria, StringComparison.OrdinalIgnoreCase)), "Todos los productos deben tener la categoría buscada.");
        }
        //categoria en mayusculas
        [Test]
        public async Task Categoria_Existe_mayusculas_Test()   //evalua si el parametro ingresado en mayusculas EXISTE en el contenido de la api
        {
            // Arrange
            var httpClient = new HttpClient();
            var categoria = "CONDIMENTOS";
            var url = $"https://api-deploy-pi-2423.azurewebsites.net/Producto/buscarxcategoria/{categoria}";

            // Act
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var productos = await response.Content.ReadFromJsonAsync<IEnumerable<Producto>>();

            // Assert
            Assert.IsNotNull(productos, "La lista de productos no debe ser nula.");
            // Asegurarse de que los productos devueltos corresponden a la categoría buscada
            Assert.True(productos.Any(p => p.nomCat.Equals(categoria, StringComparison.OrdinalIgnoreCase)), "Todos los productos deben tener la categoría buscada.");
        }

        #endregion

        #region PARA LA BUSQUEDA DE PROVEEDORES
        //proveedor existente
        [Test]
        public async Task Proveedor_Existe_Test()   //evalua si el parametro ingresado EXISTE en el contenido de la api
        {
            // Arrange
            var httpClient = new HttpClient();
            var proveedor = "COCA COLA PERU SAC";
            var url = $"https://api-deploy-pi-2423.azurewebsites.net/Producto/buscarxproveedor/{proveedor}";

            // Act
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var productos = await response.Content.ReadFromJsonAsync<IEnumerable<Producto>>();

            // Assert
            Assert.IsNotNull(productos, "La lista de proveedores no debe ser nula.");
            // Asegurarse de que los productos devueltos corresponden a la categoría buscada
            Assert.True(productos.Any(p => p.nomProv.Equals(proveedor, StringComparison.OrdinalIgnoreCase)), "La lista de productos debe estar vacía para el proveedor indicado.");
        }

        //proveedor inexistente
        [Test]
        public async Task Proveedor_NO_Existe_Test()    //evalua si el parametro ingresado NO EXISTE en el contenido de la api
        {
            // Arrange
            var httpClient = new HttpClient();
            var proveedor = "Proveedor no existe";
            var url = $"https://api-deploy-pi-2423.azurewebsites.net/Producto/buscarxproveedor/{proveedor}";

            // Act
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var productos = await response.Content.ReadFromJsonAsync<IEnumerable<Producto>>();

            // Assert
            Assert.IsNotNull(productos, "La lista de productos no debe ser nula.");
            Assert.IsFalse(productos.Any(), "La lista de productos debe estar vacía para el proveedor indicado.");
        }

        //proveedor con caracteres especiales ($, &, ", %)
        [Test]
        public async Task Proveedor_Caracteres_Especiales_Test()   //evalua si el parametro ingresado EXISTE en el contenido de la api
        {
            // Arrange
            var httpClient = new HttpClient();
            var proveedor = "JOHNSON & JOHNSON";
            var url = $"https://api-deploy-pi-2423.azurewebsites.net/Producto/buscarxproveedor/{proveedor}";

            // Act
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var productos = await response.Content.ReadFromJsonAsync<IEnumerable<Producto>>();

            // Assert
            Assert.IsNotNull(productos, "La lista de proveedores no debe ser nula.");
            // Asegurarse de que los productos devueltos corresponden a la categoría buscada
            Assert.True(productos.Any(p => p.nomProv.Equals(proveedor, StringComparison.OrdinalIgnoreCase)), "La lista de productos debe estar vacía para el proveedor indicado.");
        }
        #endregion

        #region PARA LA BUSQUEDA DE PRODUCTOS POR RANGO DE PRECIOS

        //Para productos que se encuentren dentro de un rango coherente al listado

        [Test]
        public async Task Precios_que_contienen_productos_test()
        {
            // Arrange
            var httpClient = new HttpClient();
            var precio1 = 20;
            var precio2 = 40;
            var url = $"https://api-deploy-pi-2423.azurewebsites.net/Producto/buscarxPrecio/{precio1}/{precio2}";

            // Act
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var productos = await response.Content.ReadFromJsonAsync<IEnumerable<Producto>>();

            // Assert
            Assert.IsNotNull(productos, "La lista de productos no debe ser nula.");
            // Asegurarse de que al menos un producto tiene un precio dentro del rango especificado
            Assert.IsTrue(productos.Any(p => p.preProd >= precio1 && p.preProd <= precio2), "Al menos un producto debe tener un precio dentro del rango especificado.");
        }

        //Para productos que NO se encuentren dentro de un rango coherente al listado
        [Test]
        public async Task Precios_que_NO_contienen_productos_test()
        {
            // Arrange
            var httpClient = new HttpClient();
            var precio1 = 900;
            var precio2 = 950;
            var url = $"https://api-deploy-pi-2423.azurewebsites.net/Producto/buscarxPrecio/{precio1}/{precio2}";

            // Act
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var productos = await response.Content.ReadFromJsonAsync<IEnumerable<Producto>>();

            // Asegurarse de que ningun producto tiene un precio dentro del rango especificado
            Assert.IsFalse(productos.Any(p => p.preProd >= precio1 && p.preProd <= precio2));
        }

        #endregion

    }
}
