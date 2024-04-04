using AutoMapper;
using System.Linq;
using System.Reflection;

namespace Greenglobal.Core.Helpers
{
    public static class MapperExtension
    {
        /// <summary>
        /// Ignore all non-exist properties in Source and Destination
        /// </summary>
        /// <typeparam name="TSource">Source Class</typeparam>
        /// <typeparam name="TDestination">Destination Class</typeparam>
        /// <param name="expression">Expression</param>
        /// <returns>Expression</returns>
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof(TSource);
            var destinationProperties = typeof(TDestination).GetProperties(flags);

            // Ignore in Destination
            foreach (var property in destinationProperties)
            {
                if (sourceType.GetProperty(property.Name, flags) == null)
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }

            // Ignore in Source
            foreach (var property in sourceType.GetProperties(flags))
            {
                if (typeof(TDestination).GetProperty(property.Name) == null)
                {
                    expression.ForSourceMember(property.Name, opt => opt.DoNotValidate());
                }
            }

            return expression;
        }

        public static U MapValidValues<U, T>(T source, U destination)
        {
            foreach (var propertyName in source.GetType().GetProperties().Select(p => p.Name))
            {
                var value = source.GetType().GetProperty(propertyName).GetValue(source, null);
                if (value != null)
                {
                    destination.GetType().GetProperty(propertyName).SetValue(destination, value, null);
                }
            }

            return destination;
        }
    }
}
