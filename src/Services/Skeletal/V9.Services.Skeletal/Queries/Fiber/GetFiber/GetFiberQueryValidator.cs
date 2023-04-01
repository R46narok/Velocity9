using System.Data;
using FluentValidation;
using V9.Application;
using V9.Services.Skeletal.Data.Repositories;
using V9.Services.Skeletal.Data.Entities;

namespace V9.Services.Skeletal.Queries;

public class GetFiberQueryValidator : AbstractValidator<GetFiberQuery>
{
    public GetFiberQueryValidator(IFiberRepository repository)
    {
        RuleFor(q => q.Name)
            .MustAsync(async (name, _) => await repository.GetByNameAsync(name) is not null)
            .WithErrorCode("Fiber does not exist in the database");
    }
}