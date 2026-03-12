using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class Plateforme
{
    // ====== ATTRIBUTS ======
    private int Id;
    private string Nom;

    // ====== CONSTRUCTEURS ======
    public Plateforme()
    {
        this.Id = 0;
        this.Nom = "";
    }

    public Plateforme(string newNom)
    {
        this.Id = 0;
        this.Nom = newNom;
    }

    // ====== ACCESSEURS ======

    public int GetId()
    {
        return this.Id;
    }

    public string GetNom()
    {
        return this.Nom;
    }

    public void SetNom(string newNom)
    {
        this.Nom = newNom;
    }

    // ====== METHODES ======

    public static void CreerPlateforme(MySqlConnection connection, Plateforme plateforme)
    {
        string query = "INSERT INTO plateforme(nom) VALUES (@nom)";

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@nom", plateforme.Nom);
            cmd.ExecuteNonQuery();
        }
    }

    public static List<Plateforme> GetPlateformes(MySqlConnection connection)
    {
        List<Plateforme> liste = new List<Plateforme>();

        string query = "SELECT * FROM plateforme";

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        using (MySqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                Plateforme plateforme = new Plateforme();

                plateforme.Id = reader.GetInt32("id_plateforme");
                plateforme.Nom = reader.GetString("nom");

                liste.Add(plateforme);
            }
        }

        return liste;
    }

    public static void UpdatePlateforme(MySqlConnection connection, Plateforme plateforme)
    {
        string query = "UPDATE plateforme SET nom=@nom WHERE id_plateforme=@id";

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@id", plateforme.Id);
            cmd.Parameters.AddWithValue("@nom", plateforme.Nom);

            cmd.ExecuteNonQuery();
        }
    }

    public static void DeletePlateforme(MySqlConnection connection, int id)
    {
        string query = "DELETE FROM plateforme WHERE id_plateforme=@id";

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}