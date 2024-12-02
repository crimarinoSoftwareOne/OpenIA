namespace MsOpenIA.Domain.Interfaces.Services
{
    using MsOpenIA.Domain.Entities;

    public interface IServiceDynamoDb
    {
        Task<ModelOpenAI> SaveModelAsync(ModelOpenAI model);
        Task<ModelOpenAI> GetModelAsync(string keyId);
        Task<List<ModelOpenAI>> GetAllModelsAsync();
    }
}
