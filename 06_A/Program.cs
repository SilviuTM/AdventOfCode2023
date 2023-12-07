var data = File.ReadAllLines(@".\input.txt");

List<int> times = new();
List<int> dists = new();

int pos = 0;
int GetNextNumber(string text)
{
    while (pos < text.Length && !isDigit(text[pos])) pos++;
    if (pos == text.Length) return -1;

    int nr = 0;
    while (pos < text.Length && isDigit(text[pos]))
    {
        nr = nr * 10 + (text[pos] - '0');
        pos++;
    }

    return nr;
}

bool isDigit(char c) { return c >= '0' && c <= '9'; }

// input read
int nr = GetNextNumber(data[0]);
while (nr != -1) { times.Add(nr); nr = GetNextNumber(data[0]); }

pos = 0;
nr = GetNextNumber(data[1]);
while (nr != -1) { dists.Add(nr); nr = GetNextNumber(data[1]); }

// problem solver
int answer = 1;
for (int i = 0; i < times.Count; i++)
{
    int victories = 0;
    for (int time = 1; time < times[i]; time++)
        if (time * (times[i] - time) > dists[i])
            victories++;

    answer *= victories;
}

Console.WriteLine(answer);