using Microsoft.Data.Sqlite;

namespace Conception_SAV
{
  public class DatabaseInitializer
  {
    public static void Initialize()
    {
      using (var connection = Database.GetConnection())
      {
        connection.Open();

        string sql = @"
                    CREATE TABLE IF NOT EXISTS Tickets (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserId INTEGER NOT NULL,
                        CommandeId INTEGER NOT NULL,
                        TypeDemande TEXT NOT NULL,
                        Description TEXT NOT NULL,
                        Statut TEXT NOT NULL,
                        SolutionSAV TEXT,
                        DecisionRemboursement TEXT,
                        CommentaireAdmin TEXT,
                        DateCreation TEXT NOT NULL,
                        DateModification TEXT
                    );

                    CREATE TABLE IF NOT EXISTS MessagesTicket (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        TicketId INTEGER NOT NULL,
                        UserId INTEGER NOT NULL,
                        Message TEXT NOT NULL,
                        DateMessage TEXT NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS HistoriqueTickets (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        TicketId INTEGER NOT NULL,
                        Action TEXT NOT NULL,
                        AncienneValeur TEXT,
                        NouvelleValeur TEXT,
                        DateAction TEXT NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS Utilisateurs (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Identifiant TEXT NOT NULL COLLATE NOCASE UNIQUE,
                        MotDePasseHash TEXT NOT NULL,
                        SelMotDePasse TEXT NOT NULL,
                        Role TEXT NOT NULL DEFAULT 'Client',
                        DateCreation TEXT NOT NULL
                    );
                ";

        using (var command = new SqliteCommand(sql, connection))
        {
          command.ExecuteNonQuery();
        }
      }
    }
  }
}
