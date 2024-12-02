namespace MsOpenIA.Domain.Interfaces.Utilities
{
    using MsOpenIA.Domain.Entities;

    public interface IFactoryTokenizer
    {
        ModelOpenAI TokenizerAsync(ModelOpenAI model);
    }
}
