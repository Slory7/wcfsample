using ServiceContracts;

namespace WcfService1.Business
{
    public interface IDataProcessor
    {
        CompositeType2 Process(CompositeType2 composite);
    }
}