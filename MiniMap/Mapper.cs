using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MiniMap
{
    public class Mapper : IMapper
    {
        int maxCloneLevel = 2;
        public List<Type> SystemTypes = new List<Type>
        {
            typeof(bool),
            typeof(byte),
            typeof(sbyte),
            typeof(char),
            typeof(decimal),
            typeof(float),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(object),
            typeof(short),
            typeof(ushort),
            typeof(string)
        };

        public List<Type> PrioritizedCollectionTypes = new List<Type>
        {
            typeof(IList<>),
            typeof(ICollection<>),
            typeof(IEnumerable<>)
        };

        public TSourceAndDestination Map<TSourceAndDestination>(TSourceAndDestination source)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            var tType = typeof(TSourceAndDestination);
            var tInstance = Activator.CreateInstance(tType);
            return Map(source, (TSourceAndDestination)tInstance);
        }
        public TDestination Map<TDestination>(object source)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            var tDestinationInstance = Activator.CreateInstance(typeof(TDestination));
            return Map(source, (TDestination)tDestinationInstance);
        }
        public T Map<T>(object source, T destination)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (destination == null)
                throw new ArgumentNullException("destination");

            if (IsCollection(source) || IsCollection(destination))
            {
                if (!(IsCollection(source) && IsCollection(destination)))
                    throw new CollectionMismatchException();
                return MapCollection(source, destination);
            }
            return MapSingle(source, destination);
        }

        private T MapSingle<T>(object source, T destination)
        {
            var sourceClone = Clone(source);
            var destProperties = destination.GetType().GetProperties();
            for (int i = 0; i < destProperties.Length; i++)
            {
                var srcProperty = sourceClone.GetType().GetProperty(destProperties[i].Name);
                if (srcProperty != null)
                {
                    if (!destProperties[i].PropertyType.Equals(srcProperty.PropertyType) && !SystemTypes.Contains(destProperties[i].PropertyType))
                        throw new PropertyTypeMismatchException(Environment.NewLine + nameof(sourceClone) + " - [" + srcProperty.PropertyType.ToString() + "] " + sourceClone.GetType().ToString() + "." + srcProperty.Name + Environment.NewLine + nameof(destination) + " - [" + destProperties[i].PropertyType.ToString() + "] " + destination.GetType().ToString() + "." + destProperties[i].Name);
                    if (destProperties[i].GetSetMethod() != null)
                        destProperties[i].SetValue(destination, srcProperty.GetValue(sourceClone));
                }
            }
            return destination;
        }

        private object Clone(object source)
        {
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(source), source.GetType());
        }

        private TDestination MapCollection<TDestination>(object source, TDestination destination)
        {
            IList collection;
            var destinationCollection = destination as IEnumerable<object>;
            using (var destinationEnumerator = destinationCollection.GetEnumerator())
            {
                destinationEnumerator.MoveNext();
                var itemType = GetCollectionGenericType(destination);
                collection = CreateList(itemType);
                foreach (var item in destinationCollection)
                    collection.Add(Map(item, Activator.CreateInstance(itemType)));
            }
            var sourceCollection = source as IEnumerable<object>;
            using (var sourceEnumerator = sourceCollection.GetEnumerator())
            {
                sourceEnumerator.MoveNext();
                var itemType = GetCollectionGenericType(destination);
                foreach (var item in sourceCollection)
                    collection.Add(Map(item, Activator.CreateInstance(itemType)));
            }
            return (TDestination)collection;
        }
        private Type GetCollectionGenericType<TDestination>(TDestination destination)
        {
            var item = destination.GetType().GenericTypeArguments;
            if (item != null && item[0] != null)
                return item[0];
            else
                throw new MissingGenericTypeFromCollectionException();
        }
        private bool IsCollection(object obj)
        {
            var objInterfaces = obj.GetType().GetInterfaces();
            foreach (var collectionType in PrioritizedCollectionTypes)
            {
                foreach (var item in objInterfaces)
                {
                    if (item.Name == collectionType.Name)
                        return true;
                }
            }
            return false;
        }
        private IList CreateList(Type myType)
        {
            Type genericListType = typeof(List<>).MakeGenericType(myType);
            return (IList)Activator.CreateInstance(genericListType);
        }
    }
}