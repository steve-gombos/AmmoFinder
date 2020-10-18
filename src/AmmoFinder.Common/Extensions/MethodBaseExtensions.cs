using System.Reflection;
using System.Runtime.CompilerServices;

namespace AmmoFinder.Common.Extensions
{
    public static class MethodBaseExtensions
    {
        public static string GetName(this MethodBase methodBase, [CallerMemberName] string memberName = "")
        {
            //TODO: This is probably bad.
            return methodBase.DeclaringType.DeclaringType.FullName + "." + memberName;
        }
    }
}
