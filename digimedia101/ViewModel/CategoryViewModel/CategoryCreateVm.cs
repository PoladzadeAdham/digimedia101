using System.ComponentModel.DataAnnotations;

namespace digimedia101.ViewModel.CategoryViewModel
{
    public class CategoryCreateVm
    {
        [Required, MaxLength(256)]
        public string Name { get; set; }
    }
}
