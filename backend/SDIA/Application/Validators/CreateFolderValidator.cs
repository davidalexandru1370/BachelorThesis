using Application.Commands.Folder;
using Application.Interfaces;
using Domain.Constants;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Validators;

public class CreateFolderValidator : AbstractValidator<CreateFolderCommand>
{
    public CreateFolderValidator(ISdiaDbContext dbContext)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.UserId)
            .MustAsync(async (id, cancellationToken) =>
            {
                return await dbContext.Users.AnyAsync(x => x.Id == id, cancellationToken);
            }).WithMessage(I18N.UserNotFound.ToString());
    }
}