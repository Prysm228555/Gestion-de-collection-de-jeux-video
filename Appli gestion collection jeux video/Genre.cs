using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class Genre
{
    // ====== ATTRIBUTS ======
    private int Id;
    private string Nom;

    // ====== CONSTRUCTEURS ======
    public Genre()
    {
        this.Id = 0;
        this.Nom = "";
    }

    public Genre(string newNom)
    {
        this.Id = 0;
        this.Nom = newNom;
    }

    // ====== ACCESSEURS ======

    public string GetNom()
    {
        return this.Nom;
    }

    public void SetNom(string newNom)
    {
        this.Nom = newNom;
    }

    // ====== METHODES ======

    public static void CreerGenre(MySqlConnection connection, Genre genre)
    {
        string query = "INSERT INTO genre(nom) VALUES (@nom)";

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@nom", genre.Nom);
            cmd.ExecuteNonQuery();
        }
    }

    public static List<Genre> GetGenres(MySqlConnection connection)
    {
        List<Genre> liste = new List<Genre>();

        string query = "SELECT * FROM genre";

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        using (MySqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                Genre genre = new Genre();

                genre.Id = reader.GetInt32("id_genre");
                genre.Nom = reader.GetString("nom");

                liste.Add(genre);
            }
        }

        return liste;
    }

    public static void UpdateGenre(MySqlConnection connection, Genre genre)
    {
        string query = "UPDATE genre SET nom=@nom WHERE id_genre=@id";

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@id", genre.Id);
            cmd.Parameters.AddWithValue("@nom", genre.Nom);

            cmd.ExecuteNonQuery();
        }
    }

    public static void DeleteGenre(MySqlConnection connection, int id)
    {
        string query = "DELETE FROM genre WHERE id_genre=@id";

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}