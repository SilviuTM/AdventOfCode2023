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

    numberLevels[^1].Insert(0, 0);
    for (int i = numberLevels.Count - 2; i >= 0; i--)
        numberLevels[i].Insert(0, numberLevels[i][0] - numberLevels[i + 1][0]);

    answer += numberLevels[0][0];
}

Console.WriteLine(answer);