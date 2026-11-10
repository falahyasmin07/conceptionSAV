using System;

namespace Conception_SAV
{
  public class MessageTicket
  {
    public int Id { get; set; }
    public int TicketId { get; set; }
    public int UserId { get; set; }
    public string Message { get; set; } = "";
    public DateTime DateMessage { get; set; }
  }
}
