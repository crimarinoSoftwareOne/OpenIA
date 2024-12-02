namespace MsOpenIA.Domain.Interfaces.Services
{
    using OpenAI.Chat;

    public interface IServiceOpenAI
    {
        Task<ChatCompletion> GetCompleteChatAsync(List<ChatMessage> messages, ChatCompletionOptions options);
    }
}
