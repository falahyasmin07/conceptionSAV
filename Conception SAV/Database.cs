using Microsoft.Data.Sqlite;

namespace Conception_SAV
{
  public class Database
  {
    private const string ConnectionString = "Data Source= e_commerce.db";

    public static SqliteConnection GetConnection()
    {
      return new SqliteConnection(ConnectionString);
    }
  }
}
