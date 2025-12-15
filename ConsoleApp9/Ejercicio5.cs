using ConsoleApp9;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp28
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists("appconfig.json"))
            {
                CreateAppConfigFile();
                Console.WriteLine("Archivo de configuración creado.");
            }

            ListProducts();
        }

        private static void ListProducts()
        {
            string json = File.ReadAllText("appconfig.json");
            ConfigModel config = JsonSerializer.Deserialize<ConfigModel>(json);

            using (SqlConnection connection = new SqlConnection(config.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = "SELECT ID, Nombre, Precio FROM Productos";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id;
                            if (reader.IsDBNull(0))
                            {
                                id = -1;
                            }
                            else
                            {
                                id = reader.GetInt32(0);
                            }

                            string nombre = (reader.IsDBNull(1)) ? "" : reader.GetString(1);

                            decimal precio = (reader.IsDBNull(2)) ? 0 : reader.GetDecimal(2);

                            Console.WriteLine("---");
                            Console.WriteLine("ID: " + id);
                            Console.WriteLine("Nombre: " + nombre);
                            Console.WriteLine("Precio: " + precio);
                        }
                    }
                }
            }
        }

        private static void CreateAppConfigFile()
        {

            SqlConnectionStringBuilder builder = new();
            builder.DataSource = "127.0.0.1";
            builder.InitialCatalog = "TiendaDAM";
            builder.UserID = "app_dam";
            builder.Password = "Vegetta777";
            builder.TrustServerCertificate = true;
            string connectionString = builder.ToString();

            ConfigModel config = new ConfigModel();
            config.ConnectionString = connectionString;

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            options.Converters.Add(new JsonStringEnumConverter());
            options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

            string json = JsonSerializer.Serialize<ConfigModel>(config, options);
            File.WriteAllText("appconfig.json", json);
        }
    }
}