using System.ComponentModel.DataAnnotations.Schema;
using Repository.Pattern.Infrastructure;
using PetaPoco;

namespace Repository.Pattern
{
    public abstract class Entity : IObjectState
    {
        [NotMapped]
        [Ignore]
        public ObjectState ObjectState { get; set; }
    }
}