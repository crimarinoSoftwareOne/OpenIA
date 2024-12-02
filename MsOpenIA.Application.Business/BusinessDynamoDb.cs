namespace MsOpenIA.Application.Business
{
    using MsOpenIA.Domain.Entities;
    using MsOpenIA.Domain.Interfaces.Business;
    using MsOpenIA.Domain.Interfaces.Services;
    using System.Collections.Generic;

    public class BusinessDynamoDb : IBusinessDynamoDb
    {
        private readonly IServiceDynamoDb _service;

        public BusinessDynamoDb(IServiceDynamoDb service) =>
            _service = service;

        public async Task<ModelOpenAI> SaveModelOpenAIAsync(ModelOpenAI model) =>
            ValidateModelOpenIAEmpty(model) ? await SaveModelOpenAI(model.requestAI) : await UpdateModelOpenAI(model);
        
        public async Task<ModelOpenAI> SaveResponseAsync(ModelOpenAI model) =>
            await ValidateModelOpenIA_Analized(model);

        public async Task<ModelOpenAI> GetModelAsync(ModelOpenAI model) =>
            await GetModelOpenAI(model);

        public async Task<List<ModelOpenAI>> GetAllModelsAsync() =>
            await GetAllModels();

        private async Task<ModelOpenAI> SaveModelOpenAI(RequestAI request) => 
            await _service.SaveModelAsync(new ModelOpenAI(Guid.NewGuid().ToString(), request));

        private async Task<ModelOpenAI> UpdateModelOpenAI(ModelOpenAI model) =>
            await ValidateModelOpenIA_Analized(model);

        private async Task<ModelOpenAI> GetModelOpenAI(ModelOpenAI model) =>
            await _service.GetModelAsync(model.keyId);

        private async Task<List<ModelOpenAI>> GetAllModels() => 
            await _service.GetAllModelsAsync();

        private bool ValidateModelOpenIAEmpty(ModelOpenAI model) =>
            model.keyId == "0";

        private async Task<ModelOpenAI> ValidateModelOpenIA_Analized(ModelOpenAI model)
        {
            return model.metadata.ModelAnalized ?
                await _service.SaveModelAsync(new ModelOpenAI(model.keyId, model.requestAI, model.metadata, model.metadata.UpdatedStep)) :
                model;
        }
    }
}
