namespace MsOpenIA.Domain.Entities
{
    public class MetadataModelOpenAI
    {
        public readonly Step SavedStep = new("SavedSuccess", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
        public readonly Step TokeneizedStep = new("TokeneizedSuccess", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
        public readonly Step AnalizedStep = new("AnalizedSuccess", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
        public readonly Step UpdatedStep = new("UpdatedSuccess", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));

        public MetadataModelOpenAI()
        {
            PromptRequest = new();
            ModelAnalized = false;
            StepsModel = [SavedStep];
        }

        public MetadataModelOpenAI(Promtp _promptRequest, List<Step> _steps)
        {
            ModelAnalized = false;
            StepsModel = _steps;
            PromptRequest = _promptRequest;
        }

        public MetadataModelOpenAI(MetadataModelOpenAI metadata)
        {
            ModelAnalized = true;
            metadata.StepsModel.Add(AnalizedStep);
            StepsModel = metadata.StepsModel;
            PromptRequest = metadata.PromptRequest;
        }

        public Promtp PromptRequest { get; set; }
        public List<Step> StepsModel { get; set; }
        public bool ModelAnalized { get; set; }
    }

    public class Promtp
    {
        public Promtp()
        {
            Model = string.Empty;
            TopP = 0;
            Temperature = 0;
            MaxOutputTokenCount = 0;
            FrequencyPenalty = 0;
            PresencePenalty = 0;
        }

        public Promtp(string _model, float _topP, float _temperature, int _maxOutputTokenCount, float _frequencyPenalty, float _presencePenalty)
        {
            Messages = [new SystemChatMessage(string.Empty), new UserChatMessage(string.Empty)];
            Model = _model;
            TopP = _topP;
            Temperature = _temperature;
            TokensRemaining = "0";
            DetailsRemaining = string.Empty;
            MaxOutputTokenCount = _maxOutputTokenCount;
            FrequencyPenalty = _frequencyPenalty;
            PresencePenalty = _presencePenalty;
        }

        public string? ModelName { get; set; }
        public List<ChatMessagePromtp>? Messages { get; set; }
        public Usage? Usage { get; set; }
        public string Model { get; set; }
        public float TopP { get; set; }
        public float Temperature { get; set; }
        public string? TokensRemaining { get; set; }
        public string? DetailsRemaining { get; set; }
        public int MaxOutputTokenCount { get; set; }
        public float FrequencyPenalty { get; set; }
        public float PresencePenalty { get; set; }
        public string? IdModel { get; set; }

        public class ChatMessagePromtp
        {
            public string Role { get; set; }
            public string Content { get; set; }
        }

        public class AssistantChatMessage : ChatMessagePromtp
        {
            public AssistantChatMessage(string content)
            {
                Role = "Assistant";
                Content = content;
            }
        }

        public class SystemChatMessage : ChatMessagePromtp
        {
            public SystemChatMessage(string content)
            {
                Role = "System";
                Content = content;
            }
        }

        public class UserChatMessage : ChatMessagePromtp
        {
            public UserChatMessage(string content)
            {
                Role = "User";
                Content = content;
            }
        }

        public class ToolChatMessage : ChatMessagePromtp
        {
            public string Id { get; set; }
            public string ToolResult { get; set; }

            public ToolChatMessage(string _id, string _toolResult)
            {
                Role = "ToolChat";
                Content = string.Empty;
                Id = _id;
                ToolResult = _toolResult;
            }
        }
    }

    public class Step
    {
        public Step()
        {
            Name = string.Empty;
            UpdatedAt = string.Empty;
        }

        public string? Name { get; set; }
        public string? UpdatedAt { get; set; }

        public Step(string name, string updatedAt)
        {
            Name = name;
            UpdatedAt = updatedAt;
        }
    }
}
