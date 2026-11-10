using System;
using System.Security.Cryptography;
using Microsoft.Data.Sqlite;

namespace Conception_SAV
{
  public class UserRepository
  {
    // INSCRIPTION D'UN NOUVEL UTILISATEUR
    public static bool Inscrire(
        string identifiant,
        string motDePasse,
        out string message)
    {
      identifiant = identifiant.Trim();

      if (identifiant.Length < 3)
      {
        message = "L'identifiant doit contenir au moins 3 caractères.";
        return false;
      }

      if (motDePasse.Length < 8)
      {
        message = "Le mot de passe doit contenir au moins 8 caractères.";
        return false;
      }

      // Création d'un sel aléatoire
      byte[] sel = RandomNumberGenerator.GetBytes(16);

      // Hachage du mot de passe
      byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
          motDePasse,
          sel,
          210000,
          HashAlgorithmName.SHA256,
          32);

      using (var connection = Database.GetConnection())
      {
        connection.Open();

        string sql = @"
                    INSERT INTO Utilisateurs
                    (
                        Identifiant,
                        MotDePasseHash,
                        SelMotDePasse,
                        Role,
                        DateCreation
                    )
                    VALUES
                    (
                        @Identifiant,
                        @Hash,
                        @Sel,
                        'Client',
                        @DateCreation
                    )";

        using (var command = new SqliteCommand(sql, connection))
        {
          command.Parameters.AddWithValue(
              "@Identifiant", identifiant);

          command.Parameters.AddWithValue(
              "@Hash", Convert.ToBase64String(hash));

          command.Parameters.AddWithValue(
              "@Sel", Convert.ToBase64String(sel));

          command.Parameters.AddWithValue(
              "@DateCreation",
              DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

          try
          {
            command.ExecuteNonQuery();

            message = "Compte créé avec succès !";
            return true;
          }
          catch (SqliteException ex)
          {
            if (ex.SqliteErrorCode == 19)
            {
              message = "Cet identifiant existe déjà.";
            }
            else
            {
              message = "Erreur lors de la création du compte : "
                  + ex.Message;
            }

            return false;
          }
        }
      }
    }

    // VERIFICATION DE LA CONNEXION
    public static string? VerifierConnexion(
        string identifiant,
        string motDePasse)
    {
      using (var connection = Database.GetConnection())
      {
        connection.Open();

        string sql = @"
                    SELECT MotDePasseHash, SelMotDePasse, Role
                    FROM Utilisateurs
                    WHERE Identifiant = @Identifiant";

        using (var command = new SqliteCommand(sql, connection))
        {
          command.Parameters.AddWithValue(
              "@Identifiant",
              identifiant.Trim());

          using (var reader = command.ExecuteReader())
          {
            if (!reader.Read())
            {
              return null;
            }

            byte[] hashEnregistre =
                Convert.FromBase64String(
                    reader["MotDePasseHash"].ToString()!);

            byte[] sel =
                Convert.FromBase64String(
                    reader["SelMotDePasse"].ToString()!);

            string role = reader["Role"].ToString()!;

            byte[] hashSaisi =
                Rfc2898DeriveBytes.Pbkdf2(
                    motDePasse,
                    sel,
                    210000,
                    HashAlgorithmName.SHA256,
                    32);

            if (CryptographicOperations.FixedTimeEquals(
                hashEnregistre,
                hashSaisi))
            {
              return role;
            }

            return null;
          }
        }
      }
    }

    // RECUPERATION DE L'ID DE L'UTILISATEUR
    public static int? ObtenirIdUtilisateur(string identifiant)
    {
      using (var connection = Database.GetConnection())
      {
        connection.Open();

        string sql = @"
                    SELECT Id
                    FROM Utilisateurs
                    WHERE Identifiant = @Identifiant";

        using (var command = new SqliteCommand(sql, connection))
        {
          command.Parameters.AddWithValue(
              "@Identifiant",
              identifiant.Trim());

          var resultat = command.ExecuteScalar();

          if (resultat == null || resultat == DBNull.Value)
          {
            return null;
          }

          return Convert.ToInt32(resultat);
        }
      }
    }
  }
}
