// ReSharper disable UnusedMember.Global

// ReSharper disable UnusedMemberInSuper.Global
namespace CleanSample.Framework.Domain.Functional.Choices;

public interface IChoice 
{ 
    object? Value { get ; }
    int Index { get; }
}