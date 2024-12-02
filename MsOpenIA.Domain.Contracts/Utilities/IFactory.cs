namespace MsOpenIA.Domain.Interfaces.Utilities
{
    using MsOpenIA.Domain.Entities;
    using OpenAI.Chat;
    using System.Text.Json;

    public interface IFactory
    {
        #region DTO
        ModelOpenAI DtoToModelOpenAI(dynamic obj);
        dynamic ModelOpenAIToDto(object model);
        #endregion

        #region BuildMessages
        string BuildSystemChatMessage();
        string BuildIssueDescriptionMessage(string issueDescription);
        string BuildLineCodeMessage(string lineCode);
        #endregion

        #region Function
        string NameFunction();
        ChatTool GetFunctionTool();
        #endregion

        string SerializeObject<T>(T obj);
        ResponseAI ConvertJsonToResponseAI(JsonElement obj);
    }
}
