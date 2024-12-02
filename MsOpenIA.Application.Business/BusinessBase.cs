namespace MsOpenIA.Application.Business
{
    using MsOpenIA.Domain.Interfaces.Business;
    using MsOpenIA.Domain.Entities;
    using System.Collections.Generic;

    public class BusinessBase : IBusinessBase
    {
        private readonly IBusinessDynamoDb _dynamoDbBusiness;
        private readonly IBusinessTokenizer _tokenizerBusiness;
        private readonly IBusinessOpenAI _openAIBusiness;

        public BusinessBase(IBusinessDynamoDb dynamoDb, IBusinessTokenizer tokenizerBusiness, IBusinessOpenAI openAIBusiness)
        {
            _dynamoDbBusiness = dynamoDb;
            _tokenizerBusiness = tokenizerBusiness;
            _openAIBusiness = openAIBusiness;
        }

        public async Task<List<ModelOpenAI>> GetAllAsync() =>
            await _dynamoDbBusiness.GetAllModelsAsync();

        public async Task<ModelOpenAI> GetAsync(ModelOpenAI model) =>
            await _dynamoDbBusiness.GetModelAsync(model);

        public async Task<ModelOpenAI> SaveAsync(ModelOpenAI model) =>
            await _dynamoDbBusiness.SaveModelOpenAIAsync(model);

        public async Task<ModelOpenAI> TokenizerAsync(ModelOpenAI model) =>
            await _dynamoDbBusiness.SaveResponseAsync(_tokenizerBusiness.TokenizerAsync(model));
        
        public async Task<ModelOpenAI> AnalyzerAsync(ModelOpenAI model) =>
            await _dynamoDbBusiness.SaveResponseAsync(await _openAIBusiness.AnalyzerAsync(model));
    }
}
