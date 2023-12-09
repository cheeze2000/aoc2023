namespace Aoc;

public class Day08 : Day
{
	public override void PartA(string[] input)
	{
		var directions = input[0];
		var map = new Dictionary<string, (string L, string R)>();

		for (int i = 2; i < input.Length; i++)
		{
			var line = input[i];
			var from = line[0..3];
			var left = line[7..10];
			var right = line[12..15];

			map[from] = (left, right);
		}

		var current = "AAA";
		var index = 0;
		var steps = 0;

		while (current != "ZZZ")
		{
			var direction = directions[index];
			var (left, right) = map[current];

			current = direction switch
			{
				'L' => left,
				'R' => right,
				_ => "ZZZ",
			};

			index = (index + 1) % directions.Length;
			steps++;
		}

		Console.WriteLine(steps);
	}

	public override void PartB(string[] input)
	{
		var directions = input[0];
		var map = new Dictionary<string, (string L, string R)>();
		var cycles = new List<long>();

		for (int i = 2; i < input.Length; i++)
		{
			var line = input[i];
			var from = line[0..3];
			var left = line[7..10];
			var right = line[12..15];

			map[from] = (left, right);
		}

		var candidates = map.Keys.Where(key => key.EndsWith('A')).ToList();

		foreach (var candidate in candidates)
		{
			var index = 0;
			var steps = 0L;

			var current = candidate;

			while (!current.EndsWith('Z'))
			{
				current = directions[index] == 'L'
					? map[current].L
					: map[current].R;

				index = (index + 1) % directions.Length;
				steps++;
			}

			cycles.Add(steps);
		}

		var lcm = cycles.Aggregate(Lcm);

		Console.WriteLine(lcm);
	}

	private static long Lcm(long a, long b)
	{
		return a / Gcd(a, b) * b;
	}

	private static long Gcd(long a, long b)
	{
		return b == 0 ? a : Gcd(b, a % b);
	}
}
