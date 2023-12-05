var data = File.ReadAllLines(@".\input.txt");

char DELIMITER = '|';
int answer = 0;

int pos;

bool isDigit(char c) { return c >= '0' && c <= '9'; }

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

foreach (string line in data)
{
    int count = 0;
    List<int> myNumbers = new();

    string myNumLine = line.Split(':')[1].Split(DELIMITER)[0].Trim();
    pos = 0;

    while (pos < myNumLine.Length)
        myNumbers.Add(GetNextNumber(myNumLine));

    string winNumLine = line.Split(':')[1].Split(DELIMITER)[1].Trim();
    pos = 0;

    while (pos < winNumLine.Length)
        if (myNumbers.Contains(GetNextNumber(winNumLine)))
            count++;

    if (count > 0)
        answer += (int)Math.Pow(2, count - 1);
}

Console.WriteLine(answer);