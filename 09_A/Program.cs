var data = File.ReadAllLines(@".\input.txt");

long answer = 0;
foreach (string line in data)
{
    List<List<long>> numberLevels = new();
    numberLevels.Add(line.Split(' ').ToList().Select(x => long.Parse(x)).ToList());

    while (numberLevels[^1].Where(x => x == 0).ToList().Count != numberLevels[^1].Count)
    {
        List<long> newSequence = new();
        for (int i = 0; i < numberLevels[^1].Count - 1; i++)
            newSequence.Add(numberLevels[^1][i + 1] - numberLevels[^1][i]);

        numberLevels.Add(newSequence);
    }

    numberLevels[^1].Add(0);
    for (int i = numberLevels.Count - 2; i >= 0; i--)
        numberLevels[i].Add(numberLevels[i + 1][^1] + numberLevels[i][^1]);

    answer += numberLevels[0][^1];
}

Console.WriteLine(answer);