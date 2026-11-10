using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace Conception_SAV
{
  public class MessageTicketRepository
  {
    public static void AjouterMessage(MessageTicket message)
    {
      using (var connection = Database.GetConnection())
      {
        connection.Open();

        string sql = @"
                    INSERT INTO MessagesTicket
                    (
                        TicketId,
                        UserId,
                        Message,
                        DateMessage
                    )
                    VALUES
                    (
                        @TicketId,
                        @UserId,
                        @Message,
                        @DateMessage
                    )";

        using (var command = new SqliteCommand(sql, connection))
        {
          command.Parameters.AddWithValue("@TicketId", message.TicketId);
          command.Parameters.AddWithValue("@UserId", message.UserId);
          command.Parameters.AddWithValue("@Message", message.Message);
          command.Parameters.AddWithValue(
              "@DateMessage",
              message.DateMessage.ToString("yyyy-MM-dd HH:mm:ss"));

          command.ExecuteNonQuery();
        }
      }
    }

    public static List<MessageTicket> GetMessages(int ticketId)
    {
      var messages = new List<MessageTicket>();

      using (var connection = Database.GetConnection())
      {
        connection.Open();

        string sql = @"
                    SELECT *
                    FROM MessagesTicket
                    WHERE TicketId = @TicketId
                    ORDER BY DateMessage";

        using (var command = new SqliteCommand(sql, connection))
        {
          command.Parameters.AddWithValue("@TicketId", ticketId);

          using (var reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
              messages.Add(new MessageTicket
              {
                Id = Convert.ToInt32(reader["Id"]),
                TicketId = Convert.ToInt32(reader["TicketId"]),
                UserId = Convert.ToInt32(reader["UserId"]),
                Message = reader["Message"].ToString()!,
                DateMessage = DateTime.Parse(
                      reader["DateMessage"].ToString()!)
              });
            }
          }
        }
      }

      return messages;
    }
  }
}
