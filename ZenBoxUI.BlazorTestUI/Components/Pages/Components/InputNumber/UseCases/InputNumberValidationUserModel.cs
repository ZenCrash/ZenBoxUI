using System.ComponentModel.DataAnnotations;

namespace ZenBoxUI.BlazorTestUI.Components.Pages.Components.InputNumber.UseCases
{
  public class InputNumberValidationUserModel
  {
    [Required(ErrorMessage = "Number is required.")]
    [Range(0, 101, ErrorMessage = "Number must be between 0 and 100")]
    public int? Number { get; set; } = 0;
  }
}
