namespace MsOpenIA.Infrastructure.Repositories
{
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.DataModel;
    using Microsoft.Extensions.Options;
    using MsOpenIA.Domain.Entities;
    using MsOpenIA.Domain.Interfaces.Repositories;
    using MsOpenIA.Infrastructure.Configuration;
    using System.Collections.Generic;

    public class RepositoryDynamoDb : IRepositoryDynamoDb
    {
        private readonly DynamoDbSettings _settings;
        private readonly DynamoDBContext _context;

        public RepositoryDynamoDb(IAmazonDynamoDB dynamoDb, IOptions<DynamoDbSettings> settings)
        {
            _settings = settings.Value;
            _context = new DynamoDBContext(dynamoDb);
        }

        public async Task<List<ModelOpenAI>> GetAllModelsAsync()
        {
            try
            {
                var conditions = new List<ScanCondition>();
                var search = _context.ScanAsync<ModelOpenAI>(conditions, new DynamoDBOperationConfig
                {
                    OverrideTableName = _settings.TableName
                });

                return CleanRequest(await search.GetRemainingAsync());
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<ModelOpenAI> GetModelByIdAsync(string keyId)
        {
            try
            {
                return await _context.LoadAsync<ModelOpenAI>(keyId, new DynamoDBOperationConfig
                {
                    OverrideTableName = _settings.TableName
                });
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<ModelOpenAI> SaveModelAsync(ModelOpenAI model)
        {
            try
            {
                await _context.SaveAsync(model, new DynamoDBOperationConfig
                {
                    OverrideTableName = _settings.TableName
                });

                return await GetModelByIdAsync(model.keyId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        private List<ModelOpenAI> CleanRequest(dynamic results)
        {
            if (results is IEnumerable<ModelOpenAI> modelList)
            {
                return modelList
                    .Where(model =>
                    string.IsNullOrEmpty(model?.metadata?.PromptRequest?.Model)).ToList();
            }

            return new List<ModelOpenAI>();
        }
    }
}