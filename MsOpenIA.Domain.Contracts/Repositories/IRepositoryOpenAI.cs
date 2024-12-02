namespace MsOpenIA.Domain.Interfaces.Repositories
{
    using OpenAI.Chat;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepositoryOpenAI
    {
        Task<ChatCompletion> GetCompleteChatAsync(List<ChatMessage> messages, ChatCompletionOptions options);
    }
}