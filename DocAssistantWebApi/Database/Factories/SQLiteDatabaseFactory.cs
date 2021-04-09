namespace DocAssistantWebApi.Database.Factories
{
    public class SQLiteDatabaseFactory : IDatabaseFactory
    {
        public IDatabaseContext Create()
        {
            return new SQLiteDatabaseContext();
        }
    }
}