using Inventory.DAL.Entities;

namespace Inventory.BL.ModelValidation;

public class InformationSystemValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is InformationSystem infSystem)
        {
            if (infSystem.Code.Contains("CART", StringComparison.OrdinalIgnoreCase))
            {
                ErrorMessage = $"{infSystem.Code} The code contains the name CART.";
                return false;
            }
            else if (infSystem.Code.Length != 14)
            {
                ErrorMessage = "The code does not match the 14-character format.";
                return false;
            }

            if (string.IsNullOrEmpty(infSystem.Name))
            {
                ErrorMessage = "The name of the information system is empty.";
                return false;
            }

            return true;
        }

        return false;
    }
}