using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class Utilisateur
{
    // ====== ATTRIBUTS ======
    private int Id;
    private string Nom;
    private string Prenom;
    private string Pseudo;
    private string Email;
    private string MotDePasse;

    // ====== CONSTRUCTEURS ======
    public Utilisateur()
    {
        this.Id = 0;
        this.Nom = "";
        this.Prenom = "";
        this.Pseudo = "";
        this.Email = "";
        this.MotDePasse = "";
    }

    public Utilisateur(string newNom, string newPrenom, string newPseudo, string newEmail, string newMotDePasse)
    {
        this.Id = 0;
        this.Nom = newNom;
        this.Prenom = newPrenom;
        this.Pseudo = newPseudo;
        this.Email = newEmail;
        this.MotDePasse = newMotDePasse;
    }

    // ====== ACCESSEURS ======

    // Getters
    public int GetId()
    {
        return this.Id;
    }

    public string GetNom()
    {
        return this.Nom;
    }

    public string GetPrenom()
    {
        return this.Prenom;
    }

    public string GetPseudo()
    {
        return this.Pseudo;
    }

    public string GetEmail()
    {
        return this.Email;
    }

    public string GetMotDePasse()
    {
        return this.MotDePasse;
    }

    // Setters
    public void SetNom(string newNom)
    {
        this.Nom = newNom;
    }

    public void SetPrenom(string newPrenom)
    {
        this.Prenom = newPrenom;
    }

    public void SetEmail(string newEmail)
    {
        this.Email = newEmail;
    }

    public void SetMotDePasse(string newMotDePasse)
    {
        this.MotDePasse = newMotDePasse;
    }

    // ====== METHODES ======

    // Créer l'utilisateur dans la db
    public static void CreerUtilisateur(MySqlConnection connection, Utilisateur user)
    {
        string query = "INSERT INTO utilisateur(nom, prenom, pseudo, e_mail, mot_de_passe) " +
                       "VALUES (@nom, @prenom, @pseudo, @email, @mdp)";

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@nom", user.Nom);
            cmd.Parameters.AddWithValue("@prenom", user.Prenom);
            cmd.Parameters.AddWithValue("@pseudo", user.Pseudo);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@mdp", user.MotDePasse);

            cmd.ExecuteNonQuery();
        }

        user.Id = GetIdUtilisateurFromEmail(connection, user);
    }

    // Afficher tout le contenu de la db
    public static List<Utilisateur> GetUtilisateurs(MySqlConnection connection)
    {
        List<Utilisateur> liste = new List<Utilisateur>();

        string query = "SELECT * FROM utilisateur";

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        using (MySqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                Utilisateur user = new Utilisateur();

                user.Id = reader.GetInt32("id_utilisateur");
                user.Nom = reader.GetString("nom");
                user.Prenom = reader.GetString("prenom");
                user.Pseudo = reader.GetString("pseudo");
                user.Email = reader.GetString("e_mail");
                user.MotDePasse = reader.GetString("mot_de_passe");

                liste.Add(user);
            }
        }

        return liste;
    }

    // Mettre à jour un utilisateur de la db
    public static void UpdateUtilisateur(MySqlConnection connection, Utilisateur user)
    {
        string query = "UPDATE utilisateur SET nom=@nom, prenom=@prenom, pseudo=@pseudo, e_mail=@email, mot_de_passe=@mdp WHERE id_utilisateur=@id";

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@id", user.Id);
            cmd.Parameters.AddWithValue("@nom", user.Nom);
            cmd.Parameters.AddWithValue("@prenom", user.Prenom);
            cmd.Parameters.AddWithValue("@pseudo", user.Pseudo);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@mdp", user.MotDePasse);

            cmd.ExecuteNonQuery();
        }
    }

    // Supprimer un utilisateur de la db
    public static void DeleteUtilisateur(MySqlConnection connection, int id)
    {
        string query = "DELETE FROM utilisateur WHERE id_utilisateur=@id";

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }

    // Récupère l'id de l'utilisateur à partir de son email
    private static int GetIdUtilisateurFromEmail(MySqlConnection connection, Utilisateur user)
    {
        int UserId = 0;

        string query = "SELECT id_utilisateur FROM utilisateur WHERE e_mail = @email";

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@email", user.GetEmail());

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    UserId = reader.GetInt32("id_utilisateur");
                }
            }
        }

        return UserId;
    }
}