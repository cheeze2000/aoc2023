namespace Aoc;

public class Day07 : Day
{
	private static readonly string RankPart1 = "23456789TJQKA";
	private static readonly string RankPart2 = "J23456789TQKA";

	private static double CalculateScore(string hand)
	{
		var counts = new Dictionary<char, int>();
		var dupes = new int[6];
		dupes[0] = 5;

		foreach (var card in hand)
		{
			if (counts.ContainsKey(card))
			{
				counts[card]++;
			}
			else
			{
				counts[card] = 1;
			}

			dupes[counts[card] - 1]--;
			dupes[counts[card]]++;
		}

		if (dupes[5] == 1)
		{
			return 5;
		}
		else if (dupes[4] == 1)
		{
			return 4;
		}
		else if (dupes[3] == 1)
		{
			return dupes[2] == 1 ? 3.5 : 3;
		}
		else if (dupes[2] >= 1)
		{
			return dupes[2] == 2 ? 2.5 : 2;
		}
		else
		{
			return 1;
		}
	}

	private static double CalculateScoreWithJokers(string hand)
	{
		var counts = new Dictionary<char, int>();
		var dupes = new int[6];
		var jokers = 0;
		dupes[0] = 5;

		foreach (var card in hand)
		{
			if (card == 'J')
			{
				jokers++;
				continue;
			}
			else if (counts.ContainsKey(card))
			{
				counts[card]++;
			}
			else
			{
				counts[card] = 1;
			}

			dupes[counts[card] - 1]--;
			dupes[counts[card]]++;
		}

		for (int i = 5; i >= 0; i--)
		{
			if (dupes[i] == 0)
			{
				continue;
			}

			dupes[i + jokers]++;
			dupes[i]--;
			break;
		}

		if (dupes[5] == 1)
		{
			return 5;
		}
		else if (dupes[4] == 1)
		{
			return 4;
		}
		else if (dupes[3] == 1)
		{
			return dupes[2] == 1 ? 3.5 : 3;
		}
		else if (dupes[2] >= 1)
		{
			return dupes[2] == 2 ? 2.5 : 2;
		}
		else
		{
			return 1;
		}
	}

	private static int Tiebreaker(string cardRanks, string hand1, string hand2)
	{

		for (int i = 0; i < 5; i++)
		{
			var score1 = cardRanks.IndexOf(hand1[i]);
			var score2 = cardRanks.IndexOf(hand2[i]);

			if (score1 == score2)
			{
				continue;
			}

			return score1.CompareTo(score2);
		}

		return 0;
	}

	public override void PartA(string[] input)
	{
		var set = input
			.Select(str =>
			{
				var split = str.Split(' ');
				return (split[0], Convert.ToInt32(split[1]));
			})
			.ToList();

		set.Sort((pair1, pair2) =>
		{
			var hand1 = pair1.Item1;
			var hand2 = pair2.Item1;
			var score1 = CalculateScore(hand1);
			var score2 = CalculateScore(hand2);

			return score1 == score2
				? Tiebreaker(RankPart1, hand1, hand2)
				: score1.CompareTo(score2);
		});

		var winnings = set
			.Select((pair, index) => pair.Item2 * (index + 1))
			.Sum();

		Console.WriteLine(winnings);
	}

	public override void PartB(string[] input)
	{
		var set = input
			.Select(str =>
			{
				var split = str.Split(' ');
				return (split[0], Convert.ToInt32(split[1]));
			})
			.ToList();

		set.Sort((pair1, pair2) =>
		{
			var hand1 = pair1.Item1;
			var hand2 = pair2.Item1;
			var score1 = CalculateScoreWithJokers(hand1);
			var score2 = CalculateScoreWithJokers(hand2);

			return score1 == score2
				? Tiebreaker(RankPart2, hand1, hand2)
				: score1.CompareTo(score2);
		});

		var winnings = set
			.Select((pair, index) => pair.Item2 * (index + 1))
			.Sum();

		Console.WriteLine(winnings);
	}
}
