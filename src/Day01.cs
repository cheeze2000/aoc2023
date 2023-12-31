namespace Aoc;

public class Day01 : Day
{
	public override void PartA(string[] input)
	{
		var sum = input
			.Select(line => line
				.Where(char.IsAsciiDigit)
				.Select(c => c - '0')
				.Aggregate(0, (v, c) => v == 0 ? c * 11 : v - v % 10 + c)
			)
			.Sum();

		Console.WriteLine(sum);
	}

	public override void PartB(string[] input)
	{
		var values = new Dictionary<string, int>
		{
			["one"] = 1,
			["two"] = 2,
			["three"] = 3,
			["four"] = 4,
			["five"] = 5,
			["six"] = 6,
			["seven"] = 7,
			["eight"] = 8,
			["nine"] = 9,
		};

		for (int i = 1; i <= 9; i++)
		{
			values[i.ToString()] = i;
		}

		var sum = input
			.Select(line =>
			{
				int start = default, end = default;

				int startIndex = int.MaxValue;
				int endIndex = int.MinValue;

				foreach (var (k, v) in values)
				{
					var i = line.IndexOf(k);
					var j = line.LastIndexOf(k);

					if (i >= 0 && i < startIndex)
					{
						start = v;
						startIndex = i;
					}

					if (j >= 0 && j > endIndex)
					{
						end = v;
						endIndex = j;
					}
				}

				return start * 10 + end;
			})
			.Sum();

		Console.WriteLine(sum);
	}
}
