var data = File.ReadAllLines(@".\input.txt");

Dictionary<string, string> left = new();
Dictionary<string, string> right = new();
List<(string location, long steps)> nodes = new();

// input reading
for (int line = 2; line < data.Length; line++)
{
    string current = data[line][..3];
    string goLeft = data[line].Substring(7, 3);
    string goRight = data[line].Substring(12, 3);

    left.Add(current, goLeft);
    right.Add(current, goRight);

    if (current[2] == 'A') nodes.Add((current, 0));
}

// problem solver
while (true) // we do this so if the instructions run out, we start them again infinitely
    foreach (char c in data[0])
    {
        if (nodes.Where(x => x.location[2] == 'Z').ToList().Count == nodes.Count)
        {
            // if we got here, then all nodes are on 'Z'
            // but while they may be done, they all have a different amount of steps
            // we need to sync them together (bring them to the same number of steps without changing location)
            // therefore we need to re-loop them until they all have the same number of steps
            // ie. lowest common multiple

            List<long> steps = nodes.Select(x => x.steps).ToList();
            long answer = steps.Aggregate(LowestCommonMultiple);
            
            Console.WriteLine(answer);
            return;
        }

        // go through each node. if it's not finished, keep moving on it
        for (int i = 0; i < nodes.Count; i++)
            if (nodes[i].location[2] != 'Z')
            {
                if (c == 'L')
                    nodes[i] = (left[nodes[i].location], nodes[i].steps + 1); // left
                else nodes[i] = (right[nodes[i].location], nodes[i].steps + 1); // otherwise go right
            }
    }

static long LowestCommonMultiple(long a, long b)
{
    return Math.Abs(a * b) / GreatestCommonDivisor(a, b);
}
static long GreatestCommonDivisor(long a, long b)
{
    return b == 0 ? a : GreatestCommonDivisor(b, a % b);
}