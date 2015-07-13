using System;
using System.Collections.Generic;
using System.Linq;

namespace Jok.Shared
{
	public static class Extensions
	{
		public static T GetRandom<T>(this List<T> items)
			where T: class
		{
			if (items.Count == 0)
				return null;

			if (items.Count == 1)
				return items[0];

			return items[new Random(Guid.NewGuid().ToByteArray().Sum(x => x)).Next(items.Count)];
		}
	}
}
