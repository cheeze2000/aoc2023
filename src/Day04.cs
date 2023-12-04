namespace Aoc;

public class Day04
{
	public static List<HashSet<int>[]> Parse(string[] input)
	{
		return input
			.Select<string, HashSet<int>[]>(line =>
			{
				var cards = line.Split(": ")[1].Split(" | ");
				var numbers = cards
					.Select(side => side
						.Split(' ')
						.Where(str => str.Length > 0)
						.Select(num => Convert.ToInt32(num))
						.ToHashSet()
					)
					.ToList();

				return [numbers[0], numbers[1]];
			})
			.ToList();
	}

	public static void PartA(string[] input)
	{
		var sum = Parse(input)
			.Select(numbers =>
			{
				var matchings = numbers[0].Intersect(numbers[1]).Count();

				return matchings == 0
					? 0
					: 1 << --matchings;
			})
			.Sum();

		Console.WriteLine(sum);
	}

	public static void PartB(string[] input)
	{
		var matchings = Parse(input)
			.Select(numbers => numbers[0].Intersect(numbers[1]).Count())
			.ToList();

		var copies = Enumerable.Repeat(1, input.Length).ToArray();

		for (int i = 0; i < input.Length; i++)
		{
			for (int j = i + 1; j < i + 1 + matchings[i]; j++)
			{
				copies[j] += copies[i];
			}
		}

		Console.WriteLine(copies.Sum());
	}
}
