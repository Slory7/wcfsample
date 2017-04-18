

using PetaPoco;

namespace Repository.Pattern.Infrastructure
{
    public interface IObjectState
    {
        [Ignore]
        ObjectState ObjectState { get; set; }
    }
}