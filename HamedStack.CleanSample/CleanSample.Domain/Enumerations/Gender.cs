using CleanSample.Framework.Domain.Enumerations;

namespace CleanSample.Domain.Enumerations;

public class Gender : Enumeration<Gender>
{
    private Gender()
    {

    }

    public static readonly Gender Male = new("Male", 1, "A gender identity that refers to a person who identifies as a man");
    public static readonly Gender Female = new("Female", 2, "A gender identity that refers to a person who identifies as a woman");
    public Gender(string name, int value, string? description = "") : base(name, value, description)
    {
    }
}