using System.ComponentModel.DataAnnotations;

namespace digimedia101.ViewModel.CategoryViewModel
{
    public class CategoryUpdateVm
    {
        public int Id { get; set; }
        [Required, MaxLength(256)]
        public string Name { get; set; }
    }
}
