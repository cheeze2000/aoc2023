using System.Text.RegularExpressions;

namespace Aoc;

public class Day05 : Day
{
	public record Range(long Start, long End, int MapsDone);
	public record Triple(long Start, long End, long Delta);

	private static long[] ParseSeeds(string[] input)
	{
		return Regex.Matches(input[0], @"(\d+)")
			.Select(match => Convert.ToInt64(match.Groups[0].Value))
			.ToArray();
	}

	private static long[][] ParseSeedPairs(string[] input)
	{
		return ParseSeeds(input).Chunk(2).ToArray();
	}

	private static List<List<Triple>> ParseMaps(string[] input)
	{
		var maps = new List<List<Triple>>();
		var map = new List<Triple>();

		for (int i = 3; i <= input.Length; i++)
		{
			if (i >= input.Length || input[i].Length == 0)
			{
				map.Sort((t1, t2) => t1.Start.CompareTo(t2.Start));
				maps.Add(map);

				map = [];
				i++;

				continue;
			}

			var nums = input[i]
				.Split(' ')
				.Select(n => Convert.ToInt64(n))
				.ToArray();

			var triple = new Triple(nums[1], nums[1] + nums[2] - 1, nums[0] - nums[1]);

			map.Add(triple);
		}

		return maps;
	}

	public override void PartA(string[] input)
	{
		var seeds = ParseSeeds(input);
		var maps = ParseMaps(input);

		var lowest = seeds
			.Select(seed =>
			{
				foreach (var map in maps)
				{
					foreach (var triple in map)
					{
						if (seed < triple.Start || seed > triple.End)
						{
							continue;
						}

						seed += triple.Delta;

						break;
					}
				}

				return seed;
			})
			.Min();

		Console.WriteLine(lowest);
	}

	public override void PartB(string[] input)
	{
		var pairs = ParseSeedPairs(input);
		var maps = ParseMaps(input);

		var queue = new Queue<Range>();

		foreach (var pair in pairs)
		{
			var range = new Range(pair[0], pair[0] + pair[1] - 1, 0);
			queue.Enqueue(range);
		}

		var lowest = long.MaxValue;

		while (queue.Count > 0)
		{
			var range = queue.Dequeue();

			if (range.Start > range.End)
			{
				continue;
			}

			if (range.MapsDone == maps.Count)
			{
				lowest = Math.Min(lowest, range.Start);
				continue;
			}

			var cursor = range.Start;

			foreach (var triple in maps[range.MapsDone])
			{
				if (triple.End < range.Start)
				{
					continue;
				}

				if (triple.Start > range.End)
				{
					break;
				}

				if (triple.Start > cursor)
				{
					var segment1 = new Range(
						cursor,
						triple.Start - 1,
						range.MapsDone + 1
					);

					cursor = triple.Start;
					queue.Enqueue(segment1);
				}

				var segment2 = new Range(
					cursor + triple.Delta,
					Math.Min(triple.End, range.End) + triple.Delta,
					range.MapsDone + 1
				);

				cursor = Math.Min(triple.End, range.End) + 1;
				queue.Enqueue(segment2);
			}

			if (cursor <= range.End)
			{
				var segment3 = new Range(
					cursor,
					range.End,
					range.MapsDone + 1
				);

				queue.Enqueue(segment3);
			}
		}

		Console.WriteLine(lowest);
	}
}
