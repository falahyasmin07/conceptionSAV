using System;
using System.Drawing;
using System.Windows.Forms;

namespace Conception_SAV
{
  public class FormSAV : Form
  {
    private Label titre;
    private Label sousTitre;
    private Label labelUtilisateur;

    private Button boutonCreerTicket;
    private Button boutonVoirTickets;
    private Button boutonDeconnexion;

    public FormSAV()
    {
      // FENETRE PRINCIPALE
      Text = "SAV+ - Espace client";
      Width = 900;
      Height = 550;
      StartPosition = FormStartPosition.CenterScreen;

      BackColor = Color.FromArgb(245, 247, 250);
      Font = new Font("Segoe UI", 10);

      // TITRE
      titre = new Label();
      titre.Text = "Service après-vente";
      titre.Font = new Font("Segoe UI", 28, FontStyle.Bold);
      titre.ForeColor = Color.FromArgb(35, 45, 65);
      titre.AutoSize = true;
      titre.Location = new Point(250, 65);

      // SOUS-TITRE
      sousTitre = new Label();
      sousTitre.Text = "Gérez facilement vos demandes SAV";
      sousTitre.Font = new Font("Segoe UI", 12);
      sousTitre.ForeColor = Color.FromArgb(100, 110, 125);
      sousTitre.AutoSize = true;
      sousTitre.Location = new Point(280, 115);

      // UTILISATEUR CONNECTE
      labelUtilisateur = new Label();
      labelUtilisateur.Text =
          "Connecté : " + SessionUtilisateur.Identifiant;

      labelUtilisateur.Font =
          new Font("Segoe UI", 10, FontStyle.Bold);

      labelUtilisateur.ForeColor =
          Color.FromArgb(70, 80, 100);

      labelUtilisateur.AutoSize = true;
      labelUtilisateur.Location = new Point(30, 25);

      // BOUTON CREER UN TICKET
      boutonCreerTicket = new Button();
      boutonCreerTicket.Text = "Créer un ticket";
      boutonCreerTicket.Width = 220;
      boutonCreerTicket.Height = 60;
      boutonCreerTicket.Location = new Point(180, 220);

      boutonCreerTicket.BackColor =
          Color.FromArgb(45, 115, 210);

      boutonCreerTicket.ForeColor = Color.White;
      boutonCreerTicket.FlatStyle = FlatStyle.Flat;
      boutonCreerTicket.FlatAppearance.BorderSize = 0;

      boutonCreerTicket.Font =
          new Font("Segoe UI", 11, FontStyle.Bold);

      boutonCreerTicket.Click +=
          BoutonCreerTicket_Click;

      // BOUTON VOIR MES TICKETS
      boutonVoirTickets = new Button();
      boutonVoirTickets.Text = "Voir mes tickets";
      boutonVoirTickets.Width = 220;
      boutonVoirTickets.Height = 60;
      boutonVoirTickets.Location = new Point(470, 220);

      boutonVoirTickets.BackColor =
          Color.FromArgb(70, 160, 120);

      boutonVoirTickets.ForeColor = Color.White;
      boutonVoirTickets.FlatStyle = FlatStyle.Flat;
      boutonVoirTickets.FlatAppearance.BorderSize = 0;

      boutonVoirTickets.Font =
          new Font("Segoe UI", 11, FontStyle.Bold);

      boutonVoirTickets.Click +=
          BoutonVoirTickets_Click;

      // BOUTON DECONNEXION
      boutonDeconnexion = new Button();
      boutonDeconnexion.Text = "Se déconnecter";
      boutonDeconnexion.Width = 180;
      boutonDeconnexion.Height = 45;
      boutonDeconnexion.Location = new Point(350, 360);

      boutonDeconnexion.BackColor =
          Color.FromArgb(220, 223, 228);

      boutonDeconnexion.ForeColor =
          Color.FromArgb(50, 55, 65);

      boutonDeconnexion.FlatStyle = FlatStyle.Flat;
      boutonDeconnexion.FlatAppearance.BorderSize = 0;

      boutonDeconnexion.Font =
          new Font("Segoe UI", 10, FontStyle.Bold);

      boutonDeconnexion.Click +=
          BoutonDeconnexion_Click;

      // AJOUT DES ELEMENTS
      Controls.Add(titre);
      Controls.Add(sousTitre);
      Controls.Add(labelUtilisateur);
      Controls.Add(boutonCreerTicket);
      Controls.Add(boutonVoirTickets);
      Controls.Add(boutonDeconnexion);
    }

    // OUVRIR LE FORMULAIRE DE CREATION
    private void BoutonCreerTicket_Click(
        object? sender,
        EventArgs e)
    {
      using (FormTicket formulaire = new FormTicket())
      {
        formulaire.ShowDialog();
      }
    }

    // OUVRIR LA LISTE DES TICKETS
    private void BoutonVoirTickets_Click(
        object? sender,
        EventArgs e)
    {
      using (FormListeTickets formulaire =
          new FormListeTickets())
      {
        formulaire.ShowDialog();
      }
    }

    // DECONNEXION
    private void BoutonDeconnexion_Click(
        object? sender,
        EventArgs e)
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
