using System;
using System.Drawing;
using System.Windows.Forms;

namespace Conception_SAV
{
  public class FormAdmin : Form
  {
    private DataGridView tableauTickets;

    private ComboBox comboFiltreStatut;
    private ComboBox comboFiltreType;
    private ComboBox comboStatut;
    private ComboBox comboSolution;
    private ComboBox comboDecision;

    private TextBox textCommentaire;

    public FormAdmin()
    {
      // FENETRE
      Text = "SAV+ - Administration";
      ClientSize = new Size(1050, 680);
      StartPosition = FormStartPosition.CenterScreen;
      FormBorderStyle = FormBorderStyle.FixedSingle;
      MaximizeBox = false;

      BackColor = Color.FromArgb(245, 247, 250);
      Font = new Font("Segoe UI", 9);

      // TITRE
      Label titre = new Label();
      titre.Text = "Tableau de bord SAV";
      titre.Font = new Font("Segoe UI", 21, FontStyle.Bold);
      titre.ForeColor = Color.FromArgb(35, 45, 65);
      titre.AutoSize = true;
      titre.Location = new Point(30, 18);

      Label sousTitre = new Label();
      sousTitre.Text = "Gestion et suivi des demandes clients";
      sousTitre.ForeColor = Color.Gray;
      sousTitre.AutoSize = true;
      sousTitre.Location = new Point(33, 55);

      // FILTRE STATUT
      Label labelFiltreStatut = new Label();
      labelFiltreStatut.Text = "Statut :";
      labelFiltreStatut.Location = new Point(30, 95);
      labelFiltreStatut.AutoSize = true;

      comboFiltreStatut = new ComboBox();
      comboFiltreStatut.Location = new Point(90, 91);
      comboFiltreStatut.Width = 150;
      comboFiltreStatut.DropDownStyle =
          ComboBoxStyle.DropDownList;

      comboFiltreStatut.Items.AddRange(new object[]
      {
                "Tous", "Ouvert", "En cours", "Résolu", "Fermé"
      });

      comboFiltreStatut.SelectedIndex = 0;
      comboFiltreStatut.SelectedIndexChanged += Filtres_Changed;

      // FILTRE TYPE
      Label labelFiltreType = new Label();
      labelFiltreType.Text = "Type :";
      labelFiltreType.Location = new Point(270, 95);
      labelFiltreType.AutoSize = true;

      comboFiltreType = new ComboBox();
      comboFiltreType.Location = new Point(320, 91);
      comboFiltreType.Width = 160;
      comboFiltreType.DropDownStyle =
          ComboBoxStyle.DropDownList;

      comboFiltreType.Items.AddRange(new object[]
      {
                "Tous", "Remboursement", "Assistance", "Question"
      });

      comboFiltreType.SelectedIndex = 0;
      comboFiltreType.SelectedIndexChanged += Filtres_Changed;

      // TABLEAU
      tableauTickets = new DataGridView();
      tableauTickets.Location = new Point(25, 130);
      tableauTickets.Size = new Size(1000, 265);

      tableauTickets.AutoSizeColumnsMode =
          DataGridViewAutoSizeColumnsMode.Fill;

      tableauTickets.ReadOnly = true;
      tableauTickets.AllowUserToAddRows = false;
      tableauTickets.SelectionMode =
          DataGridViewSelectionMode.FullRowSelect;

      tableauTickets.MultiSelect = false;
      tableauTickets.BackgroundColor = Color.White;
      tableauTickets.BorderStyle = BorderStyle.None;
      tableauTickets.RowHeadersVisible = false;

      tableauTickets.ColumnHeadersDefaultCellStyle.BackColor =
          Color.FromArgb(35, 50, 75);

      tableauTickets.ColumnHeadersDefaultCellStyle.ForeColor =
          Color.White;

      tableauTickets.ColumnHeadersDefaultCellStyle.Font =
          new Font("Segoe UI", 9, FontStyle.Bold);

      tableauTickets.ColumnHeadersHeight = 35;

      tableauTickets.Columns.Add("Id", "N° Ticket");
      tableauTickets.Columns.Add("UserId", "Utilisateur");
      tableauTickets.Columns.Add("CommandeId", "Commande");
      tableauTickets.Columns.Add("TypeDemande", "Type");
      tableauTickets.Columns.Add("Description", "Description");
      tableauTickets.Columns.Add("Statut", "Statut");
      tableauTickets.Columns.Add("DateCreation", "Date");

      // GROUPE STATUT
      GroupBox groupeStatut = new GroupBox();
      groupeStatut.Text = "Gestion du statut";
      groupeStatut.Location = new Point(25, 415);
      groupeStatut.Size = new Size(460, 150);

      Label labelStatut = new Label();
      labelStatut.Text = "Nouveau statut :";
      labelStatut.Location = new Point(15, 42);
      labelStatut.AutoSize = true;

      comboStatut = new ComboBox();
      comboStatut.Location = new Point(125, 37);
      comboStatut.Width = 145;
      comboStatut.DropDownStyle =
          ComboBoxStyle.DropDownList;

      comboStatut.Items.AddRange(new object[]
      {
                "Ouvert", "En cours", "Résolu", "Fermé"
      });

      comboStatut.SelectedIndex = 0;

      Button boutonModifierStatut = new Button();
      boutonModifierStatut.Text = "Modifier";
      boutonModifierStatut.Location = new Point(285, 35);
      boutonModifierStatut.Size = new Size(140, 35);
      boutonModifierStatut.BackColor =
          Color.FromArgb(47, 105, 210);
      boutonModifierStatut.ForeColor = Color.White;
      boutonModifierStatut.FlatStyle = FlatStyle.Flat;
      boutonModifierStatut.FlatAppearance.BorderSize = 0;

      boutonModifierStatut.Click +=
          BoutonModifierStatut_Click;

      groupeStatut.Controls.Add(labelStatut);
      groupeStatut.Controls.Add(comboStatut);
      groupeStatut.Controls.Add(boutonModifierStatut);

      // GROUPE SOLUTION SAV
      GroupBox groupeSolution = new GroupBox();
      groupeSolution.Text = "Traitement SAV";
      groupeSolution.Location = new Point(505, 415);
      groupeSolution.Size = new Size(520, 150);

      Label labelSolution = new Label();
      labelSolution.Text = "Solution :";
      labelSolution.Location = new Point(15, 35);
      labelSolution.AutoSize = true;

      comboSolution = new ComboBox();
      comboSolution.Location = new Point(85, 30);
      comboSolution.Width = 140;
      comboSolution.DropDownStyle =
          ComboBoxStyle.DropDownList;

      comboSolution.Items.AddRange(new object[]
      {
                "Réparation", "Échange", "Remboursement"
      });

      comboSolution.SelectedIndex = 0;

      Label labelDecision = new Label();
      labelDecision.Text = "Décision :";
      labelDecision.Location = new Point(240, 35);
      labelDecision.AutoSize = true;

      comboDecision = new ComboBox();
      comboDecision.Location = new Point(315, 30);
      comboDecision.Width = 170;
      comboDecision.DropDownStyle =
          ComboBoxStyle.DropDownList;

      comboDecision.Items.AddRange(new object[]
      {
                "Accepté", "Refusé"
      });

      Label labelCommentaire = new Label();
      labelCommentaire.Text = "Commentaire :";
      labelCommentaire.Location = new Point(15, 75);
      labelCommentaire.AutoSize = true;

      textCommentaire = new TextBox();
      textCommentaire.Location = new Point(110, 72);
      textCommentaire.Size = new Size(375, 32);
      textCommentaire.Multiline = true;

      Button boutonModifierSolution = new Button();
      boutonModifierSolution.Text = "Enregistrer la solution";
      boutonModifierSolution.Location = new Point(145, 112);
      boutonModifierSolution.Size = new Size(210, 28);
      boutonModifierSolution.BackColor =
          Color.FromArgb(70, 160, 120);
      boutonModifierSolution.ForeColor = Color.White;
      boutonModifierSolution.FlatStyle = FlatStyle.Flat;
      boutonModifierSolution.FlatAppearance.BorderSize = 0;

      boutonModifierSolution.Click +=
          BoutonModifierSolution_Click;

      groupeSolution.Controls.Add(labelSolution);
      groupeSolution.Controls.Add(comboSolution);
      groupeSolution.Controls.Add(labelDecision);
      groupeSolution.Controls.Add(comboDecision);
      groupeSolution.Controls.Add(labelCommentaire);
      groupeSolution.Controls.Add(textCommentaire);
      groupeSolution.Controls.Add(boutonModifierSolution);

      // BOUTON MESSAGES
      Button boutonMessages = new Button();
      boutonMessages.Text = "Voir les messages";
      boutonMessages.Size = new Size(190, 40);
      boutonMessages.Location = new Point(340, 610);
      boutonMessages.BackColor =
          Color.FromArgb(47, 105, 210);
      boutonMessages.ForeColor = Color.White;
      boutonMessages.FlatStyle = FlatStyle.Flat;
      boutonMessages.FlatAppearance.BorderSize = 0;
      boutonMessages.Click += BoutonMessages_Click;

      // BOUTON DECONNEXION
      Button boutonDeconnexion = new Button();
      boutonDeconnexion.Text = "Se déconnecter";
      boutonDeconnexion.Size = new Size(190, 40);
      boutonDeconnexion.Location = new Point(550, 610);
      boutonDeconnexion.BackColor =
          Color.FromArgb(220, 223, 228);
      boutonDeconnexion.ForeColor =
          Color.FromArgb(50, 55, 65);
      boutonDeconnexion.FlatStyle = FlatStyle.Flat;
      boutonDeconnexion.FlatAppearance.BorderSize = 0;
      boutonDeconnexion.Click +=
          BoutonDeconnexion_Click;

      // AJOUT DES ELEMENTS
      Controls.Add(titre);
      Controls.Add(sousTitre);

      Controls.Add(labelFiltreStatut);
      Controls.Add(comboFiltreStatut);

      Controls.Add(labelFiltreType);
      Controls.Add(comboFiltreType);

      Controls.Add(tableauTickets);
      Controls.Add(groupeStatut);
      Controls.Add(groupeSolution);

      Controls.Add(boutonMessages);
      Controls.Add(boutonDeconnexion);

      ChargerTickets();
    }

    // FILTRES
    private void Filtres_Changed(
        object? sender, EventArgs e)
    {
      ChargerTickets();
    }

    // CHARGER LES TICKETS
    private void ChargerTickets()
    {
      tableauTickets.Rows.Clear();

      var tickets = TicketRepository.GetTickets();

      foreach (var ticket in tickets)
      {
        bool statutValide =
            comboFiltreStatut.Text == "Tous" ||
            ticket.Statut == comboFiltreStatut.Text;

        bool typeValide =
            comboFiltreType.Text == "Tous" ||
            ticket.TypeDemande == comboFiltreType.Text;

        if (statutValide && typeValide)
        {
          tableauTickets.Rows.Add(
              ticket.Id,
              ticket.UserId,
              ticket.CommandeId,
              ticket.TypeDemande,
              ticket.Description,
              ticket.Statut,
              ticket.DateCreation.ToString("dd/MM/yyyy HH:mm")
          );
        }
      }
    }

    // MODIFIER LE STATUT
    private void BoutonModifierStatut_Click(
        object? sender, EventArgs e)
    {
      if (tableauTickets.SelectedRows.Count == 0)
      {
        MessageBox.Show("Sélectionnez un ticket.");
        return;
      }

      int ticketId = Convert.ToInt32(
          tableauTickets.SelectedRows[0].Cells["Id"].Value);

      TicketRepository.ModifierStatut(
          ticketId, comboStatut.Text);

      MessageBox.Show("Statut modifié avec succès.");

      ChargerTickets();
    }

    // MODIFIER LA SOLUTION
    private void BoutonModifierSolution_Click(
        object? sender, EventArgs e)
    {
      if (tableauTickets.SelectedRows.Count == 0)
      {
        MessageBox.Show("Sélectionnez un ticket.");
        return;
      }

      int ticketId = Convert.ToInt32(
          tableauTickets.SelectedRows[0].Cells["Id"].Value);

      string solution = comboSolution.Text;
      string? decision = null;

      if (solution == "Remboursement")
      {
        if (string.IsNullOrWhiteSpace(comboDecision.Text))
        {
          MessageBox.Show(
              "Choisissez une décision de remboursement.");
          return;
        }

        decision = comboDecision.Text;
      }

      string? commentaire =
          string.IsNullOrWhiteSpace(textCommentaire.Text)
          ? null
          : textCommentaire.Text;

      TicketRepository.ModifierSolution(
          ticketId, solution, decision, commentaire);

      MessageBox.Show("Solution enregistrée avec succès.");

      textCommentaire.Clear();
      ChargerTickets();
    }

    // VOIR LES MESSAGES
    private void BoutonMessages_Click(
        object? sender, EventArgs e)
    {
      if (tableauTickets.SelectedRows.Count == 0)
      {
        MessageBox.Show("Sélectionnez un ticket.");
        return;
      }

      int ticketId = Convert.ToInt32(
          tableauTickets.SelectedRows[0].Cells["Id"].Value);

      using (FormMessagesTicket formulaire =
          new FormMessagesTicket(ticketId))
      {
        formulaire.ShowDialog();
      }
    }

    // DECONNEXION
    private void BoutonDeconnexion_Click(
        object? sender, EventArgs e)
    {
      DialogResult resultat = MessageBox.Show(
          "Voulez-vous vraiment vous déconnecter ?",
          "Déconnexion",
          MessageBoxButtons.YesNo,
          MessageBoxIcon.Question);

      if (resultat == DialogResult.Yes)
      {
        Close();
      }
    }
  }
}
