# Connect to Any SQL Database in C# with a Universal ADO.NET Codebase

Based on [https://www.devart.com/dotconnect/universal/connect-with-universal.html](https://www.devart.com/dotconnect/universal/connect-with-universal.html)

This tutorial shows how to build a unified, database-agnostic ADO.NET codebase in C# using dotConnect Universal. Whether you're targeting SQL Server, Oracle, MySQL, PostgreSQL, SQLite, or even data sources via OLEDB or ODBC, dotConnect Universal offers a consistent programming interface across providers. This guide helps you reduce duplicate logic, simplify maintenance, and switch data providers with minimal code changes.

## Connect to SQL Server using dotConnect Universal

Learn how to configure dotConnect Universal to connect to a SQL Server database. This section demonstrates how to define the provider, construct the connection string, and perform standard ADO.NET operations—without changing your codebase when switching between providers.

```cs
using Devart.Data.Universal;

string connectionString =
    "Provider=SQL Server;" +
    "Data Source=SERVER;" +
    "Initial Catalog=Northwind;" +
    "User ID=TestUser;" +
    "License Key=**********";

using var connection = new UniConnection(connectionString);

try
{
    connection.Open();

    using var command = connection.CreateCommand();
    command.CommandText = "SELECT actor_id, first_name, last_name FROM actor";
    using var reader = command.ExecuteReader();

    Console.WriteLine("Actors:");
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
```

## Connect to Oracle using dotConnect Universal

This section walks through connecting to Oracle using dotConnect Universal, showing how to supply Oracle-specific settings like service names or SID in a format compatible with Universal’s abstraction layer.

```cs
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
```

## Connect to MySQL and MariaDB using dotConnect Universal

Explore how to access MySQL or MariaDB using dotConnect Universal. You'll configure the Universal connection string to use the MySQL provider and maintain full compatibility with your shared ADO.NET code.

```cs
using Devart.Data.Universal;

string connectionString =
    "Provider=MySQL;" +
    "Server=127.0.0.1;" +
    "Port=3306;" +
    "User ID=TestUser;" +
    "Password=TestPassword;" +
    "Database=sakila;" +
    "License Key=**********";

using var connection = new UniConnection(connectionString);

try
{
    connection.Open();

    using var command = connection.CreateCommand();
    command.CommandText = "SELECT actor_id, first_name, last_name FROM actor";
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
```

## Connect to PostgreSQL using dotConnect Universal

This section shows how to plug PostgreSQL into your universal data access layer. Using dotConnect Universal, you can execute the same ADO.NET logic—connection, command, and data reading—without PostgreSQL-specific rewrites.

```cs
using Devart.Data.Universal;

string connectionString =
    "Provider=PostgreSQL;" +
    "Host=127.0.0.1;" +
    "Port=5432;" +
    "User Id=TestUser;" +
    "Password=TestPassword;" +
    "Database=TestDatabase;" +
    "Initial Schema=Public;" +
    "License Key=**********";

using var connection = new UniConnection(connectionString);

try
{
    connection.Open();

    using var command = connection.CreateCommand();
    command.CommandText = "SELECT actor_id, first_name, last_name FROM actor ORDER BY actor_id LIMIT 5";
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
```

## Connect to SQLite using dotConnect Universal

For lightweight or embedded apps, this section demonstrates how to connect to local SQLite databases using dotConnect Universal. It also highlights integrated encryption support and how to switch between encrypted and non-encrypted databases.

```cs
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
```

## Connect to any data source using OLEDB in C#

When working with legacy systems or proprietary sources, OLEDB can bridge the gap. This section shows how to configure and use OleDbConnection in C# with provider-specific connection strings while maintaining compatibility with your broader ADO.NET workflow.

```cs
using Devart.Data.Universal;

string connectionString =
    "Provider=MS Access;" +
    "Data Source=127.0.0.1;" +
    "User ID=TestUser;" +
    "Password=TestPassword;" +
    "Database=TestDb;" +
    "License Key=**********";

using var connection = new UniConnection(connectionString);

try
{
    connection.Open();

    using var command = connection.CreateCommand();
    command.CommandText = "SELECT actor_id, first_name, last_name FROM actor";
    using var reader = command.ExecuteReader();

    Console.WriteLine("Actors:");
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
```

## Connect to any data source using ODBC in C#

ODBC offers another path to reach various databases. This section explains how to use OdbcConnection in your C# app to connect to virtually any data source that provides an ODBC driver, including how to manage DSNs and driver-specific parameters.

```cs
using Devart.Data.Universal;

string connectionString =
    "Provider=ODBC;" +
    "Dsn=MyDsn;" +
    "User ID=TestUser;" +
    "Password=TestPassword;" +
    "License Key=**********";

using var connection = new UniConnection(connectionString);

try
{
    connection.Open();

    using var command = connection.CreateCommand();
    command.CommandText = "SELECT actor_id, first_name, last_name FROM actor";
    using var reader = command.ExecuteReader();

    Console.WriteLine("Actors:");
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
```