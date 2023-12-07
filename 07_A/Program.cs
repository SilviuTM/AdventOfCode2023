var data = File.ReadAllLines(@".\input.txt");

Dictionary<string, int> bids = new();
Dictionary<string, int> pwr = new();
Dictionary<char, int> signValue = new()
{
    { '2', 0 },
    { '3', 1 },
    { '4', 2 },
    { '5', 3 },
    { '6', 4 },
    { '7', 5 },
    { '8', 6 },
    { '9', 7 },
    { 'T', 8 },
    { 'J', 9 },
    { 'Q', 10 },
    { 'K', 11 },
    { 'A', 12 }
};


// input reading
foreach (string line in data)
{
    bids.Add(line.Split(' ')[0], int.Parse(line.Split(" ")[1]));
}

// computing relevant data
foreach (string hand in bids.Keys)
{
    Dictionary<char, int> occurences = new();
    foreach (char c in hand)
        if (occurences.ContainsKey(c)) occurences[c]++;
        else occurences.Add(c, 1);

    int type = -1;
    if (occurences.ContainsValue(5)) type = 6; // 5 of a kind
    else if (occurences.ContainsValue(4)) type = 5; // 4 of a kind
    else if (occurences.ContainsValue(3) && occurences.ContainsValue(2)) type = 4; // full house
    else if (occurences.ContainsValue(3)) type = 3; // 3 of a kind
    else if (occurences.Values.Count((x) => x == 2) == 2) type = 2; // 2 pairs
    else if (occurences.Values.Count((x) => x == 2) == 1) type = 1; // 1 pair
    else type = 0; // high card

    pwr.Add(hand, type);
}

// problem solver
List<string> orderedHands = bids.Keys.ToList();
orderedHands.Sort((x, y) => 
{ 
    if (pwr[x] < pwr[y]) return 1;
    if (pwr[x] > pwr[y]) return -1;
    for (int i = 0; i < x.Length; i++)
        if (signValue[x[i]] < signValue[y[i]]) return 1;
        else if (signValue[x[i]] > signValue[y[i]]) return -1;

    return 0;
});

int answer = 0;
for (int i = 0; i < orderedHands.Count; i++)
    answer += bids[orderedHands[i]] * (orderedHands.Count - i);

Console.WriteLine(answer);