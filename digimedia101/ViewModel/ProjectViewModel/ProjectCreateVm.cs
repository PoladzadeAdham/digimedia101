using System.ComponentModel.DataAnnotations;

namespace digimedia101.ViewModel.ProjectViewModel
{
    public class ProjectCreateVm
    {
        [Required, MaxLength(256)]
        public string Name { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
