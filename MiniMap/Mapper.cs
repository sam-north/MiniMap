using System;

namespace MiniMap
{
    public class Mapper : IMapper
    {
        public T Map<T>(T source) where T : new()
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            return Map(source, new T());
        }
        public T Map<T>(object source) where T : new()
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            return Map(source, new T());
        }
        public T Map<T>(object source, T destination)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            var srcProperties = source.GetType().GetProperties();
            var destProperties = destination.GetType().GetProperties();
            for (int i = 0; i < destProperties.Length; i++)
            {
                var srcProperty = source.GetType().GetProperty(destProperties[i].Name);
                if (srcProperty != null)
                {
                    if (!destProperties[i].PropertyType.Equals(srcProperty.PropertyType))
                        throw new PropertyTypeMismatchException(Environment.NewLine + nameof(source) + " - [" + srcProperty.PropertyType.ToString() + "] " + source.GetType().ToString() + "." + srcProperty.Name + Environment.NewLine + nameof(destination) + " - [" + destProperties[i].PropertyType.ToString() + "] " + destination.GetType().ToString() + "." + destProperties[i].Name);
                    if (destProperties[i].GetSetMethod() != null)
                        destProperties[i].SetValue(destination, srcProperty.GetValue(source));
                }
            }
            return destination;
        }
    }
}