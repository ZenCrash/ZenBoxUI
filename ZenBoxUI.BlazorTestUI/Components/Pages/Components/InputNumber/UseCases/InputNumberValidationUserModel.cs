using System.ComponentModel.DataAnnotations;

namespace ZenBoxUI.BlazorTestUI.Components.Pages.Components.InputNumber.UseCases
{
  public class InputNumberValidationUserModel
  {
    [Required(ErrorMessage = "Value is required.")]
    [Range(1, 20, ErrorMessage = "Value must be inclusive between 1, 20")]
    public int? NumberInput { get; set; } = 1;
  }
}
