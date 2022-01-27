using FluentValidation;
using WhatToDo.Api.Dtos;
using WhatToDo.Core.Contracts;

namespace WhatToDo.Api.Validations
{
    public class CreateItemDtoValidator : AbstractValidator<CreateItemDto>
    {
        private readonly IToDoItemRepository _repository;

        // TODO: Confirm the accepted length of description with product team
        public CreateItemDtoValidator(IToDoItemRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description cannot be empty")
                .Length(2, 200).WithMessage("Description should be between 2 and 200 characters")
                .MustAsync((o, cancellation) => IsNotDuplicate(o)).WithMessage("This item already exists.");
        }

        private async Task<bool> IsNotDuplicate(string description)
        {
            var result =  await _repository.IsDuplicateAsync(description);
            return !result;
        }
    }
}
