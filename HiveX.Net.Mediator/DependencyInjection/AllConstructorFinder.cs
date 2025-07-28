using Autofac.Core.Activators.Reflection;
using System.Reflection;


namespace HiveX.Net.Mediator.DependencyInjection
{
    /// <summary>
    /// Finds all constructors (public and non-public) of a given type.
    /// </summary>
    public class AllConstructorFinder : IConstructorFinder
    {
        /// <summary>
        /// Returns all instance constructors (both public and non-public) of the specified target type.
        /// </summary>
        /// <param name="targetType">The type whose constructors are to be found.</param>
        /// <returns>An array of ConstructorInfo objects representing all constructors of the type.</returns>
        public ConstructorInfo[] FindConstructors(Type targetType)
        {
            return targetType.GetConstructors(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic
            );
        }
    }

}
