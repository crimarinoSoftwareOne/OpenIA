namespace MsOpenIA.Domain.Entities
{
    public class RequestAI
    {
        public RequestAI()
        {
            lineCode = string.Empty;
            issueDescription = string.Empty;
        }

        public RequestAI(string _lineCode, string _issueDescription)
        {
            lineCode = _lineCode;
            issueDescription = _issueDescription;
        }

        public string lineCode { get; set; }

        public string issueDescription { get; set; }
    }
}
