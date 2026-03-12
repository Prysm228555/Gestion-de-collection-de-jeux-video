using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class Jeu
{
    // ====== ATTRIBUTS ======
    private int Id;
    private string Titre;
    private int AnneeSortie;
    private DateTime DateEmprunt;
    private DateTime DateRetourPrevu;
    private DateTime DateRetourReel;
    private int IdGenre;
    private int IdPlateforme;
    private int IdEditeur;
    private int IdUtilisateur;
    private int IdUtilisateurRetour;

    // ====== CONSTRUCTEURS ======
    public Jeu()
    {
        this.Id = 0;
        this.Titre = "";
        this.AnneeSortie = 0;
        this.DateEmprunt = DateTime.MinValue;
        this.DateRetourPrevu = DateTime.MinValue;
        this.DateRetourReel = DateTime.MinValue;
        this.IdGenre = 0;
        this.IdPlateforme = 0;
        this.IdEditeur = 0;
        this.IdUtilisateur = 0;
        this.IdUtilisateurRetour = 0;
    }

    public Jeu(string titre, int annee, int genre, int plateforme, int editeur)
    {
        this.Id = 0;
        this.Titre = titre;
        this.AnneeSortie = annee;
        this.DateEmprunt = DateTime.MinValue;
        this.DateRetourPrevu = DateTime.MinValue;
        this.DateRetourReel = DateTime.MinValue;
        this.IdGenre = genre;
        this.IdPlateforme = plateforme;
        this.IdEditeur = editeur;
        this.IdUtilisateur = 0;
        this.IdUtilisateurRetour = 0;
    }

    // ====== ACCESSEURS ======

    public int GetId()
    {
        return this.Id;
    }

    public string GetTitre()
    {
        return this.Titre;
    }

    public int GetAnneeSortie()
    {
        return this.AnneeSortie;
    }

    public int GetGenre()
    {
        return this.IdGenre;
    }

    public int GetPlateforme()
    {
        return this.IdPlateforme;
    }

    public int GetEditeur()
    {
        return this.IdEditeur;
    }

    // ====== METHODES ======

    // Créer un jeu dans la db
    public static void CreerJeu(MySqlConnection connection, Jeu jeu)
    {
        string query = "INSERT INTO jeu(titre, annee_sortie, id_genre, id_plateforme, id_editeur) " +
                       "VALUES (@titre, @annee, @genre, @plateforme, @editeur)";

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@titre", jeu.Titre);
            cmd.Parameters.AddWithValue("@annee", jeu.AnneeSortie);
            cmd.Parameters.AddWithValue("@genre", jeu.IdGenre);
            cmd.Parameters.AddWithValue("@plateforme", jeu.IdPlateforme);
            cmd.Parameters.AddWithValue("@editeur", jeu.IdEditeur);

            cmd.ExecuteNonQuery();
        }

        jeu.Id = GetIdJeuFromTitre(connection, jeu);
    }

    // Afficher tous les jeux
    public static List<Jeu> GetJeux(MySqlConnection connection)
    {
        List<Jeu> liste = new List<Jeu>();

        string query = "SELECT * FROM jeu";

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        using (MySqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                Jeu jeu = new Jeu();

                jeu.Id = reader.GetInt32("id_jeu");
                jeu.Titre = reader.GetString("titre");
                jeu.AnneeSortie = reader.GetInt32("annee_sortie");
                jeu.IdGenre = reader.GetInt32("id_genre");
                jeu.IdPlateforme = reader.GetInt32("id_plateforme");
                jeu.IdEditeur = reader.GetInt32("id_editeur");

                liste.Add(jeu);
            }
        }

        return liste;
    }

    // Mettre à jour un jeu
    public static void UpdateJeu(MySqlConnection connection, Jeu jeu)
    {
        string query = "UPDATE jeu SET titre=@titre, annee_sortie=@annee, id_genre=@genre, id_plateforme=@plateforme, id_editeur=@editeur WHERE id_jeu=@id";

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@id", jeu.Id);
            cmd.Parameters.AddWithValue("@titre", jeu.Titre);
            cmd.Parameters.AddWithValue("@annee", jeu.AnneeSortie);
            cmd.Parameters.AddWithValue("@genre", jeu.IdGenre);
            cmd.Parameters.AddWithValue("@plateforme", jeu.IdPlateforme);
            cmd.Parameters.AddWithValue("@editeur", jeu.IdEditeur);

            cmd.ExecuteNonQuery();
        }
    }

    // Supprimer un jeu
    public static void DeleteJeu(MySqlConnection connection, int id)
    {
        string query = "DELETE FROM jeu WHERE id_jeu=@id";

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }

    // Récupérer l'id du jeu via son titre
    private static int GetIdJeuFromTitre(MySqlConnection connection, Jeu jeu)
    {
        int JeuId = 0;

        string query = "SELECT id_jeu FROM jeu WHERE titre = @titre";

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@titre", jeu.GetTitre());

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    JeuId = reader.GetInt32("id_jeu");
                }
            }
        }

        return JeuId;
    }
}