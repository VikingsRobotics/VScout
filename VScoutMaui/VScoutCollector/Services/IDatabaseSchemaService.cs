namespace VScoutCollector.Services
{
    public interface IDatabaseSchemaService
    {
        Task CreateDatabaseAsync();
        void DropDatabase();
    }
}