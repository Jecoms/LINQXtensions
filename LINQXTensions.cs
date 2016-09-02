using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQXtensions
{
	public static class LINQXtensions
	{
		/// <summary>
		/// Creates a HashSet<T> from an IEnumerable<T>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source) => new HashSet<T>(source);
		
		/// <summary>
		/// Retrieves the index for the maximum value of the collection.
		/// <para/>Will return -1 if collection is empty or larger than int.MaxValue.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items">Collection of elements to search for the index of the minimum value.</param>
		/// <returns>Index of first found maximum value or -1 if collection is empty or larger than int.MaxValue.</returns>
		public static int MaxIndex<T>(this IEnumerable<T> items)
			where T : IComparable<T>
		{
			var maxIndex = -1;
			if (items != null && items.Count() > 0 && items.LongCount() < int.MaxValue)
			{
				maxIndex = 0;
				var maxValue = items.First();
				var index = 0;

				foreach (var item in items)
				{
					if (item.CompareTo(maxValue) > 0)
					{
						maxIndex = index++;
						maxValue = item;
					}
					else
					{
						index++;
					}
				}
			}
			return maxIndex;
		}

		/// <summary>
		/// Retrieves the index for the maximum value of the collection.
		/// Will return -1 if empty collection or collection count is greater than int.MaxValue.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items">Collection of elements to search for the index of the minimum value.</param>
		/// <returns>Index of first found maximum value or -1 if collection is empty or larger than int.MaxValue.</returns>
		public static int MinIndex<T>(this IEnumerable<T> items)
			where T : IComparable<T>
		{
			var minIndex = -1;
			if (items != null && items.Count() > 0 && items.LongCount() < int.MaxValue)
			{
				minIndex = 0;
				var minValue = items.First();

				var index = 0;
				foreach (var item in items)
				{
					if (item.CompareTo(minValue) < 0)
					{
						minIndex = index++;
						minValue = item;
					}
					else
					{
						index++;
					}
				}
			}
			return minIndex;
		}
	}
}