﻿using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ZeroGravity.Application;
using ZeroGravity.Services.Skeletal.Data.Repositories;

namespace ZeroGravity.Services.Skeletal.Commands.Exercises.CreateExercise;

public class CreateExerciseCommandValidator : AbstractValidator<CreateExerciseCommand>
{
    public CreateExerciseCommandValidator(
        IExerciseRepository repository,
        IAuthorRepository authorRepository,
        IMuscleRepository muscleRepository)
    {
        RuleFor(cmd => cmd.Name)
            .MustAsync(async (name, _) => await repository.GetByNameAsync(name) is null)
            .WithErrorCode(StatusCode.BadRequest)
            .WithMessage("Already exists");

        RuleFor(cmd => cmd.Name)
            .NotEmpty()
            .WithErrorCode(StatusCode.BadRequest);
        
        RuleFor(cmd => cmd.Description)
            .NotEmpty()
            .WithErrorCode(StatusCode.BadRequest);

        RuleFor(cmd => cmd.AuthorName)
            .MustAsync(async (author, _) => await authorRepository.GetByNameAsync(author, false) is not null)
            .WithErrorCode(StatusCode.BadRequest)
            .WithMessage("Author must not be null");
        
        
        RuleFor(cmd => cmd.TargetNames)
            .ForEach(target => 
                target.MustAsync(async (t, _) => await muscleRepository.GetByNameAsync(t, false) is not null))
            .WithErrorCode(StatusCode.BadRequest)
            .WithMessage("Target must not be null");
    }
}