namespace MsOpenIA.Domain.Entities
{
    public class Usage
    {
        public Usage() 
        {
            OutputTokenCount = string.Empty;
            InputTokenCount = string.Empty;
            TokensUsed = string.Empty;
        }
        
        public Usage(double _outputTokenCount, double _inputTokenCount, int _tokensUsed)
        {
            OutputTokenCount = _outputTokenCount.ToString();
            InputTokenCount = _inputTokenCount.ToString();
            TokensUsed = _tokensUsed.ToString();
        }

        public string? OutputTokenCount { get; set; }
        public string? InputTokenCount { get; set; }
        public string? TokensUsed { get; set; }
    }
}
