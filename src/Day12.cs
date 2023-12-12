namespace Aoc;

public class Day12 : Day
{
	private static string currentSprings = string.Empty;
	private static List<int> currentNumbers = [];
	private readonly static Dictionary<(int, int), long> cache = [];

	private static List<(string, List<int>)> ParseInput(string[] input)
	{
		return input
			.Select(line =>
			{
				var springs = line.Split(' ')[0];
				var csv = line.Split(' ')[1];
				var nums = csv.Split(',').Select(n => Convert.ToInt32(n)).ToList();

				return (springs, nums);
			})
			.ToList();
	}

	private static long CalculateCombinations(int i, int j)
	{
		if (cache.ContainsKey((i, j)))
		{
			return cache[(i ,j)];
		}

		if (i >= currentSprings.Length && j >= currentNumbers.Count)
		{
			return 1;
		}

		if (i >= currentSprings.Length)
		{
			return 0;
		}

		if (j >= currentNumbers.Count)
		{
			return currentSprings[i..].Contains('#') ? 0 : 1;
		}

		var character = currentSprings[i];
		var number = currentNumbers[j];

		if (i + number > currentSprings.Length)
		{
			return 0;
		}

		var substr = currentSprings.Substring(i, number);
		var combinations = 0L;

		if (character != '#')
		{
			combinations += CalculateCombinations(i + 1, j);
		}

		if (!substr.Contains('.') && (i + number == currentSprings.Length || currentSprings[i + number] != '#'))
		{
			combinations += CalculateCombinations(i + number + 1, j + 1);
		}

		cache[(i, j)] = combinations;

		return combinations;
	}

	public override void PartA(string[] input)
	{
		var lines = ParseInput(input);
		var sum = lines
			.Select(pair =>
			{
				currentSprings = pair.Item1;
				currentNumbers = pair.Item2;
				cache.Clear();

				return CalculateCombinations(0, 0);
			})
			.Sum();

		Console.WriteLine(sum);
	}

	public override void PartB(string[] input)
	{
		var lines = ParseInput(input);
		var sum = lines
			.Select(pair =>
			{
				currentSprings = string.Join('?', Enumerable.Repeat(pair.Item1, 5));
				currentNumbers = Enumerable.Repeat(pair.Item2, 5).SelectMany(x => x).ToList();
				cache.Clear();

				return CalculateCombinations(0, 0);
			})
			.Sum();

		Console.WriteLine(sum);
	}
}
