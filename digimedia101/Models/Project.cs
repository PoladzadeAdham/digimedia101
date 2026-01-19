using digimedia101.Models.Common;

namespace digimedia101.Models
{
    public class Project : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
