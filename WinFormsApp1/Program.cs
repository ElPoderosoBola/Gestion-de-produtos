using ClassLibrary1;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WinFormsApp1
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            if (!File.Exists("appconfig.json"))
            {
                CreateAppConfigFile();
                Console.WriteLine("Archivo de configuración creado.");
            }

            AccesoDatos gestorDB = new AccesoDatos();


            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
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

    
