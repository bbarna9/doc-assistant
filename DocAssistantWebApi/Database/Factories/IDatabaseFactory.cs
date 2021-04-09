namespace DocAssistantWebApi.Database.Factories
{
    public interface IDatabaseFactory
    {
        IDatabaseContext Create();
    }
}