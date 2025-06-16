using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadViewer.Common
{
	public class TypeObjectFactoryCache
	{
		private readonly Dictionary<Type, object> _cache = new Dictionary<Type, object>();
		public T GetOrCreate<T>(params object[] args)
		{
			return (T)GetOrCreate(typeof(T), args);
		}

		public object GetOrCreate(Type type, params object[] args)
		{
			if (_cache.TryGetValue(type, out var instance))
				return instance;

			instance = Activator.CreateInstance(type, args);
			_cache[type] = instance;
			return instance;
		}

		public bool Contains<T>() => _cache.ContainsKey(typeof(T));

		public void Remove<T>() => _cache.Remove(typeof(T));

		public void Clear() => _cache.Clear();
	}
}
