using System.ComponentModel.DataAnnotations;

namespace Application._Common.Validation;

public static class GuidValidatorAttribute
{
    public static bool IsValidGuid(Guid? value)
    {
        if (value == null)
        {
            return true;
        }

        return Guid.TryParse(value.ToString(), out _);
    }

    public static bool IsValidGuid(Guid value)
    {
        if (value == null)
        {
            return true;
        }

        return Guid.TryParse(value.ToString(), out _);
    }
}