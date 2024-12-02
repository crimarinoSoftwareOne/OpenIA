namespace MsOpenIA.Domain.Entities
{
    using Amazon.DynamoDBv2.DataModel;

    [DynamoDBTable("dbOpenAI")]
    public class ModelOpenAI
    {
        private readonly string _model = "gpt-3.5-turbo";
        private readonly float _topP = 1;
        private readonly float _temperature = 1;
        private readonly int _maxOutputTokenCount = 2000;
        private readonly float _frequencyPenalty = 0;
        private readonly float _presencePenalty = 0;

        public ModelOpenAI()
        {
            keyId = "0";
        }

        public ModelOpenAI(string _keyId, RequestAI _request)
        {
            keyId = _keyId;
            requestAI = _request;
            metadata = new();
            metadata.PromptRequest = new(_model, _topP, _temperature, _maxOutputTokenCount, _frequencyPenalty, _presencePenalty);
        }

        public ModelOpenAI(string _keyId, RequestAI _request, MetadataModelOpenAI _metadataModel, Step step)
        {
            keyId = _keyId;
            requestAI = _request;
            _metadataModel.StepsModel.Add(step);
            metadata = _metadataModel;
        }

        public ModelOpenAI(ModelOpenAI model)
        {
            keyId = model.keyId;
            requestAI = model.requestAI;
            responseAI = model.responseAI;
            metadata = new MetadataModelOpenAI(model.metadata);
        }

        [DynamoDBHashKey]
        public string keyId { get; set; }

        public RequestAI? requestAI { get; set; }

        public ResponseAI? responseAI { get; set; }

        public MetadataModelOpenAI?  metadata { get; set; }
    }
}
