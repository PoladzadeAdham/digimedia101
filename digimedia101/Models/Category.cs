using digimedia101.Models.Common;

namespace digimedia101.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Project> Projects { get; set; }

    }
}
