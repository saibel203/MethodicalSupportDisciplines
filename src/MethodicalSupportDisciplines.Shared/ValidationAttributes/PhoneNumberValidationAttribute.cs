using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MethodicalSupportDisciplines.Shared.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class PhoneNumberValidationAttribute : ValidationAttribute
{
    private const string RegexPattern = @"^\+|(\+380)|380";

    public PhoneNumberValidationAttribute()
    {
        ErrorMessage = "{0}"; // Placeholder for the error message
    }
    
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not null)
        {
            string? phoneNumber = value.ToString();

            if (phoneNumber is not null && Regex.IsMatch(phoneNumber, RegexPattern))
            {
                return new ValidationResult(ErrorMessage);
            }
        }

        return ValidationResult.Success!;
    }
}