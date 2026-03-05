using System;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace AP_test1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // connexion à la base
            // Chaîne de connexion à la base de données MySQL
            MySqlConnection? maConnection;
            string connectionString = "server=localhost;user=root;database=r2d2;port=3306;password=";
            Console.WriteLine("START");

            // Création d'une nouvelle connexion MySQL
            using (maConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Ouverture de la connexion
                    maConnection.Open();
                    Console.WriteLine("Connexion réussie à la base de données MySQL.");

                    // Exemple de requête SQL
                    string query = "SELECT * FROM sexe";

                    // Création d'une commande SQL
                    using (MySqlCommand command = new MySqlCommand(query, maConnection))
                    {
                        Console.WriteLine(maConnection.IsDisposed);
                        // Exécution de la commande et lecture des résultats
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine(reader[0] + " " + reader[1] + " " + reader[2]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Gestion des erreurs de connexion
                    Console.WriteLine("Erreur de connexion : " + ex.Message);
                }

                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
     
                ApplicationConfiguration.Initialize();
                Application.Run(new Form1(maConnection));
            }
        
        }
    }
}