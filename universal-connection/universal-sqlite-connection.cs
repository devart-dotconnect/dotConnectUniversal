using Devart.Data.Universal;

string connectionString =
    "Provider=SQLite;" +
    "Data Source=myDatabase.db;" +
    "FailIfMissing=False;" +
    "License Key=**********";

using var connection = new UniConnection(connectionString);

try
{
    connection.Open();

    using var command = connection.CreateCommand();
    command.CommandText = "SELECT actor_id, first_name, last_name FROM actor LIMIT 5";
    using var reader = command.ExecuteReader();

    Console.WriteLine("Top 5 actors:");
    while (reader.Read())
    {
        var id = reader.GetInt32(0);
        var firstName = reader.GetString(1);
        var lastName = reader.GetString(2);
        Console.WriteLine($"{id}: {firstName} {lastName}");
    }
}
catch (Exception ex)
{
    Console.WriteLine("Error while connecting or executing query:");
    Console.WriteLine(ex.Message);
    Environment.ExitCode = 1;
}
finally
{
    if (connection.State != System.Data.ConnectionState.Closed)
    {
        connection.Close();
    }
}