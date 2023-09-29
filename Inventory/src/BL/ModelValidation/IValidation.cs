namespace Inventory.BL.ModelValidation;

public interface IValidation
{
    void Validate<TEntity>(TEntity entity);
}