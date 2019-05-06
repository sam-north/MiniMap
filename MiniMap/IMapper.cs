using System.Collections.Generic;

namespace MiniMap
{
    public interface IMapper
    {
        TSourceAndDestination Map<TSourceAndDestination>(TSourceAndDestination source);
        TDestination Map<TDestination>(object source);
        TDestination Map<TDestination>(object source, TDestination destination);
    }
}