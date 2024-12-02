namespace MsOpenIA.Application.Business
{
    using MsOpenIA.Domain.Entities;
    using MsOpenIA.Domain.Interfaces.Business;
    using MsOpenIA.Domain.Interfaces.Services;
    using MsOpenIA.Domain.Interfaces.Utilities;
    using OpenAI.Chat;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class BusinessOpenAI : IBusinessOpenAI
    {
        private readonly IFactoryOpenaAI _factory;
        private readonly IServiceOpenAI _openAIService;

        public BusinessOpenAI(IFactoryOpenaAI factory, IServiceOpenAI openAIService)
        {
            _factory = factory;
            _openAIService = openAIService;
        }

        public async Task<ModelOpenAI> AnalyzerAsync(ModelOpenAI model) =>
            ProcessCompletionResponse(await GetCompleteChatAsync(model), model);

        private async Task<ChatCompletion> GetCompleteChatAsync(ModelOpenAI model)
        {
            (List<ChatMessage> messages, ChatCompletionOptions options) = _factory.GetCompleteChat(model);
            return await _openAIService.GetCompleteChatAsync(messages, options);
        }

        private ModelOpenAI ProcessCompletionResponse(ChatCompletion completion, ModelOpenAI model) =>
            _factory.ProcessCompletionResponse(completion, model);
    }
}
