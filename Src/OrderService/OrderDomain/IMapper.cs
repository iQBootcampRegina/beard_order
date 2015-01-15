namespace OrderService.OrderDomain
{
	public interface IMapper<in TIn, out TOut>
	{
		TOut Map(TIn input);
	}
}