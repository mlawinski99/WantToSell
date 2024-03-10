namespace WantToSell.Domain.Mappers;

public interface IEnumerableMapper<T1,T2>
{
    Task<IEnumerable<T2>> Map(IEnumerable<T1> models);
}