namespace MsOpenIA.Application.Business
{
    using MsOpenIA.Domain.Entities;
    using MsOpenIA.Domain.Interfaces.Business;
    using MsOpenIA.Domain.Interfaces.Utilities;

    public class BusinessTokenizer : IBusinessTokenizer
    {
        private readonly IFactoryTokenizer _factory;

        public BusinessTokenizer(IFactoryTokenizer factory) =>
            _factory = factory;

        public ModelOpenAI TokenizerAsync(ModelOpenAI model) =>
            _factory.TokenizerAsync(model);
    }
}
