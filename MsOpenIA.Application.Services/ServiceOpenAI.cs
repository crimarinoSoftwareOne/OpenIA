namespace MsOpenIA.Application.Services
{
    using MsOpenIA.Domain.Interfaces.Repositories;
    using MsOpenIA.Domain.Interfaces.Services;
    using OpenAI.Chat;

    public class ServiceOpenAI : IServiceOpenAI
    {
        IRepositoryOpenAI _repository;

        public ServiceOpenAI(IRepositoryOpenAI repository) =>
            _repository = repository;

        public async Task<ChatCompletion> GetCompleteChatAsync(List<ChatMessage> messages, ChatCompletionOptions options) =>
            await _repository.GetCompleteChatAsync(messages, options);
    }
}
