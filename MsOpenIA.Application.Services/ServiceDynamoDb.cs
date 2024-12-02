namespace MsOpenIA.Application.Services
{
    using MsOpenIA.Domain.Entities;
    using MsOpenIA.Domain.Interfaces.Repositories;
    using MsOpenIA.Domain.Interfaces.Services;

    public class ServiceDynamoDb : IServiceDynamoDb
    {
        IRepositoryDynamoDb _repository;

        public ServiceDynamoDb(IRepositoryDynamoDb repository) =>
            _repository = repository;
        
        public async Task<ModelOpenAI> SaveModelAsync(ModelOpenAI model) =>
            await _repository.SaveModelAsync(model);

        public async Task<ModelOpenAI> GetModelAsync(string keyId) =>
            await _repository.GetModelByIdAsync(keyId);

        public async Task<List<ModelOpenAI>> GetAllModelsAsync() =>
            await _repository.GetAllModelsAsync();
    }
}
