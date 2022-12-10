using FluentValidation;
using FluentValidation.Results;

namespace CityForum.WebApi.Models;

public abstract class TopicRequest
{
    public string Name { get; set; }

    public class Validator : AbstractValidator<TopicRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("Length must be not great than 100");
        }
    }
}

public static class TopicRequestExtension
{
    public static ValidationResult Validate(this TopicRequest model)
    {
        return new TopicRequest.Validator().Validate(model);
    }
}