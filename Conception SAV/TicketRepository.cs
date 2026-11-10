using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace Conception_SAV
{
  public class TicketRepository
  {
    // AJOUTER UN TICKET
    public static void AjouterTicket(Ticket ticket)
    {
      using (var connection = Database.GetConnection())
      {
        connection.Open();

        string sql = @"
                    INSERT INTO Tickets
                    (
                        UserId,
                        CommandeId,
                        TypeDemande,
                        Description,
                        Statut,
                        SolutionSAV,
                        DecisionRemboursement,
                        CommentaireAdmin,
                        DateCreation
                    )
                    VALUES
                    (
                        @UserId,
                        @CommandeId,
                        @TypeDemande,
                        @Description,
                        @Statut,
                        @SolutionSAV,
                        @DecisionRemboursement,
                        @CommentaireAdmin,
                        @DateCreation
                    )";

        using (var command = new SqliteCommand(sql, connection))
        {
          command.Parameters.AddWithValue("@UserId", ticket.UserId);
          command.Parameters.AddWithValue("@CommandeId", ticket.CommandeId);
          command.Parameters.AddWithValue("@TypeDemande", ticket.TypeDemande);
          command.Parameters.AddWithValue("@Description", ticket.Description);
          command.Parameters.AddWithValue("@Statut", ticket.Statut);

          command.Parameters.AddWithValue(
              "@SolutionSAV",
              (object?)ticket.SolutionSAV ?? DBNull.Value);

          command.Parameters.AddWithValue(
              "@DecisionRemboursement",
              (object?)ticket.DecisionRemboursement ?? DBNull.Value);

          command.Parameters.AddWithValue(
              "@CommentaireAdmin",
              (object?)ticket.CommentaireAdmin ?? DBNull.Value);

          command.Parameters.AddWithValue(
              "@DateCreation",
              ticket.DateCreation.ToString("yyyy-MM-dd HH:mm:ss"));

          command.ExecuteNonQuery();
        }
      }
    }

    // AFFICHER TOUS LES TICKETS POUR L'ADMINISTRATEUR
    public static List<Ticket> GetTickets()
    {
      var tickets = new List<Ticket>();

      using (var connection = Database.GetConnection())
      {
        connection.Open();

        string sql = @"
                    SELECT *
                    FROM Tickets
                    ORDER BY DateCreation DESC";

        using (var command = new SqliteCommand(sql, connection))
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            tickets.Add(new Ticket
            {
              Id = Convert.ToInt32(reader["Id"]),
              UserId = Convert.ToInt32(reader["UserId"]),
              CommandeId = Convert.ToInt32(reader["CommandeId"]),
              TypeDemande = reader["TypeDemande"].ToString()!,
              Description = reader["Description"].ToString()!,
              Statut = reader["Statut"].ToString()!,
              DateCreation = DateTime.Parse(
                    reader["DateCreation"].ToString()!)
            });
          }
        }
      }

      return tickets;
    }

    // AFFICHER LES TICKETS D'UN CLIENT
    public static List<Ticket> GetTicketsParUtilisateur(int userId)
    {
      var tickets = new List<Ticket>();

      using (var connection = Database.GetConnection())
      {
        connection.Open();

        string sql = @"
                    SELECT *
                    FROM Tickets
                    WHERE UserId = @UserId
                    ORDER BY DateCreation DESC";

        using (var command = new SqliteCommand(sql, connection))
        {
          command.Parameters.AddWithValue("@UserId", userId);

          using (var reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
              tickets.Add(new Ticket
              {
                Id = Convert.ToInt32(reader["Id"]),
                UserId = Convert.ToInt32(reader["UserId"]),
                CommandeId = Convert.ToInt32(reader["CommandeId"]),
                TypeDemande = reader["TypeDemande"].ToString()!,
                Description = reader["Description"].ToString()!,
                Statut = reader["Statut"].ToString()!,
                DateCreation = DateTime.Parse(
                      reader["DateCreation"].ToString()!)
              });
            }
          }
        }
      }

      return tickets;
    }

    // MODIFIER LE STATUT D'UN TICKET
    public static void ModifierStatut(
        int ticketId,
        string nouveauStatut)
    {
      using (var connection = Database.GetConnection())
      {
        connection.Open();

        string sql = @"
                    UPDATE Tickets
                    SET Statut = @Statut,
                        DateModification = @DateModification
                    WHERE Id = @Id";

        using (var command = new SqliteCommand(sql, connection))
        {
          command.Parameters.AddWithValue("@Statut", nouveauStatut);

          command.Parameters.AddWithValue(
              "@DateModification",
              DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

          command.Parameters.AddWithValue("@Id", ticketId);

          command.ExecuteNonQuery();
        }
      }
    }

    // MODIFIER LA SOLUTION SAV
    public static void ModifierSolution(
        int ticketId,
        string solution,
        string? decisionRemboursement,
        string? commentaireAdmin)
    {
      using (var connection = Database.GetConnection())
      {
        connection.Open();

        string sql = @"
                    UPDATE Tickets
                    SET SolutionSAV = @SolutionSAV,
                        DecisionRemboursement = @DecisionRemboursement,
                        CommentaireAdmin = @CommentaireAdmin,
                        DateModification = @DateModification
                    WHERE Id = @Id";

        using (var command = new SqliteCommand(sql, connection))
        {
          command.Parameters.AddWithValue("@SolutionSAV", solution);

          command.Parameters.AddWithValue(
              "@DecisionRemboursement",
              (object?)decisionRemboursement ?? DBNull.Value);

          command.Parameters.AddWithValue(
              "@CommentaireAdmin",
              (object?)commentaireAdmin ?? DBNull.Value);

          command.Parameters.AddWithValue(
              "@DateModification",
              DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

          command.Parameters.AddWithValue("@Id", ticketId);

          command.ExecuteNonQuery();
        }
      }
    }
  }
}

