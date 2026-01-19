using System.ComponentModel.DataAnnotations;

namespace digimedia101.ViewModel.ProjectViewModel
{
    public class ProjectUpdateVm
    {
        public int Id { get; set; }
        [Required, MaxLength(256)]
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
