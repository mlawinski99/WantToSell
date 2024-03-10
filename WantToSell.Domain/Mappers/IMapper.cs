namespace WantToSell.Domain.Mappers;

public interface IMapper<T1,T2>
{
    Task<T2> Map(T1 model);
}