using System.Data;
using FluentValidation;
using ZeroGravity.Application;
using ZeroGravity.Services.Skeletal.Data.Entities;
using ZeroGravity.Services.Skeletal.Data.Repositories;

namespace ZeroGravity.Services.Skeletal.Queries;

public class GetFiberQueryValidator : AbstractValidator<GetFiberQuery>
{
    public GetFiberQueryValidator(IFiberRepository repository)
    {
        RuleFor(q => q.Name)
            .MustAsync(async (name, _) => await repository.GetByNameAsync(name) is not null)
            .WithErrorCode("Fiber does not exist in the database");
    }
}