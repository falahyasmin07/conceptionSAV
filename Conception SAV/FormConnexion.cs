using System;
using System.Drawing;
using System.Windows.Forms;

namespace Conception_SAV
{
  public class FormConnexion : Form
  {
    private TextBox textIdentifiant;
    private TextBox textMotDePasse;

    public FormConnexion()
    {
      Text = "SAV+ - Connexion";
      ClientSize = new Size(650, 600);
      StartPosition = FormStartPosition.CenterScreen;
      FormBorderStyle = FormBorderStyle.FixedSingle;
      MaximizeBox = false;

      BackColor = Color.FromArgb(245, 247, 251);
      Font = new Font("Segoe UI", 10);

      // BANDEAU
      Panel bandeau = new Panel();
      bandeau.Location = new Point(0, 0);
      bandeau.Size = new Size(650, 130);
      bandeau.BackColor = Color.FromArgb(24, 39, 66);

      Label logo = new Label();
      logo.Text = "SAV+";
      logo.Font = new Font("Segoe UI", 30, FontStyle.Bold);
      logo.ForeColor = Color.White;
      logo.AutoSize = true;
      logo.Location = new Point(40, 20);

      Label slogan = new Label();
      slogan.Text = "SERVICE APRÈS-VENTE";
      slogan.Font = new Font("Segoe UI", 10, FontStyle.Bold);
      slogan.ForeColor = Color.FromArgb(180, 205, 255);
      slogan.AutoSize = true;
      slogan.Location = new Point(45, 78);

      bandeau.Controls.Add(logo);
      bandeau.Controls.Add(slogan);
      Controls.Add(bandeau);

      // TITRE
      Label titre = new Label();
      titre.Text = "Bienvenue";
      titre.Font = new Font("Segoe UI", 25, FontStyle.Bold);
      titre.ForeColor = Color.FromArgb(24, 39, 66);
      titre.AutoSize = true;
      titre.Location = new Point(150, 155);
      Controls.Add(titre);

      Label sousTitre = new Label();
      sousTitre.Text = "Connectez-vous à votre espace SAV";
      sousTitre.ForeColor = Color.FromArgb(100, 110, 125);
      sousTitre.AutoSize = true;
      sousTitre.Location = new Point(153, 200);
      Controls.Add(sousTitre);

      // IDENTIFIANT
      Label labelIdentifiant = new Label();
      labelIdentifiant.Text = "Identifiant";
      labelIdentifiant.Font = new Font(
          "Segoe UI", 10, FontStyle.Bold);
      labelIdentifiant.AutoSize = true;
      labelIdentifiant.Location = new Point(150, 245);
      Controls.Add(labelIdentifiant);

      textIdentifiant = new TextBox();
      textIdentifiant.Location = new Point(150, 273);
      textIdentifiant.Size = new Size(350, 32);
      textIdentifiant.Font = new Font("Segoe UI", 11);
      Controls.Add(textIdentifiant);

      // MOT DE PASSE
      Label labelMotDePasse = new Label();
      labelMotDePasse.Text = "Mot de passe";
      labelMotDePasse.Font = new Font(
          "Segoe UI", 10, FontStyle.Bold);
      labelMotDePasse.AutoSize = true;
      labelMotDePasse.Location = new Point(150, 320);
      Controls.Add(labelMotDePasse);

      textMotDePasse = new TextBox();
      textMotDePasse.Location = new Point(150, 348);
      textMotDePasse.Size = new Size(350, 32);
      textMotDePasse.Font = new Font("Segoe UI", 11);
      textMotDePasse.UseSystemPasswordChar = true;
      Controls.Add(textMotDePasse);

      // BOUTON CONNEXION
      Button boutonConnexion = new Button();
      boutonConnexion.Text = "Se connecter";
      boutonConnexion.Location = new Point(150, 405);
      boutonConnexion.Size = new Size(350, 45);
      boutonConnexion.BackColor =
          Color.FromArgb(47, 105, 210);
      boutonConnexion.ForeColor = Color.White;
      boutonConnexion.FlatStyle = FlatStyle.Flat;
      boutonConnexion.FlatAppearance.BorderSize = 0;
      boutonConnexion.Font =
          new Font("Segoe UI", 11, FontStyle.Bold);
      boutonConnexion.Cursor = Cursors.Hand;

      boutonConnexion.Click += BoutonConnexion_Click;

      Controls.Add(boutonConnexion);

      // BOUTON INSCRIPTION
      Button boutonInscription = new Button();
      boutonInscription.Text = "Créer un compte";
      boutonInscription.Location = new Point(150, 465);
      boutonInscription.Size = new Size(350, 42);
      boutonInscription.BackColor = Color.White;
      boutonInscription.ForeColor =
          Color.FromArgb(47, 105, 210);
      boutonInscription.FlatStyle = FlatStyle.Flat;
      boutonInscription.FlatAppearance.BorderColor =
          Color.FromArgb(47, 105, 210);
      boutonInscription.Font =
          new Font("Segoe UI", 10, FontStyle.Bold);
      boutonInscription.Cursor = Cursors.Hand;

      boutonInscription.Click += BoutonInscription_Click;

      Controls.Add(boutonInscription);

      // PIED DE PAGE
      Label pied = new Label();
      pied.Text = "Votre service après-vente, simplement.";
      pied.ForeColor = Color.FromArgb(120, 130, 145);
      pied.AutoSize = true;
      pied.Location = new Point(205, 540);

      Controls.Add(pied);

      AcceptButton = boutonConnexion;
    }

    // CONNEXION
    private void BoutonConnexion_Click(
        object? sender,
        EventArgs e)
    {
      string identifiant = textIdentifiant.Text.Trim();
      string motDePasse = textMotDePasse.Text;

      if (string.IsNullOrWhiteSpace(identifiant) ||
          string.IsNullOrWhiteSpace(motDePasse))
      {
        MessageBox.Show(
            "Veuillez remplir tous les champs.",
            "Connexion",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      // ADMINISTRATEUR
      if (identifiant == "admin" &&
          motDePasse == "admin123")
      {
        SessionUtilisateur.Identifiant = "admin";
        SessionUtilisateur.UserId = null;
        SessionUtilisateur.Role = "Admin";

        Hide();

        using (FormAdmin formulaire = new FormAdmin())
        {
          formulaire.ShowDialog();
        }

        SessionUtilisateur.FermerSession();

        textMotDePasse.Clear();
        Show();

        return;
      }

      // CLIENT
      string? role = UserRepository.VerifierConnexion(
          identifiant,
          motDePasse);

      if (role == "Client")
      {
        int? idUtilisateur =
            UserRepository.ObtenirIdUtilisateur(identifiant);

        if (idUtilisateur == null)
        {
          MessageBox.Show("Utilisateur introuvable.");
          return;
        }

        SessionUtilisateur.UserId = idUtilisateur;
        SessionUtilisateur.Identifiant = identifiant;
        SessionUtilisateur.Role = "Client";

        Hide();

        using (FormSAV formulaire = new FormSAV())
        {
          formulaire.ShowDialog();
        }

        SessionUtilisateur.FermerSession();

        textMotDePasse.Clear();
        Show();
      }
      else
      {
        MessageBox.Show(
            "Identifiant ou mot de passe incorrect.",
            "Connexion refusée",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
      }
    }

    // INSCRIPTION
    private void BoutonInscription_Click(
        object? sender,
        EventArgs e)
    {
      using (FormInscription formulaire =
          new FormInscription())
      {
        formulaire.ShowDialog();
      }

      textMotDePasse.Clear();
      textIdentifiant.Focus();
    }
  }
}
