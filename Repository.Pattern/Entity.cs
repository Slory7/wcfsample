using Repository.Pattern.Infrastructure;
using PetaPoco;

namespace Repository.Pattern
{
    public abstract class Entity : IObjectState
    {
        [Ignore]
        public ObjectState ObjectState { get; set; }
    }
}