using Core.Entites;

namespace Entities.Concrete
{
    public class Color : IEntity
    {
        public int Id { get; set; }
        public string ColorName { get; set; }
    }
}
