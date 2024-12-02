namespace MsOpenIA.Application.Utilities
{
    using MsOpenIA.Domain.Entities;
    using MsOpenIA.Domain.Interfaces.Utilities;
    using OpenAI.Assistants;
    using OpenAI.Chat;
    using TiktokenSharp;

    public class FactoryTokenizer : IFactoryTokenizer
    {
        private readonly IFactory _utils;
        private ModelOpenAI _model;

        private string GetDetailsRemaining =>
            "The calculated tokens are analyzed based on the displayed messages and the response structure of the AI model." +
            "This ensures that the model interprets and generates responses that are accurate and contextually relevant.";

        public FactoryTokenizer(IFactory utils) =>
            _utils = utils;

        public ModelOpenAI TokenizerAsync(ModelOpenAI model) 
        {
            _model = model;
            return PrepareChatMessages();
        }

        private string CountTokensAsync(Promtp promtp, ChatTool chatTool)
        {
            TikToken tikToken = TikToken.EncodingForModel(promtp.Model);

            List<int> tokenPromtp = tikToken.Encode(_utils.SerializeObject<Promtp>(promtp));
            List<int> tokenChatTool = tikToken.Encode(_utils.SerializeObject<ChatTool>(chatTool));

            int quantityTokens = tokenPromtp.Count + tokenChatTool.Count;
            return quantityTokens.ToString();
        }

        private ModelOpenAI PrepareChatMessages()
        {
            _model.metadata.PromptRequest.Messages =
                [
                    new Promtp.SystemChatMessage(_utils.BuildSystemChatMessage()),
                    new Promtp.UserChatMessage(_utils.BuildLineCodeMessage(_model.requestAI.lineCode)),
                    new Promtp.UserChatMessage(_utils.BuildIssueDescriptionMessage(_model.requestAI.issueDescription))
                ];

            _model.metadata.PromptRequest.TokensRemaining = CountTokensAsync(_model.metadata.PromptRequest, _utils.GetFunctionTool());
            _model.metadata.PromptRequest.DetailsRemaining = GetDetailsRemaining;
            return new ModelOpenAI(_model.keyId, _model.requestAI, _model.metadata, _model.metadata.TokeneizedStep);
        }
    }
}
