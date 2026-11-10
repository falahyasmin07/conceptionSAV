using System;

namespace Conception_SAV
{
  public class Ticket
  {
    public int Id { get; set; }

    public int UserId { get; set; }

    public int CommandeId { get; set; }

    public string TypeDemande { get; set; }

    public string Description { get; set; }

    public string Statut { get; set; }

    public string? SolutionSAV { get; set; }

    public string? DecisionRemboursement { get; set; }

    public string? CommentaireAdmin { get; set; }

    public DateTime DateCreation { get; set; }

    public DateTime? DateModification { get; set; }
  }
}
