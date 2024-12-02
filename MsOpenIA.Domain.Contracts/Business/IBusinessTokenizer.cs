namespace MsOpenIA.Domain.Interfaces.Business
{
    using MsOpenIA.Domain.Entities;

    public interface IBusinessTokenizer
    {
        ModelOpenAI TokenizerAsync(ModelOpenAI model);
    }
}
