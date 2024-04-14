using System.ComponentModel.DataAnnotations;
using System.Reflection;

public class ValidateCopyNumbersAttribute : ValidationAttribute
{
    private readonly string _totalCopiesPropertyName;
    private readonly string _availableCopiesPropertyName;

    public ValidateCopyNumbersAttribute(string totalCopiesPropertyName, string availableCopiesPropertyName)
    {
        _totalCopiesPropertyName = totalCopiesPropertyName;
        _availableCopiesPropertyName = availableCopiesPropertyName;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var totalCopiesProperty = validationContext.ObjectType.GetProperty(_totalCopiesPropertyName);
        var availableCopiesProperty = validationContext.ObjectType.GetProperty(_availableCopiesPropertyName);

        if (totalCopiesProperty == null)
            throw new ArgumentException("Property with this name not found", _totalCopiesPropertyName);

        if (availableCopiesProperty == null)
            throw new ArgumentException("Property with this name not found", _availableCopiesPropertyName);

        var totalCopiesValue = (int)totalCopiesProperty.GetValue(validationContext.ObjectInstance);
        var availableCopiesValue = (int)availableCopiesProperty.GetValue(validationContext.ObjectInstance);

        if (totalCopiesValue < availableCopiesValue)
        {
            return new ValidationResult($"Total copies cannot be less than available copies.");
        }

        return ValidationResult.Success;
    }
}
