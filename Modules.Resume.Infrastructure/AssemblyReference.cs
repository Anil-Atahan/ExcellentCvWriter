using System.Reflection;

namespace Modules.Resume.Infrastructure;

/// <summary>
/// Represents the resume module infrastructure assembly reference.
/// </summary>
public static class AssemblyReference
{
    /// <summary>
    /// The assembly.
    /// </summary>
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
