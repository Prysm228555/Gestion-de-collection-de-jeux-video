using MySql.Data.MySqlClient;
using System.Data.Common;

namespace AP_test1
{
    public partial class Form1 : Form
    {
        public MySqlConnection? fconnection;

        public Form1()
        {
            InitializeComponent();
        }

        public Form1(MySqlConnection icon)
        {
            fconnection = icon;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Exemple de requête SQL
            string query = "SELECT * FROM JEU";

            // Création d'une commande SQL
            using (MySqlCommand command = new MySqlCommand(query, fconnection))
            {
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
    }
}
