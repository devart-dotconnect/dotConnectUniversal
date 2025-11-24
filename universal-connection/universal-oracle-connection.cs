using Devart.Data.Universal;

string connectionString =
    "Provider=Oracle;" +
    "Direct=True;" +
    "Host=127.0.0.1;" +
    "Service Name=TestDb;" +
    "User ID=TestUser;" +
    "Password=TestPassword;" +
    "License Key=**********";


using var connection = new UniConnection(connectionString);


try
{
    connection.Open();


    using var command = connection.CreateCommand();
    command.CommandText = "SELECT actor_id, first_name, last_name FROM actor FETCH FIRST 5 ROWS ONLY";
    using var reader = command.ExecuteReader();


    Console.WriteLine("Top 5 actors:");
    while (reader.Read())
    {
        var id = reader.GetInt32(0);
        var firstName = reader.IsDBNull(1) ? "" : reader.GetString(1);
        var lastName = reader.IsDBNull(2) ? "" : reader.GetString(2);
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