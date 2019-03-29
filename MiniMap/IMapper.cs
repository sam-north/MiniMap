namespace MiniMap
{
    public interface IMapper
    {
        T Map<T>(T source) where T : new();
        T Map<T>(object source) where T : new();
        T Map<T>(object source, T destination);
    }
}