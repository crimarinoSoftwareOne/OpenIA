namespace MsOpenIA.Application.Utilities
{
    using MsOpenIA.Domain.Entities;
    using MsOpenIA.Domain.Interfaces.Utilities;
    using OpenAI.Chat;
    using System.Text.Json;

    public class FactoryOpenaAI : IFactoryOpenaAI
    {
        private readonly IFactory _utils;
        private ModelOpenAI _model;

        public FactoryOpenaAI(IFactory utils) =>
            _utils = utils;

        #region Response
        public ModelOpenAI ProcessCompletionResponse(ChatCompletion completion, ModelOpenAI model)
        {
            _model = model;
            return MapFinishReason(completion);
        }

        private ModelOpenAI MapFinishReason(ChatCompletion completion)
        {
            if (completion.FinishReason == ChatFinishReason.ToolCalls)
            {
                OpenAI.Chat.AssistantChatMessage assistant = new(completion);

                if (assistant.Content.Count() > 0)
                {
                    _model.metadata.PromptRequest.Messages.Add(new Promtp.AssistantChatMessage(assistant.Content[0].Text));
                }

                foreach (ChatToolCall toolCall in completion.ToolCalls)
                {
                    if (toolCall.FunctionName.Equals(_utils.NameFunction()))
                    {
                        MapCompletionContentToResponse(toolCall);
                    }
                }

                _model.metadata.PromptRequest.IdModel = completion.Id;
                _model.metadata.PromptRequest.ModelName = completion.Model;
                _model.metadata.PromptRequest.Usage = new(completion.Usage.OutputTokenCount, completion.Usage.InputTokenCount, completion.Usage.TotalTokenCount);
            }

            return new ModelOpenAI(_model);
        }

        private void MapCompletionContentToResponse(ChatToolCall toolCall)
        {
            try
            {
                using JsonDocument argumentsJson = JsonDocument.Parse(toolCall.FunctionArguments);
                _model.responseAI = _utils.ConvertJsonToResponseAI(argumentsJson.RootElement);
                _model.metadata.PromptRequest.Messages.Add(new Promtp.ToolChatMessage(toolCall.Id, _utils.SerializeObject<ResponseAI>(_model.responseAI)));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
        #endregion

        #region Request
        public (List<ChatMessage>, ChatCompletionOptions) GetCompleteChat(ModelOpenAI model)
        {
            _model = model;
            return BuildRequest();
        }
        
        private (List<ChatMessage>, ChatCompletionOptions) BuildRequest() =>
            (PrepareChatMessages(), ConfigureOptions());

        private ChatCompletionOptions ConfigureOptions()
        {
            return new ChatCompletionOptions()
            {
                Tools = { _utils.GetFunctionTool() },
                TopP = _model.metadata.PromptRequest.TopP,
                MaxOutputTokenCount = _model.metadata.PromptRequest.MaxOutputTokenCount,
                Temperature = _model.metadata.PromptRequest.Temperature,
                FrequencyPenalty = _model.metadata.PromptRequest.FrequencyPenalty,
                PresencePenalty = _model.metadata.PromptRequest.PresencePenalty,
            };
        }

        private List<ChatMessage> PrepareChatMessages()
        {
            return
                [
                    new OpenAI.Chat.SystemChatMessage(_utils.BuildSystemChatMessage()),
                    new OpenAI.Chat.UserChatMessage(_utils.BuildIssueDescriptionMessage(_model.requestAI.issueDescription)),
                    new OpenAI.Chat.UserChatMessage(_utils.BuildLineCodeMessage(_model.requestAI.lineCode))
                ];
        }
        #endregion
    }
}
