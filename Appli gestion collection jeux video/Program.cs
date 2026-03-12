using System;
using System.Data.Common;
using AP_test1;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static Utilisateur;


class Program
{
    [STAThread]
    static void Main()
    {
        string connectionString = "server=localhost;user=root;database=r2d2;port=3306;password=";

        using (MySqlConnection maConnection = new MySqlConnection(connectionString))
        {
            try
            {
                maConnection.Open();
                Console.WriteLine("Connexion réussie à la base de données MySQL.");

                List<Utilisateur> users = Utilisateur.GetUtilisateurs(maConnection);

                foreach (var user in users)
                {
                    Console.WriteLine(user.GetNom() + " " + user.GetPrenom());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(maConnection));
        }
    }
}