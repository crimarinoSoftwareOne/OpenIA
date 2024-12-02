namespace MsOpenIA.Domain.Interfaces.Mappers
{
    using System.Threading.Tasks;

    public interface IMapperResponse
    {
        Task<dynamic> GetAllAsync(dynamic model);
        Task<dynamic> SaveAsync(dynamic model);
        Task<dynamic> TokenizerAsync(dynamic model);
        Task<dynamic> GetAsync(dynamic model);
        Task<dynamic> AnalyzerAsync(dynamic model);
    }
}