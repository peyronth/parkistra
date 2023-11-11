namespace ParkIstra.Libraries.EF;

public class EFValidator
{
    public static bool Validate(object entity)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(entity);

        var isValid = Validator.TryValidateObject(
            entity, validationContext, validationResults, true);

        if (!isValid)
        {
            throw new EntityValidationException(
                $"Validation for {entity.GetType().Name} has failed.",
                validationResults);
        }

        return true;
    }
}
