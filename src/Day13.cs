namespace Aoc;

public class Day13 : Day
{
	private static List<List<string>> ParsePatterns(string[] input)
	{
		var patterns = new List<List<string>>();
		var pattern = new List<string>();

		for (int i = 0; i <= input.Length; i++)
		{
			if (i == input.Length || string.IsNullOrEmpty(input[i]))
			{
				patterns.Add(pattern);
				pattern = [];
				continue;
			}

			pattern.Add(input[i]);
		}

		return patterns;
	}

	private static string Row(List<string> pattern, int row) => pattern[row];
	private static string Col(List<string> pattern, int col) => string.Concat(pattern.Select(s => s[col]));

	private static bool IsHorizontalMirror(List<string> pattern, int row)
	{
		var i = row;
		var j = row + 1;

		while (i >= 0 && j < pattern.Count)
		{
			if (Row(pattern, i) != Row(pattern, j))
			{
				return false;
			}

			i--;
			j++;
		}

		return true;
	}

	private static bool IsAlmostHorizontalMirror(List<string> pattern, int row)
	{
		var i = row;
		var j = row + 1;

		var mismatch = 0;

		while (i >= 0 && j < pattern.Count)
		{
			var r1 = Row(pattern, i);
			var r2 = Row(pattern, j);

			for (int k = 0; k < r1.Length; k++)
			{
				mismatch += r1[k] == r2[k] ? 0 : 1;
			}

			if (mismatch > 1)
			{
				return false;
			}

			i--;
			j++;
		}

		return mismatch == 1;
	}

	private static bool IsVerticalMirror(List<string> pattern, int col)
	{
		var i = col;
		var j = col + 1;

		while (i >= 0 && j < pattern.First().Length)
		{
			if (Col(pattern, i) != Col(pattern, j))
			{
				return false;
			}

			i--;
			j++;
		}

		return true;
	}

	private static bool IsAlmostVerticalMirror(List<string> pattern, int col)
	{
		var i = col;
		var j = col + 1;

		var mismatch = 0;

		while (i >= 0 && j < pattern.First().Length)
		{
			var c1 = Col(pattern, i);
			var c2 = Col(pattern, j);

			for (int k = 0; k < c1.Length; k++)
			{
				mismatch += c1[k] == c2[k] ? 0 : 1;
			}

			if (mismatch > 1)
			{
				return false;
			}

			i--;
			j++;
		}

		return mismatch == 1;
	}

	public override void PartA(string[] input)
	{
		var patterns = ParsePatterns(input);

		var sum = patterns
			.Select(pattern =>
			{
				for (int i = 0; i < pattern.Count - 1; i++)
				{
					if (IsHorizontalMirror(pattern, i))
					{
						return ++i * 100;
					}
				}

				for (int i = 0; i < pattern.First().Length - 1; i++)
				{
					if (IsVerticalMirror(pattern, i))
					{
						return ++i;
					}
				}

				return default;
			})
			.Sum();

		Console.WriteLine(sum);
	}

	public override void PartB(string[] input)
	{
		var patterns = ParsePatterns(input);

		var sum = patterns
			.Select(pattern =>
			{
				for (int i = 0; i < pattern.Count - 1; i++)
				{
					if (IsAlmostHorizontalMirror(pattern, i))
					{
						return ++i * 100;
					}
				}

				for (int i = 0; i < pattern.First().Length - 1; i++)
				{
					if (IsAlmostVerticalMirror(pattern, i))
					{
						return ++i;
					}
				}

				return default;
			})
			.Sum();

		Console.WriteLine(sum);
	}
}
