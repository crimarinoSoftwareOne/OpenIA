namespace MsOpenIA.Domain.Entities
{
    public class ResponseAI
    {
        public string solutionDescription { get; set; }
        public string solutionCode { get; set; }
        public string timeToFix { get; set; }
        public List<AlternativeSolution> alternativeSolutions { get; set; }

        public ResponseAI() 
        {
            solutionDescription = string.Empty;
            solutionCode = string.Empty;
            timeToFix = string.Empty;
            alternativeSolutions = [new AlternativeSolution()];
        }

        public ResponseAI(string _solutionDescription, string _solutionCode, string _timeToFix)
        {
            solutionDescription = _solutionDescription;
            solutionCode = _solutionCode;
            timeToFix = _timeToFix;
            alternativeSolutions = [new AlternativeSolution()];
        }

        public ResponseAI(string _solutionDescription, string _solutionCode, string _timeToFix, List<AlternativeSolution> _alternativeSolutions)
        {
            solutionDescription = _solutionDescription;
            solutionCode = _solutionCode;
            timeToFix = _timeToFix;
            alternativeSolutions = _alternativeSolutions;
        }

        public class AlternativeSolution
        {
            public string solutionDescription { get; set; }
            public string solutionCode { get; set; }
            public string timeToFix { get; set; }

            public AlternativeSolution() 
            {
                solutionDescription = string.Empty;
                solutionCode = string.Empty;
                timeToFix = string.Empty;
            }

            public AlternativeSolution(string _solutionDescription, string _solutionCode, string _timeToFix)
            {
                solutionDescription = _solutionDescription;
                solutionCode = _solutionCode;
                timeToFix = _timeToFix;
            }
        }
    }
}