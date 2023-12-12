namespace Aoc;

using Coord = (int x, int y);

public class Day11 : Day
{
	public record Universe
	{
		public required List<Coord> Galaxies;
		public required List<int> EmptyRows;
		public required List<int> EmptyCols;
	}

	private static bool IsOrdered(int a, int b, int c) => (a < b && b < c) || (a > b && b > c);

	private static Universe ParseUniverse(string[] input)
	{
		var galaxies = new List<Coord>();
		var rows = new HashSet<int>();
		var cols = new HashSet<int>();

		for (int i = 0; i < input.Length; i++)
		{
			for (int j = 0; j < input[i].Length; j++)
			{
				if (input[i][j] == '#')
				{
					galaxies.Add((i, j));
					rows.Add(i);
					cols.Add(j);
				}
			}
		}

		var emptyRows = Enumerable.Range(0, input.Length)
			.Where(n => !rows.Contains(n))
			.ToList();

		var emptyCols = Enumerable.Range(0, input.First().Length)
			.Where(n => !cols.Contains(n))
			.ToList();

		return new Universe
		{
			Galaxies = galaxies,
			EmptyRows = emptyRows,
			EmptyCols = emptyCols,
		};
	}

	public override void PartA(string[] input)
	{
		var universe = ParseUniverse(input);
		var galaxies = universe.Galaxies;
		var emptyRows = universe.EmptyRows;
		var emptyCols = universe.EmptyCols;

		var sum = 0;

		for (int i = 0; i < galaxies.Count; i++)
		{
			for (int j = i + 1; j < galaxies.Count; j++)
			{
				var (g1, g2) = (galaxies[i], galaxies[j]);

				var dx = Math.Abs(g1.x - g2.x);
				var dy = Math.Abs(g1.y - g2.y);

				var expandedRows = emptyRows.Where(r => IsOrdered(g1.x, r, g2.x)).Count();
				var expandedCols = emptyCols.Where(c => IsOrdered(g1.y, c, g2.y)).Count();

				sum += dx + dy + expandedRows + expandedCols;
			}
		}

		Console.WriteLine(sum);
	}

	public override void PartB(string[] input)
	{
		var universe = ParseUniverse(input);
		var galaxies = universe.Galaxies;
		var emptyRows = universe.EmptyRows;
		var emptyCols = universe.EmptyCols;

		var sum = 0L;

		for (int i = 0; i < galaxies.Count; i++)
		{
			for (int j = i + 1; j < galaxies.Count; j++)
			{
				var (g1, g2) = (galaxies[i], galaxies[j]);

				var dx = Math.Abs(g1.x - g2.x);
				var dy = Math.Abs(g1.y - g2.y);

				var expandedRows = emptyRows.Where(r => IsOrdered(g1.x, r, g2.x)).Count();
				var expandedCols = emptyCols.Where(c => IsOrdered(g1.y, c, g2.y)).Count();

				sum += dx + dy + (expandedRows + expandedCols) * 999999;
			}
		}

		Console.WriteLine(sum);
	}
}
