using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ClassLibrary1;

namespace ConsoleApp9
{
    internal class Ejercicio6
    {
        static void Main(string[] args)
        {

            if (!File.Exists("appconfig.json"))
            {
                CreateAppConfigFile();
                Console.WriteLine("Archivo de configuración creado.");
            }

            AccesoDatos miMaletin = new AccesoDatos();

            while (true)
            {

                Console.WriteLine("Elige una opción");
                Console.WriteLine("1. Insertar un producto nuevo");
                Console.WriteLine("2. Actualizar un producto");
                Console.WriteLine("3. Borrar un producto");
                Console.WriteLine("4. Salir");

                string opcion = Console.ReadLine();

                switch (opcion)
                {

                    case "1":
                        Console.Write("Nombre del nuevo producto: ");
                        string nombreIns = Console.ReadLine();
                        Console.Write("Precio del nuevo producto: ");
                        decimal precioIns = decimal.Parse(Console.ReadLine());

                        miMaletin.InsertarProducto(nombreIns, precioIns);
                        Console.WriteLine("Producto insertado");
                        break;

                    case "2":
                        Console.Write("ID del producto a cambiar: ");
                        int idUpd = int.Parse(Console.ReadLine());
                        Console.Write("Nuevo Nombre: ");
                        string nombreUpd = Console.ReadLine();
                        Console.Write("Nuevo Precio: ");
                        decimal precioUpd = decimal.Parse(Console.ReadLine());

                        miMaletin.ActualizarProducto(idUpd, nombreUpd, precioUpd);
                        Console.WriteLine("Producto actualizado");
                        break;

                    case "3":
                        Console.Write("ID del producto a borrar: ");
                        int idDel = int.Parse(Console.ReadLine());

                        miMaletin.BorrarProducto(idDel);
                        Console.WriteLine("Producto borrado");
                        break;

                    case "4":;
                        return;

                    default:
                        break;
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

