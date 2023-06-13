using FluentValidation;
using RazorPagesDemo.Models.DTO;

namespace RazorPagesDemo.Models.Validations
{
	public class UserDtoValidator : AbstractValidator<UserDtoRequest>
	{
		public UserDtoValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty()
				.Length(3, 14);

			RuleFor(x => x.YearOfBirth)
				.NotEmpty()
				.InclusiveBetween(1900, 2005);

			RuleFor(x => x.Gender)
				.IsInEnum();
		}
	}
}
