using System;
using System.Drawing;
using System.Windows.Forms;

namespace Conception_SAV
{
  public class FormTicket : Form
  {
    private Label titre;
    private Label sousTitre;

    private Label labelCommande;
    private TextBox textCommande;

    private Label labelType;
    private ComboBox comboType;

    private Label labelDescription;
    private TextBox textDescription;

    private Button boutonEnregistrer;
    private Button boutonAnnuler;

    public FormTicket()
    {
      // FENÊTRE
      Text = "Créer un ticket SAV";
      Width = 700;
      Height = 600;
      StartPosition = FormStartPosition.CenterScreen;

      BackColor = Color.FromArgb(245, 247, 250);
      Font = new Font("Segoe UI", 10);

      // TITRE
      titre = new Label();
      titre.Text = "Créer un ticket SAV";
      titre.Font = new Font("Segoe UI", 24, FontStyle.Bold);
      titre.ForeColor = Color.FromArgb(35, 45, 65);
      titre.AutoSize = true;
      titre.Location = new Point(220, 35);

      // SOUS-TITRE
      sousTitre = new Label();
      sousTitre.Text = "Décrivez votre demande au service après-vente";
      sousTitre.Font = new Font("Segoe UI", 10);
      sousTitre.ForeColor = Color.FromArgb(100, 110, 125);
      sousTitre.AutoSize = true;
      sousTitre.Location = new Point(150, 80);

      // NUMÉRO DE COMMANDE
      labelCommande = new Label();
      labelCommande.Text = "Numéro de commande";
      labelCommande.Font = new Font("Segoe UI", 10, FontStyle.Bold);
      labelCommande.AutoSize = true;
      labelCommande.Location = new Point(80, 135);

      textCommande = new TextBox();
      textCommande.Location = new Point(80, 165);
      textCommande.Width = 230;
      textCommande.Font = new Font("Segoe UI", 11);

      // TYPE DE DEMANDE
      labelType = new Label();
      labelType.Text = "Type de demande";
      labelType.Font = new Font("Segoe UI", 10, FontStyle.Bold);
      labelType.AutoSize = true;
      labelType.Location = new Point(360, 135);

      comboType = new ComboBox();
      comboType.Location = new Point(360, 165);
      comboType.Width = 230;
      comboType.Font = new Font("Segoe UI", 11);
      comboType.DropDownStyle = ComboBoxStyle.DropDownList;

      comboType.Items.Add("Remboursement");
      comboType.Items.Add("Assistance");
      comboType.Items.Add("Question");
      comboType.SelectedIndex = 0;

      // DESCRIPTION
      labelDescription = new Label();
      labelDescription.Text = "Description de votre demande";
      labelDescription.Font = new Font("Segoe UI", 10, FontStyle.Bold);
      labelDescription.AutoSize = true;
      labelDescription.Location = new Point(80, 225);

      textDescription = new TextBox();
      textDescription.Location = new Point(80, 255);
      textDescription.Width = 510;
      textDescription.Height = 130;
      textDescription.Multiline = true;
      textDescription.Font = new Font("Segoe UI", 11);
      textDescription.ScrollBars = ScrollBars.Vertical;

      // BOUTON ENREGISTRER
      boutonEnregistrer = new Button();
      boutonEnregistrer.Text = "Enregistrer le ticket";
      boutonEnregistrer.Width = 210;
      boutonEnregistrer.Height = 50;
      boutonEnregistrer.Location = new Point(150, 440);
      boutonEnregistrer.BackColor = Color.FromArgb(45, 115, 210);
      boutonEnregistrer.ForeColor = Color.White;
      boutonEnregistrer.FlatStyle = FlatStyle.Flat;
      boutonEnregistrer.FlatAppearance.BorderSize = 0;
      boutonEnregistrer.Font = new Font("Segoe UI", 10, FontStyle.Bold);
      boutonEnregistrer.Click += BoutonEnregistrer_Click;

      // BOUTON ANNULER
      boutonAnnuler = new Button();
      boutonAnnuler.Text = "Annuler";
      boutonAnnuler.Width = 140;
      boutonAnnuler.Height = 50;
      boutonAnnuler.Location = new Point(390, 440);
      boutonAnnuler.BackColor = Color.FromArgb(220, 223, 228);
      boutonAnnuler.ForeColor = Color.FromArgb(50, 55, 65);
      boutonAnnuler.FlatStyle = FlatStyle.Flat;
      boutonAnnuler.FlatAppearance.BorderSize = 0;
      boutonAnnuler.Font = new Font("Segoe UI", 10, FontStyle.Bold);
      boutonAnnuler.Click += BoutonAnnuler_Click;

      // AJOUT DES CONTRÔLES
      Controls.Add(titre);
      Controls.Add(sousTitre);

      Controls.Add(labelCommande);
      Controls.Add(textCommande);

      Controls.Add(labelType);
      Controls.Add(comboType);

      Controls.Add(labelDescription);
      Controls.Add(textDescription);

      Controls.Add(boutonEnregistrer);
      Controls.Add(boutonAnnuler);
    }

    private void BoutonEnregistrer_Click(object? sender, EventArgs e)
    {
      // VÉRIFIER LA SESSION
      if (SessionUtilisateur.UserId == null)
      {
        MessageBox.Show(
            "Aucun utilisateur connecté.",
            "Erreur",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      // VÉRIFIER LE NUMÉRO DE COMMANDE
      if (!int.TryParse(textCommande.Text, out int numeroCommande))
      {
        MessageBox.Show(
            "Veuillez saisir un numéro de commande valide.",
            "Erreur",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      // VÉRIFIER LA DESCRIPTION
      if (string.IsNullOrWhiteSpace(textDescription.Text))
      {
        MessageBox.Show(
            "Veuillez décrire votre demande.",
            "Erreur",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      // CRÉER LE TICKET AVEC LE VRAI ID DU CLIENT
      var ticket = new Ticket
      {
        UserId = SessionUtilisateur.UserId.Value,
        CommandeId = numeroCommande,
        TypeDemande = comboType.Text,
        Description = textDescription.Text,
        Statut = "Ouvert",
        DateCreation = DateTime.Now
      };

      // ENREGISTRER DANS SQLITE
      TicketRepository.AjouterTicket(ticket);

      MessageBox.Show(
          "Votre ticket a été enregistré avec succès.",
          "Ticket créé",
          MessageBoxButtons.OK,
          MessageBoxIcon.Information);

      textCommande.Clear();
      textDescription.Clear();
      comboType.SelectedIndex = 0;
    }

    private void BoutonAnnuler_Click(object? sender, EventArgs e)
    {
      Close();
    }
  }
}
