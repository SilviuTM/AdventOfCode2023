var data = File.ReadAllLines(@".\input.txt");

Dictionary<string, string> left = new();
Dictionary<string, string> right = new();

string curLocation= "AAA";

// input reading
for (int line = 2; line < data.Length; line++)
{
    string current = data[line][..3];
    string goLeft = data[line].Substring(7, 3);
    string goRight = data[line].Substring(12, 3);

    left.Add(current, goLeft);
    right.Add(current, goRight);
}

// problem solver
int steps = 0;
while (true) // we do this so if the instructions run out, we start them again infinitely
    foreach (char c in data[0])
    {
        if (curLocation.Equals("ZZZ"))
        {
            Console.WriteLine(steps);
            return;
        }

        if (c == 'L') curLocation = left[curLocation]; // left
        else curLocation = right[curLocation]; // otherwise go right

        steps++;
    }