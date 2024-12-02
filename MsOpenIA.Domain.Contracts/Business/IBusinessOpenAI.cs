namespace MsOpenIA.Domain.Interfaces.Business
{
    using MsOpenIA.Domain.Entities;
    using System.Threading.Tasks;

    public interface IBusinessOpenAI
    {
        Task<ModelOpenAI> AnalyzerAsync(ModelOpenAI request);
    }
}
