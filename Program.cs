using System.Diagnostics;

namespace Aoc;

public class Program
{
	[Flags]
	enum Part
	{
		A = 0b01,
		B = 0b10,
	}

	public static void Main(string[] args)
	{
		if (args.Length != 1)
		{
			Console.WriteLine("e.g., dotnet run 3, dotnet run 5a, dotnet run 10b");
			return;
		}

		var arg = args[0];
		int day;
		Part part;

		if (arg.EndsWith('a') || arg.EndsWith('b'))
		{
			day = Convert.ToInt32(arg.Remove(arg.Length - 1));
			part = Enum.Parse<Part>(arg.Last().ToString(), true);
		}
		else
		{
			day = Convert.ToInt32(arg);
			part = Part.A | Part.B;
		}

		var name = $"Day{day:D2}";
		var type = Type.GetType($"Aoc.{name}");

		if (type == null)
		{
			Console.WriteLine($"Have you solved {name} yet?");
			return;
		}

		var partA = type.GetMethod("PartA");
		var partB = type.GetMethod("PartB");

		var input = File.ReadAllText($"inputs/{name}.txt");
		var lines = File.ReadAllLines($"inputs/{name}.txt");

		if (part.HasFlag(Part.A))
		{
			if (partA == null)
			{
				Console.WriteLine($"Have you solved {name} part A yet?");
				return;
			}

			var param = partA.GetParameters().First();

			Execute(param.ParameterType == typeof(string[])
				? () => partA.Invoke(null, [lines])
				: () => partA.Invoke(null, [input]));
		}

		if (part.HasFlag(Part.B))
		{
			if (partB == null)
			{
				Console.WriteLine($"Have you solved {name} part B yet?");
				return;
			}

			var param = partB.GetParameters().First();

			Execute(param.ParameterType == typeof(string[])
				? () => partB.Invoke(null, [lines])
				: () => partB.Invoke(null, [input]));
		}
	}

	private static void Execute(Action action)
	{
		Console.Write("Output: ");

		var stopwatch = new Stopwatch();
		stopwatch.Start();
		action();
		stopwatch.Stop();

		Console.WriteLine($"Time taken: {stopwatch.ElapsedMilliseconds} ms");
	}
}
