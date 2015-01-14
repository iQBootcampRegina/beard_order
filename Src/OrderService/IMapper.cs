﻿namespace OrderService
{
	public interface IMapper<in TIn, out TOut>
	{
		TOut Map(TIn input);
	}
}