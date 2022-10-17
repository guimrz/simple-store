using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;
using SimpleStore.Services.Catalog.Domain;
using System.ComponentModel.DataAnnotations;

namespace SimpleStore.Services.Catalog.Application.Validators
{
    public class ValidBrandValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            bool result = validationContext.GetRequiredService<IUnitOfWork>()
                .Repository<Brand>()
                .All
                .AsNoTracking()
                .Any(p => p.Id.Equals(value));

            return result ? null : new ValidationResult($"The brand with id '{value}' could not be found.");
        }
    }
}
