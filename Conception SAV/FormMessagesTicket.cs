using System;
using System.Drawing;
using System.Windows.Forms;

namespace Conception_SAV
{
  public class FormMessagesTicket : Form
  {
    private int ticketId;
    private DataGridView tableauMessages;
    private TextBox textMessage;
    private Button boutonEnvoyer;

    public FormMessagesTicket(int idTicket)
    {
      ticketId = idTicket;

      Text = "Messages du ticket";
      Width = 800;
      Height = 600;
      StartPosition = FormStartPosition.CenterScreen;

      tableauMessages = new DataGridView();
      tableauMessages.Location = new Point(20, 20);
      tableauMessages.Width = 740;
      tableauMessages.Height = 400;

      tableauMessages.AutoSizeColumnsMode =
          DataGridViewAutoSizeColumnsMode.Fill;

      tableauMessages.ReadOnly = true;
      tableauMessages.AllowUserToAddRows = false;

      tableauMessages.Columns.Add("UserId", "Utilisateur");
      tableauMessages.Columns.Add("Message", "Message");
      tableauMessages.Columns.Add("DateMessage", "Date");

      textMessage = new TextBox();
      textMessage.Location = new Point(20, 450);
      textMessage.Width = 580;
      textMessage.Height = 60;
      textMessage.Multiline = true;

      boutonEnvoyer = new Button();
      boutonEnvoyer.Text = "Envoyer";
      boutonEnvoyer.Location = new Point(620, 455);
      boutonEnvoyer.Width = 140;
      boutonEnvoyer.Height = 40;

      boutonEnvoyer.Click += BoutonEnvoyer_Click;

      Controls.Add(tableauMessages);
      Controls.Add(textMessage);
      Controls.Add(boutonEnvoyer);

      ChargerMessages();
    }

    private void ChargerMessages()
    {
      tableauMessages.Rows.Clear();

      var messages =
          MessageTicketRepository.GetMessages(ticketId);

      foreach (var message in messages)
      {
        tableauMessages.Rows.Add(
            message.UserId,
            message.Message,
            message.DateMessage.ToString("dd/MM/yyyy HH:mm")
        );
      }
    }

    private void BoutonEnvoyer_Click(object? sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(textMessage.Text))
      {
        MessageBox.Show("Écrivez un message.");
        return;
      }

      var message = new MessageTicket
      {
        TicketId = ticketId,
        UserId = 1,
        Message = textMessage.Text,
        DateMessage = DateTime.Now
      };

      MessageTicketRepository.AjouterMessage(message);

      textMessage.Clear();

      ChargerMessages();
    }
  }
}
