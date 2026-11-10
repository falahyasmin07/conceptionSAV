using System;
using System.Drawing;
using System.Windows.Forms;

namespace Conception_SAV
{
  public class FormListeTickets : Form
  {
    private Label titre;
    private DataGridView tableauTickets;
    private Button boutonMessages;
    private Button boutonFermer;

    public FormListeTickets()
    {
      // FENÊTRE
      Text = "Mes tickets SAV";
      Width = 1000;
      Height = 650;
      StartPosition = FormStartPosition.CenterScreen;

      BackColor = Color.FromArgb(245, 247, 250);
      Font = new Font("Segoe UI", 10);

      // TITRE
      titre = new Label();
      titre.Text = "Mes tickets SAV";
      titre.Font = new Font(
          "Segoe UI",
          24,
          FontStyle.Bold);

      titre.ForeColor = Color.FromArgb(35, 45, 65);
      titre.AutoSize = true;
      titre.Location = new Point(380, 30);

      // TABLEAU
      tableauTickets = new DataGridView();

      tableauTickets.Location = new Point(30, 100);
      tableauTickets.Width = 920;
      tableauTickets.Height = 400;

      tableauTickets.AutoSizeColumnsMode =
          DataGridViewAutoSizeColumnsMode.Fill;

      tableauTickets.ReadOnly = true;
      tableauTickets.AllowUserToAddRows = false;

      tableauTickets.SelectionMode =
          DataGridViewSelectionMode.FullRowSelect;

      tableauTickets.MultiSelect = false;

      tableauTickets.BackgroundColor = Color.White;
      tableauTickets.BorderStyle = BorderStyle.None;

      tableauTickets.ColumnHeadersDefaultCellStyle.BackColor =
          Color.FromArgb(45, 115, 210);

      tableauTickets.ColumnHeadersDefaultCellStyle.ForeColor =
          Color.White;

      tableauTickets.ColumnHeadersDefaultCellStyle.Font =
          new Font("Segoe UI", 10, FontStyle.Bold);

      tableauTickets.ColumnHeadersHeight = 40;

      tableauTickets.DefaultCellStyle.Font =
          new Font("Segoe UI", 9);

      tableauTickets.DefaultCellStyle.SelectionBackColor =
          Color.FromArgb(220, 235, 250);

      tableauTickets.DefaultCellStyle.SelectionForeColor =
          Color.FromArgb(35, 45, 65);

      // COLONNES
      tableauTickets.Columns.Add(
          "Id",
          "N° Ticket");

      tableauTickets.Columns.Add(
          "CommandeId",
          "Commande");

      tableauTickets.Columns.Add(
          "TypeDemande",
          "Type");

      tableauTickets.Columns.Add(
          "Description",
          "Description");

      tableauTickets.Columns.Add(
          "Statut",
          "Statut");

      tableauTickets.Columns.Add(
          "DateCreation",
          "Date de création");

      // BOUTON MESSAGES
      boutonMessages = new Button();
      boutonMessages.Text = "Voir les messages";
      boutonMessages.Width = 200;
      boutonMessages.Height = 50;
      boutonMessages.Location = new Point(320, 535);

      boutonMessages.BackColor =
          Color.FromArgb(45, 115, 210);

      boutonMessages.ForeColor = Color.White;

      boutonMessages.FlatStyle = FlatStyle.Flat;
      boutonMessages.FlatAppearance.BorderSize = 0;

      boutonMessages.Font =
          new Font("Segoe UI", 10, FontStyle.Bold);

      boutonMessages.Click +=
          BoutonMessages_Click;

      // BOUTON FERMER
      boutonFermer = new Button();
      boutonFermer.Text = "Fermer";
      boutonFermer.Width = 140;
      boutonFermer.Height = 50;
      boutonFermer.Location = new Point(540, 535);

      boutonFermer.BackColor =
          Color.FromArgb(220, 223, 228);

      boutonFermer.ForeColor =
          Color.FromArgb(50, 55, 65);

      boutonFermer.FlatStyle = FlatStyle.Flat;
      boutonFermer.FlatAppearance.BorderSize = 0;

      boutonFermer.Font =
          new Font("Segoe UI", 10, FontStyle.Bold);

      boutonFermer.Click +=
          BoutonFermer_Click;

      // AJOUT
      Controls.Add(titre);
      Controls.Add(tableauTickets);
      Controls.Add(boutonMessages);
      Controls.Add(boutonFermer);

      ChargerTickets();
    }

    private void ChargerTickets()
    {
      tableauTickets.Rows.Clear();

      var tickets = TicketRepository.GetTickets();

      foreach (var ticket in tickets)
      {
        int ligne = tableauTickets.Rows.Add(
            ticket.Id,
            ticket.CommandeId,
            ticket.TypeDemande,
            ticket.Description,
            ticket.Statut,
            ticket.DateCreation.ToString(
                "dd/MM/yyyy HH:mm")
        );

        // Couleur du statut
        if (ticket.Statut == "Ouvert")
        {
          tableauTickets.Rows[ligne]
              .Cells["Statut"]
              .Style.ForeColor =
              Color.FromArgb(45, 115, 210);
        }
        else if (ticket.Statut == "En cours")
        {
          tableauTickets.Rows[ligne]
              .Cells["Statut"]
              .Style.ForeColor =
              Color.FromArgb(230, 150, 40);
        }
        else if (ticket.Statut == "Résolu")
        {
          tableauTickets.Rows[ligne]
              .Cells["Statut"]
              .Style.ForeColor =
              Color.FromArgb(40, 150, 100);
        }
        else if (ticket.Statut == "Fermé")
        {
          tableauTickets.Rows[ligne]
              .Cells["Statut"]
              .Style.ForeColor =
              Color.FromArgb(120, 120, 120);
        }
      }
    }

    private void BoutonMessages_Click(
        object? sender,
        EventArgs e)
    {
      if (tableauTickets.SelectedRows.Count == 0)
      {
        MessageBox.Show(
            "Sélectionnez un ticket.",
            "Information",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        return;
      }

      int ticketId = Convert.ToInt32(
          tableauTickets.SelectedRows[0]
              .Cells["Id"].Value);

      FormMessagesTicket formulaire =
          new FormMessagesTicket(ticketId);

      formulaire.ShowDialog();
    }

    private void BoutonFermer_Click(
        object? sender,
        EventArgs e)
    {
      Close();
    }
  }
}
