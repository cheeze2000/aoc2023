using System.Text.RegularExpressions;

namespace Aoc;

public class Day02 : Day
{
	public override void PartA(string[] input)
	{
		var game = 0;

		var sum = input
			.Select(line =>
			{
				var matches = Regex.Matches(line, @"(\d+) (red|green|blue)");

				var valid = matches.All(match =>
				{
					var count = Convert.ToInt32(match.Groups[1].Value);
					var colour = match.Groups[2].Value;

					return colour switch
					{
						"red" => count <= 12,
						"green" => count <= 13,
						"blue" => count <= 14,
						_ => true,
					};
				});

				return ++game * (valid ? 1 : 0);
			})
			.Sum();

		Console.WriteLine(sum);
	}

	public override void PartB(string[] input)
	{
		var sum = input
			.Select(line =>
			{
				var matches = Regex.Matches(line, @"(\d+) (red|green|blue)");

				var red = 0;
				var green = 0;
				var blue = 0;

				matches.ToList().ForEach(match =>
				{
					var count = Convert.ToInt32(match.Groups[1].Value);
					var colour = match.Groups[2].Value;

					switch (colour)
					{
						case "red":
							red = Math.Max(red, count);
							break;
						case "green":
							green = Math.Max(green, count);
							break;
						case "blue":
							blue = Math.Max(blue, count);
							break;
					};
				});

				return red * green * blue;
			})
			.Sum();

		Console.WriteLine(sum);
	}
}
