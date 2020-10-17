using System.IO;
using System.Reflection;

namespace AmmoFinder.Common.Extensions
{
    public static class AssemblyExtensions
    {
        public static string GetXmlComments(this Assembly assembly)
        {
            var commentsFile = assembly.GetName().Name;
            return Path.Combine(System.AppContext.BaseDirectory, $"{commentsFile}.xml");
        }
    }
}
