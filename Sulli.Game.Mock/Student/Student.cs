using Sulli.Game.Storage;

namespace Sulli.Game.Mock
{
    public class Student : EntityBase<int>
    {
        public string? Name { get; set; }

        public int Age { get; set; }
    }
}