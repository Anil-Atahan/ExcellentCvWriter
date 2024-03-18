using System.Reflection;

namespace Modules.Resume.Application;

/// <summary>
/// Represents the resume module application assembly reference.
/// </summary>
public static class AssemblyReference
{
    /// <summary>
    /// The assembly.
    /// </summary>
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
