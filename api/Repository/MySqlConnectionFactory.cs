namespace api.Repository;

public class MySqlConnectionFactory
{
    public static MySqlConnection GetConnection()
    {
        const string connectionString = "server=localhost;" +
                                        "port=3306;" +
                                        "database=db_evento;" +
                                        "uid=root;" +
                                        "pwd=Senai@2024;";
        return new MySqlConnection(connectionString);
    }
}