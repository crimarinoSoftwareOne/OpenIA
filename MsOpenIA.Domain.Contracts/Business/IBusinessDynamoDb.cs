namespace MsOpenIA.Domain.Interfaces.Business
{
    using MsOpenIA.Domain.Entities;

    public interface IBusinessDynamoDb
    {
        Task<ModelOpenAI> SaveModelOpenAIAsync(ModelOpenAI model);
        Task<ModelOpenAI> SaveResponseAsync(ModelOpenAI model);
        Task<ModelOpenAI> GetModelAsync(ModelOpenAI model);
        Task<List<ModelOpenAI>> GetAllModelsAsync();
    }
}
