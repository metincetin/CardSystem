using System.Collections.Generic;

namespace CardSystem
{
	public interface ICastTarget
	{
		bool TryGetReference<T>(out T reference) where T : class;
		T GetFirstReference<T>() where T : class;
		IEnumerable<T> Iterate<T>() where T : class;
	}
}