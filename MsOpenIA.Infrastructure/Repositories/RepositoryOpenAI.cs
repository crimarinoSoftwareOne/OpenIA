namespace MsOpenIA.Infrastructure.Repositories
{
    using MsOpenIA.Domain.Interfaces.Repositories;
    using OpenAI.Chat;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class RepositoryOpenAI : IRepositoryOpenAI
    {
        private readonly ChatClient _chatClient;

        public RepositoryOpenAI(string apiKey, string typeModel) => 
            _chatClient = new ChatClient(model: typeModel, apiKey: apiKey);

        public async Task<ChatCompletion> GetCompleteChatAsync(List<ChatMessage> messages, ChatCompletionOptions options)
        {
            try
            {
                return await _chatClient.CompleteChatAsync(messages, options);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
