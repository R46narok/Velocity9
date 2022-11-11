using System.Data;
using FluentValidation;
using ZeroGravity.Application;
using ZeroGravity.Services.Muscles.Data.Entities;
using ZeroGravity.Services.Muscles.Data.Repositories;

namespace ZeroGravity.Services.Muscles.Queries;

public class GetFiberQueryValidator : AbstractValidator<GetFiberQuery>
{
    public GetFiberQueryValidator(IFiberRepository repository)
    {
        RuleFor(q => q.Name)
            .MustAsync(async (name, _) => await repository.GetByNameAsync(name) is not null)
            .WithErrorCode(StatusCode.NotFound)
            .WithMessage(DetailsMessage.For(StatusCode.NotFound, nameof(Fiber)));
    }
}