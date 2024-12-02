namespace MsOpenIA.Domain.Interfaces.Business
{
    using MsOpenIA.Domain.Entities;

    public interface IBusinessBase
    {
        Task<ModelOpenAI> SaveAsync(ModelOpenAI model);
        Task<ModelOpenAI> GetAsync(ModelOpenAI model);
        Task<List<ModelOpenAI>> GetAllAsync();
        Task<ModelOpenAI> AnalyzerAsync(ModelOpenAI model);
        Task<ModelOpenAI> TokenizerAsync(ModelOpenAI model);
    }
}
