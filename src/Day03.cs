namespace Aoc;

public class Day03
{
	public record Number(int Value, int Start, int End);
	public record Symbol(char Value, int Start);

	public static (Dictionary<int, List<Number>>, Dictionary<int, List<Symbol>>) Parse(string[] input)
	{
		Dictionary<int, List<Number>> nums = [];
		Dictionary<int, List<Symbol>> syms = [];

		for (int row = 0; row < input.Length; row++)
		{
			nums[row] = [];
			syms[row] = [];

			var line = input[row];
			int value = default, start = default, end = default;

			var column = -1;
			while (++column <= line.Length)
			{
				var c = column == line.Length ? default : line[column];

				if (value > 0 && !char.IsAsciiDigit(c))
				{
					nums[row].Add(new Number(value, start, end));
					value = start = end = default;

					column--;
					continue;
				}
				else if (column < line.Length)
				{
					if (char.IsAsciiDigit(c))
					{
						if (value == default)
						{
							value = c - '0';
							start = end = column;
						}
						else
						{
							value = value * 10 + c - '0';
							end = column;
						}
					}
					else if (c != '.')
					{
						syms[row].Add(new Symbol(c, column));
					}
				}
			}
		}

		return (nums, syms);
	}

	public static void PartA(string[] input)
	{
		var (rowNums, rowSyms) = Parse(input);

		var sum = rowNums.Select(kv =>
			{
				var row = kv.Key;
				var nums = kv.Value;

				return nums
					.Select(num =>
					{
						var adjacent = rowSyms[row]
							.Concat(row > 0 ? rowSyms[row - 1] : [])
							.Concat(row < input.Length - 1 ? rowSyms[row + 1] : [])
							.Any(sym => sym.Start >= num.Start - 1 && sym.Start <= num.End + 1);

						return adjacent ? num.Value : default;
					})
					.Sum();
			})
			.Sum();

		Console.WriteLine(sum);
	}

	public static void PartB(string[] input)
	{
		var (rowNums, rowSyms) = Parse(input);

		var sum = rowSyms.Select(kv =>
			{
				var row = kv.Key;
				var syms = kv.Value;

				return syms
					.Where(sym => sym.Value == '*')
					.Select(sym =>
					{
						var adjacents = rowNums[row]
							.Concat(row > 0 ? rowNums[row - 1] : [])
							.Concat(row < input.Length - 1 ? rowNums[row + 1] : [])
							.Where(num => sym.Start >= num.Start - 1 && sym.Start <= num.End + 1)
							.ToList();

						return adjacents.Count == 2
							? adjacents[0].Value * adjacents[1].Value
							: default;
					})
					.Sum();
			})
			.Sum();

		Console.WriteLine(sum);
	}
}
