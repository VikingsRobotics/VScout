namespace VScoutCentral.Services
{
    public interface IDatabaseSchemaService
    {
        Task CreateDatabaseAsync();
        void DropDatabase();
    }
}