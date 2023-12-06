using System.Text.RegularExpressions;

namespace Aoc;

public class Day06 : Day
{
	public override void PartA(string[] input)
	{
		var times = Regex.Matches(input[0], @"(\d+)")
			.Select(match => Convert.ToInt32(match.Groups[0].Value))
			.ToArray();

		var distances = Regex.Matches(input[1], @"(\d+)")
			.Select(match => Convert.ToInt32(match.Groups[0].Value))
			.ToArray();

		var prod = 1;

		for (int i = 0; i < times.Length; i++)
		{
			var time = times[i];
			var distance = distances[i];

			var low = (time - Math.Sqrt(time * time - 4 * distance)) / 2;
			var high = (time + Math.Sqrt(time * time - 4 * distance)) / 2;

			var min = (int)Math.Floor(low) + 1;
			var max = (int)Math.Ceiling(high) - 1;

			prod *= max - min + 1;
		}

		Console.WriteLine(prod);
	}

	public override void PartB(string[] input)
	{
		var times = Regex.Matches(input[0], @"(\d+)")
			.Select(match => Convert.ToInt32(match.Groups[0].Value))
			.ToArray();

		var distances = Regex.Matches(input[1], @"(\d+)")
			.Select(match => Convert.ToInt32(match.Groups[0].Value))
			.ToArray();

		var time = Convert.ToInt64(string.Concat(times));
		var distance = Convert.ToInt64(string.Concat(distances));

		var low = (time - Math.Sqrt(time * time - 4 * distance)) / 2;
		var high = (time + Math.Sqrt(time * time - 4 * distance)) / 2;

		var min = (int)Math.Floor(low) + 1;
		var max = (int)Math.Ceiling(high) - 1;

		Console.WriteLine(max - min + 1);
	}
}
