using FluentValidation;
using FluentValidation.Results;

namespace CityForum.WebApi.Models;

public abstract class MessageRequest
{
    public string Text { get; set; }

    public class Validator : AbstractValidator<MessageRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Text)
                .MaximumLength(500).WithMessage("Length must be not great than 500");
        }
    }
}

public static class MessageRequestExtension
{
    public static ValidationResult Validate(this MessageRequest model)
    {
        return new MessageRequest.Validator().Validate(model);
    }
}