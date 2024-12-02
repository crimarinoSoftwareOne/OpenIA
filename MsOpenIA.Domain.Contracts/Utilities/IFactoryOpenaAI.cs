namespace MsOpenIA.Domain.Interfaces.Utilities
{
    using OpenAI.Chat;
    using MsOpenIA.Domain.Entities;

    public interface IFactoryOpenaAI
    {
        (List<ChatMessage>, ChatCompletionOptions) GetCompleteChat(ModelOpenAI model);
        ModelOpenAI ProcessCompletionResponse(ChatCompletion completion, ModelOpenAI model);
    }
}
