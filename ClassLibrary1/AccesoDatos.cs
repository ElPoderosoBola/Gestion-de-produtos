using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClassLibrary1
{



    public class AccesoDatos
    {

        private string _connectionString;

        public AccesoDatos()
        {

            string json = File.ReadAllText("appconfig.json");

            ConfigModel config = JsonSerializer.Deserialize<ConfigModel>(json);

            _connectionString = config.ConnectionString;
        }

        public async Task<int> InsertarProducto(Producto producto)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("InsertarProducto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@nombre", producto.Nombre);
                    command.Parameters.AddWithValue("@precio", producto.Precio);
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> BorrarProducto(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("BorrarProducto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> ActualizarProducto(Producto producto)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("ActualizarProducto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", producto.ID);
                    command.Parameters.AddWithValue("@nombre", producto.Nombre);
                    command.Parameters.AddWithValue("@precio", producto.Precio);
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<Producto>> ListarProductos(string ordenarPor, int paginaNum, int tamanoPagina, string filtroNombre)
        {
            List<Producto> productos = new List<Producto>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("ListarProductos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ordenarPor", ordenarPor);
                    command.Parameters.AddWithValue("@paginaNum", paginaNum);
                    command.Parameters.AddWithValue("@tamanoPagina", tamanoPagina);


                    command.Parameters.AddWithValue("@filtroNombre", filtroNombre);


                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Producto producto = new Producto();


                            producto.ID = reader.GetInt32(0);
                            producto.Nombre = reader.GetString(1);
                            producto.Precio = reader.GetDecimal(2);
                            productos.Add(producto);
                        }
                    }
                }
            }
            return productos;
        }
    }
}