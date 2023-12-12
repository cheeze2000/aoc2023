namespace Aoc;

using Coord = (int x, int y);

public class Day10 : Day
{
	private static readonly string LEFT = "-S7J";
	private static readonly string DOWN = "|SF7";
	private static readonly string UP = "|SLJ";
	private static readonly string RIGHT = "-SFL";

	private static HashSet<Coord> FindLoop(string[] input)
	{
		var start = (0, 0);

		var row = input.Length;
		var col = input.First().Length;

		for (int i = 0; i < row; i++)
		{
			for (int j = 0; j < col; j++)
			{
				if (input[i][j] == 'S')
				{
					start = (i, j);
					break;
				}
			}
		}

		var visited = new HashSet<Coord>();
		var previous = new Dictionary<Coord, Coord>();

		var stack = new List<Coord> { start };
		previous[start] = start;

		while (true)
		{
			var coord = stack.Last();
			stack.RemoveAt(stack.Count - 1);

			var x = coord.x;
			var y = coord.y;
			var symbol = input[x][y];

			if (visited.Contains(coord))
			{
				if (symbol == 'S')
				{
					var loop = new HashSet<Coord>();

					do
					{
						loop.Add(coord);
						coord = previous[coord];
					}
					while (coord != start);

					return loop;
				}

				continue;
			}

			visited.Add(coord);

			if (LEFT.Contains(symbol) && y - 1 >= 0)
			{
				var next = (x, y - 1);
				if (RIGHT.Contains(input[x][y - 1]) && previous[coord] != next)
				{
					stack.Add(next);
					previous[next] = coord;
				}
			}

			if (DOWN.Contains(symbol) && x + 1 < row)
			{
				var next = (x + 1, y);
				if (UP.Contains(input[x + 1][y]) && previous[coord] != next)
				{
					stack.Add(next);
					previous[next] = coord;
				}
			}

			if (UP.Contains(symbol) && x - 1 >= 0)
			{
				var next = (x - 1, y);
				if (DOWN.Contains(input[x - 1][y]) && previous[coord] != next)
				{
					stack.Add(next);
					previous[next] = coord;
				}
			}

			if (RIGHT.Contains(symbol) && y + 1 < col)
			{
				var next = (x, y + 1);
				if (LEFT.Contains(input[x][y + 1]) && previous[coord] != next)
				{
					stack.Add(next);
					previous[next] = coord;
				}
			}
		}
	}

	public override void PartA(string[] input)
	{
		var loop = FindLoop(input);

		Console.WriteLine(loop.Count / 2);
	}

	public override void PartB(string[] input)
	{
		var loop = FindLoop(input);
		var ans = 0;

		for (int i = 0; i < input.Length; i++)
		{
			for (int j = 0; j < input[i].Length; j++)
			{
				if (loop.Contains((i, j)))
				{
					continue;
				}

				int intersections = 0;

				for (int k = j; k >= 0; k--)
				{
					intersections += loop.Contains((i, k)) && DOWN.Contains(input[i][k])
						? 1
						: 0;
				}

				ans += intersections % 2;
			}
		}

		Console.WriteLine(ans);
	}
}
