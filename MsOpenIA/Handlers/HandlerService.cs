namespace MsOpenIA.Handlers
{
    using static MsOpenIA.DTO.ResponseDto.OpenAIError;
    using MsOpenIA.Domain.Interfaces.Mappers;
    using MsOpenIA.DTO;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;
    using System;

    public class HandlerService
    {
        private readonly HttpStatusCode _StatusResponse = HttpStatusCode.OK;
        private readonly IMapperResponse _mapper;
        private readonly string _MessageResponse = "Finish";

        public HandlerService(IMapperResponse responseMapper) =>
            _mapper = responseMapper;

        #region DTO
        public Task<ResponseDto> AnalyzerAsync(ResponseDto model) =>
            ProcessRequest(model, _mapper.AnalyzerAsync);

        public Task<ResponseDto> SaveAsync(ResponseDto model) =>
            ProcessRequest(model, _mapper.SaveAsync);

        public Task<ResponseDto> GetAsync(ResponseDto model) =>
            ProcessRequest(model, _mapper.GetAsync);

        public Task<ResponseDto> GetAllAsync(ResponseDto model) =>
            ProcessRequest(model, _mapper.GetAllAsync);

        public Task<ResponseDto> TokenizerAsync(ResponseDto model) =>
            ProcessRequest(model, _mapper.TokenizerAsync);

        private async Task<ResponseDto> ProcessRequest(ResponseDto model, Func<ResponseDto, Task<dynamic>> serviceMethod)
        {
            var response = await ValidateModel(model, serviceMethod);
            return new ResponseDto(_StatusResponse, _MessageResponse, response);
        }

        private async Task<dynamic> ValidateModel(ResponseDto model, Func<ResponseDto, Task<dynamic>> serviceMethod)
        {
            if (model.Status == _StatusResponse && model.Data?.ValueKind != JsonValueKind.Null)
            {
                return await serviceMethod(model);
            }

            return null;
        }
        #endregion

        #region Error

        private static Task<ResponseDto> SendError(HttpStatusCode status, string message, string info) =>
            Task.FromResult(new ResponseDto(status, message, info));

        public Task<ResponseDto> HandleError(Exception ex)
        {
            return ex.Message switch
            {
                string msg when msg.Contains(OpenAIErrorCodes.InvalidAuthentication.Message) =>
                    SendError((HttpStatusCode)OpenAIErrorCodes.InvalidAuthentication.StatusCode,
                        OpenAIErrorCodes.InvalidAuthentication.ErrorCode, OpenAIErrorCodes.InvalidAuthentication.Message),

                string msg when msg.Contains(OpenAIErrorCodes.IncorrectApiKey.Message) =>
                    SendError((HttpStatusCode)OpenAIErrorCodes.IncorrectApiKey.StatusCode,
                        OpenAIErrorCodes.IncorrectApiKey.ErrorCode, OpenAIErrorCodes.IncorrectApiKey.Message),

                string msg when msg.Contains(OpenAIErrorCodes.NotMemberOfOrganization.Message) =>
                    SendError((HttpStatusCode)OpenAIErrorCodes.NotMemberOfOrganization.StatusCode,
                        OpenAIErrorCodes.NotMemberOfOrganization.ErrorCode, OpenAIErrorCodes.NotMemberOfOrganization.Message),

                string msg when msg.Contains(OpenAIErrorCodes.UnsupportedRegion.Message) =>
                    SendError((HttpStatusCode)OpenAIErrorCodes.UnsupportedRegion.StatusCode,
                        OpenAIErrorCodes.UnsupportedRegion.ErrorCode, OpenAIErrorCodes.UnsupportedRegion.Message),

                string msg when msg.Contains(OpenAIErrorCodes.RateLimitReached.Message) =>
                    SendError((HttpStatusCode)OpenAIErrorCodes.RateLimitReached.StatusCode,
                        OpenAIErrorCodes.RateLimitReached.ErrorCode, OpenAIErrorCodes.RateLimitReached.Message),

                string msg when msg.Contains(OpenAIErrorCodes.QuotaExceeded.Message) =>
                    SendError((HttpStatusCode)OpenAIErrorCodes.QuotaExceeded.StatusCode,
                        OpenAIErrorCodes.QuotaExceeded.ErrorCode, OpenAIErrorCodes.QuotaExceeded.Message),

                string msg when msg.Contains(OpenAIErrorCodes.ServerError.Message) =>
                    SendError((HttpStatusCode)OpenAIErrorCodes.ServerError.StatusCode,
                        OpenAIErrorCodes.ServerError.ErrorCode, OpenAIErrorCodes.ServerError.Message),

                string msg when msg.Contains(OpenAIErrorCodes.EngineOverloaded.Message) =>
                    SendError((HttpStatusCode)OpenAIErrorCodes.EngineOverloaded.StatusCode,
                        OpenAIErrorCodes.EngineOverloaded.ErrorCode, OpenAIErrorCodes.EngineOverloaded.Message),

                _ => SendError(HttpStatusCode.ServiceUnavailable, ex.Message, ex.InnerException?.Message)
            };
        }
        #endregion
    }
}