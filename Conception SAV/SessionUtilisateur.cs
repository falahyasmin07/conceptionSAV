namespace Conception_SAV
{
  public static class SessionUtilisateur
  {
    public static int? UserId { get; set; }

    public static string? Identifiant { get; set; }

    public static string? Role { get; set; }

    public static void FermerSession()
    {
      UserId = null;
      Identifiant = null;
      Role = null;
    }
  }
}
