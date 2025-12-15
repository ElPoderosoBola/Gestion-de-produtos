using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Data.SqlClient;

namespace ConsoleApp9
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

            UpdateDatabase();
        }

        private static void UpdateDatabase()
        {
            Console.WriteLine("ID del producto a actualizar:");
            string id = Console.ReadLine();

            Console.WriteLine("Nombre actualizado del producto:");
            string name = Console.ReadLine();

            Console.WriteLine("Precio actualizado del producto:");
            string desc = Console.ReadLine();



            string json = File.ReadAllText("appconfig.json");
            ConfigModel config = JsonSerializer.Deserialize<ConfigModel>(json);

            using (SqlConnection connection = new SqlConnection(config.ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE Productos SET Nombre = @Name, Precio = @price WHERE ID = @Id";

                SqlParameter nameParameter = new SqlParameter();
                nameParameter.ParameterName = "@name";
                nameParameter.SqlDbType = SqlDbType.NVarChar;
                nameParameter.Value = name;
                nameParameter.Direction = ParameterDirection.Input;

                SqlParameter descParameter = new SqlParameter();
                descParameter.ParameterName = "@price";
                descParameter.SqlDbType = SqlDbType.Money;
                descParameter.Value = desc;
                descParameter.Direction = ParameterDirection.Input;

                SqlParameter idParameter = new SqlParameter();
                idParameter.ParameterName = "@id";
                idParameter.SqlDbType = SqlDbType.Int;
                idParameter.Value = id;
                idParameter.Direction = ParameterDirection.Input;

                command.Parameters.Add(nameParameter);
                command.Parameters.Add(descParameter);
                command.Parameters.Add(idParameter);

                int affectedRows = command.ExecuteNonQuery();
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


