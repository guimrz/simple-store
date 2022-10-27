using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;
using SimpleStore.Services.Catalog.Domain;
using System.ComponentModel.DataAnnotations;

namespace SimpleStore.Services.Catalog.Application.Validators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ValidBrandValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            bool result = value == null || validationContext.GetRequiredService<IUnitOfWork>()
                .Repository<Brand>()
                .Entities
                .AsNoTracking()
                .Any(p => p.Id.Equals(value));

            return result ? null : new ValidationResult($"The brand with id '{value}' could not be found.");
        }
    }
}
