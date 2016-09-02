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

		/// <summary>
		/// Computes Population Standard Deviation
		/// </summary>
		/// <param name="values"></param>
		/// <returns>
		/// </returns>
		public static double StandardDeviation(this IEnumerable<double> values)
		{
			var mean = values.Average();
			return Math.Sqrt(values.Average(x => Math.Pow(x - mean, 2)));
		}

		/// <summary>
		/// Computes Sample Standard Deviation
		/// </summary>
		/// <param name="values"></param>
		/// <returns>
		/// </returns>
		public static double StdDev(this IEnumerable<double> values)
		{
			var mean = values.Average();
			return Math.Sqrt( values.Sum(x => Math.Pow(x - mean, 2)) / (values.Count() - 1));
		}

		/// <summary>
		/// Returns the most common element of collection
		/// <para/>
		/// If elements are tied for most common, only one is returned at random. 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static T MostCommon<T>(this IEnumerable<T> source)
			where T : IComparable<T>
		{
			return source.GroupBy(s => s).OrderByDescending(s => s.Count()).FirstOrDefault().FirstOrDefault();
		}

		/// <summary>
		/// Returns the count of distinct elements in collection
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static int DistinctCount<T>(this IEnumerable<T> source)
			where T : IComparable<T>
		{
			return source.Distinct().Count();
		}

		// BONUS: This will be moved to a MathXtensions library

		/// <summary>
		/// Returns nearest prime number less than or equal to the given ceiling. 
		/// <para/>
		/// Input must be less than sqrt of int.MaxValue (46340)
		/// <para/>
		/// Implements sieve of Eratosthenes
		/// </summary>
		/// <param name="ceiling"></param>
		/// <returns></returns>
		public static int NearestPreviousPrime(int ceiling)
		{
			if (ceiling < 1 || ceiling > 46340) return -1; // 46340 == (int)Math.Sqrt(int.MaxValue));

			var numbers = Enumerable.Range(0, ceiling+1).ToList();
			
			for(var i = 2; i <= ceiling; i++)
			{
				for (var k = (int)Math.Pow(i, 2); k <= ceiling; k += i)
				{
					if (numbers[k] != 0) numbers[k] = 0;
				}
			}

			return numbers.Last(x => x != 0);
		}
	}
}