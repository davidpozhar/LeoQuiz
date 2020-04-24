using System.Collections.Generic;

namespace LeoQuiz.Core.Entities
{
    public class UserRole : IEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
