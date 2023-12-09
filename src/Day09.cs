namespace Aoc;

public class Day09 : Day
{
	private static long Extrapolate(List<long> nums)
	{
		if (nums.All(num => num == 0))
		{
			return 0;
		}

		var next = new List<long>();

		for (int i = 0; i < nums.Count - 1; i++)
		{
			next.Add(nums[i + 1] - nums[i]);
		}

		return nums.Last() + Extrapolate(next);
	}

	public override void PartA(string[] input)
	{
		var sum = input
			.Select(line =>
			{
				var nums = line
					.Split(' ')
					.Select(num => Convert.ToInt64(num))
					.ToList();

				return Extrapolate(nums);
			})
			.Sum();

		Console.WriteLine(sum);
	}

	public override void PartB(string[] input)
	{
		var reversed = input
			.Select(line => string.Join(' ', line.Split(' ').Reverse()))
			.ToArray();

		PartA(reversed);
	}
}
