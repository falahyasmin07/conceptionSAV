using System;
using System.Drawing;
using System.Windows.Forms;

namespace Conception_SAV
{
  public class FormInscription : Form
  {
    private TextBox textIdentifiant;
    private TextBox textMotDePasse;
    private TextBox textConfirmation;
    private Button boutonInscrire;
    private Button boutonAnnuler;

    public FormInscription()
    {
      Text = "Inscription - Service après-vente";
      Width = 520;
      Height = 560;
      StartPosition = FormStartPosition.CenterScreen;

      BackColor = Color.FromArgb(245, 247, 250);
      Font = new Font("Segoe UI", 10);

      Label titre = new Label();
      titre.Text = "Créer un compte";
      titre.Font = new Font("Segoe UI", 25, FontStyle.Bold);
      titre.ForeColor = Color.FromArgb(35, 45, 65);
      titre.AutoSize = true;
      titre.Location = new Point(105, 35);

      Label sousTitre = new Label();
      sousTitre.Text = "Inscrivez-vous pour accéder à votre espace SAV";
      sousTitre.Font = new Font("Segoe UI", 9);
      sousTitre.ForeColor = Color.FromArgb(100, 110, 125);
      sousTitre.AutoSize = true;
      sousTitre.Location = new Point(55, 85);

      // IDENTIFIANT
      Label labelIdentifiant = new Label();
      labelIdentifiant.Text = "Identifiant";
      labelIdentifiant.AutoSize = true;
      labelIdentifiant.Location = new Point(70, 135);

      textIdentifiant = new TextBox();
      textIdentifiant.Location = new Point(70, 160);
      textIdentifiant.Width = 360;
      textIdentifiant.Font = new Font("Segoe UI", 11);

      // MOT DE PASSE
      Label labelMotDePasse = new Label();
      labelMotDePasse.Text = "Mot de passe (8 caractères minimum)";
      labelMotDePasse.AutoSize = true;
      labelMotDePasse.Location = new Point(70, 210);

      textMotDePasse = new TextBox();
      textMotDePasse.Location = new Point(70, 235);
      textMotDePasse.Width = 360;
      textMotDePasse.UseSystemPasswordChar = true;
      textMotDePasse.Font = new Font("Segoe UI", 11);

      // CONFIRMATION
      Label labelConfirmation = new Label();
      labelConfirmation.Text = "Confirmer le mot de passe";
      labelConfirmation.AutoSize = true;
      labelConfirmation.Location = new Point(70, 285);

      textConfirmation = new TextBox();
      textConfirmation.Location = new Point(70, 310);
      textConfirmation.Width = 360;
      textConfirmation.UseSystemPasswordChar = true;
      textConfirmation.Font = new Font("Segoe UI", 11);

      // BOUTON INSCRIPTION
      boutonInscrire = new Button();
      boutonInscrire.Text = "Créer mon compte";
      boutonInscrire.Location = new Point(70, 385);
      boutonInscrire.Width = 210;
      boutonInscrire.Height = 45;
      boutonInscrire.BackColor = Color.FromArgb(45, 115, 210);
      boutonInscrire.ForeColor = Color.White;
      boutonInscrire.FlatStyle = FlatStyle.Flat;
      boutonInscrire.FlatAppearance.BorderSize = 0;
      boutonInscrire.Font = new Font(
          "Segoe UI", 10, FontStyle.Bold);

      boutonInscrire.Click += BoutonInscrire_Click;

      // BOUTON ANNULER
      boutonAnnuler = new Button();
      boutonAnnuler.Text = "Annuler";
      boutonAnnuler.Location = new Point(300, 385);
      boutonAnnuler.Width = 130;
      boutonAnnuler.Height = 45;
      boutonAnnuler.BackColor = Color.FromArgb(220, 223, 228);
      boutonAnnuler.FlatStyle = FlatStyle.Flat;
      boutonAnnuler.FlatAppearance.BorderSize = 0;

      boutonAnnuler.Click += (sender, e) => Close();

      Controls.Add(titre);
      Controls.Add(sousTitre);
      Controls.Add(labelIdentifiant);
      Controls.Add(textIdentifiant);
      Controls.Add(labelMotDePasse);
      Controls.Add(textMotDePasse);
      Controls.Add(labelConfirmation);
      Controls.Add(textConfirmation);
      Controls.Add(boutonInscrire);
      Controls.Add(boutonAnnuler);

      AcceptButton = boutonInscrire;
      CancelButton = boutonAnnuler;
    }

    private void BoutonInscrire_Click(object? sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(textIdentifiant.Text) ||
          string.IsNullOrWhiteSpace(textMotDePasse.Text) ||
          string.IsNullOrWhiteSpace(textConfirmation.Text))
      {
        MessageBox.Show(
            "Veuillez remplir tous les champs.",
            "Inscription",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      if (textMotDePasse.Text != textConfirmation.Text)
      {
        MessageBox.Show(
            "Les deux mots de passe ne correspondent pas.",
            "Inscription",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);

        return;
      }

      bool succes = UserRepository.Inscrire(
          textIdentifiant.Text,
          textMotDePasse.Text,
          out string message);

      if (succes)
      {
        MessageBox.Show(
            message,
            "Inscription réussie",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        Close();
      }
      else
      {
        MessageBox.Show(
            message,
            "Inscription impossible",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
      }
    }
  }
}
