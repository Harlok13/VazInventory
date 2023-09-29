namespace Inventory.BL.ModelValidation;

public class EntityValidation : IValidation
{
    public void Validate<TEntity>(TEntity entity)
    {
        var results = new List<ValidationResult>();
        if (entity != null)
        {
            var context = new ValidationContext(entity);

            if (!Validator.TryValidateObject(entity, context, results, true))
            {
                foreach (var error in results)
                {
                    WriteLine(error.ErrorMessage);
                }
            }
        }
        else
        {
            WriteLine($"EntityValidation error. Entity {typeof(TEntity)} is null!");
        }
    }
}