using System.Reflection;

namespace ExcellentCvWriter.Api;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
