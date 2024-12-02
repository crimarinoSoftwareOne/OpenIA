namespace MsOpenIA.Domain.Interfaces.Repositories
{
    using MsOpenIA.Domain.Entities;

    public interface IRepositoryDynamoDb
    {
        Task<List<ModelOpenAI>> GetAllModelsAsync();
        Task<ModelOpenAI> GetModelByIdAsync(string keyId);
        Task<ModelOpenAI> SaveModelAsync(ModelOpenAI model);
    }
}
