using System.Reflection;

namespace ExcellentCvWriter.SharedKernel;

/// <summary>
/// Represents the shared kernel assembly reference.
/// </summary>
public static class AssemblyReference
{
    /// <summary>
    /// The assembly.
    /// </summary>
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
