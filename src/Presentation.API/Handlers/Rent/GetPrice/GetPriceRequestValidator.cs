using Core.Extensions;
using FluentValidation;
using System;

namespace Presentation.API.Handlers.Rent.GetPrice
{
    public class GetPriceRequestValidator : AbstractValidator<GetPriceRequest>
    {
        public GetPriceRequestValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("{PropertyName} no puede estar vacío")
                .NotNull().WithMessage("{PropertyName} no puede estar vacío")
                .Must(IsValidGuid).WithMessage("{PropertyName} tiene un formato de Guid inválido");

            RuleFor(x => x.Hours)
               .GreaterThan(0).WithMessage("{PropertyName} debe ser mayor a cero");

            RuleFor(x => x.Quantity)
               .GreaterThan(0).WithMessage("{PropertyName} debe ser mayor a cero");
        }
        private bool IsValidGuid(string id)
        => Guid.NewGuid().ValidateGuid(id);
    }
}
