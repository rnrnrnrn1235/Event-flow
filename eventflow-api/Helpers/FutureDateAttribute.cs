using System.ComponentModel.DataAnnotations;

public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime date)
        {
            if (date < DateTime.UtcNow)
            {
                return new ValidationResult("Event date must be in the future.");
            }
        }
        return ValidationResult.Success;
    }
}