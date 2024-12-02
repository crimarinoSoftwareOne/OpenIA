namespace MsOpenIA.Application.Mappers
{
    using MsOpenIA.Domain.Interfaces.Business;
    using MsOpenIA.Domain.Interfaces.Mappers;
    using MsOpenIA.Domain.Interfaces.Utilities;
    using System.Threading.Tasks;

    public class MapperResponse : IMapperResponse
    {
        private readonly IBusinessBase _business;
        private readonly IFactory _utils;

        public MapperResponse(IFactory utils, IBusinessBase business)
        {
            _business = business;
            _utils = utils;
        }

        public async Task<dynamic> GetAllAsync(dynamic model) =>
            ModelOpenAIToDto(await _business.GetAllAsync());

        public async Task<dynamic> SaveAsync(dynamic model) =>
            ModelOpenAIToDto(await _business.SaveAsync(DtoToModelOpenAI(model.Data)));

        public async Task<dynamic> TokenizerAsync(dynamic model) =>
            ModelOpenAIToDto(await _business.TokenizerAsync(DtoToModelOpenAI(model.Data)));

        public async Task<dynamic> GetAsync(dynamic model) =>
            ModelOpenAIToDto(await _business.GetAsync(DtoToModelOpenAI(model.Data)));

        public async Task<dynamic> AnalyzerAsync(dynamic model) =>
            ModelOpenAIToDto(await _business.AnalyzerAsync(DtoToModelOpenAI(model.Data)));

        private dynamic DtoToModelOpenAI(dynamic model) =>
            _utils.DtoToModelOpenAI(model);

        private dynamic ModelOpenAIToDto(object model) =>
            _utils.ModelOpenAIToDto(model);
    }
}