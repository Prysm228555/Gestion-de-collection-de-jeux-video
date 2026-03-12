using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class Editeur
{
    // ====== ATTRIBUTS ======
    private int Id;
    private string Nom;

    // ====== CONSTRUCTEURS ======
    public Editeur()
    {
        this.Id = 0;
        this.Nom = "";
    }

    public Editeur(string newNom)
    {
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

    public static void CreerEditeur(MySqlConnection connection, Editeur editeur)
    {
        string query = "INSERT INTO editeur(nom) VALUES (@nom)";

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@nom", editeur.Nom);
            cmd.ExecuteNonQuery();
        }
    }

    public static List<Editeur> GetEditeurs(MySqlConnection connection)
    {
        List<Editeur> liste = new List<Editeur>();

        string query = "SELECT * FROM editeur";

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        using (MySqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                Editeur editeur = new Editeur();

                editeur.Id = reader.GetInt32("id_editeur");
                editeur.Nom = reader.GetString("nom");

                liste.Add(editeur);
            }
        }

        return liste;
    }

    public static void UpdateEditeur(MySqlConnection connection, Editeur editeur)
    {
        string query = "UPDATE editeur SET nom=@nom WHERE id_editeur=@id";

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@id", editeur.Id);
            cmd.Parameters.AddWithValue("@nom", editeur.Nom);

            cmd.ExecuteNonQuery();
        }
    }

    public static void DeleteEditeur(MySqlConnection connection, int id)
    {
        string query = "DELETE FROM editeur WHERE id_editeur=@id";

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}