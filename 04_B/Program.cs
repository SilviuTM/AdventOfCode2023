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


int[] cards = new int[200];
for (int i = 1; i < 200; i++) cards[i] = 1;

int cardID = 0;
foreach (string line in data)
{
    cardID++;
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

    // now get copies based on count
    for (int k = 0; k < count && cardID + k + 1 < 200; k++)
        cards[cardID + k + 1] += cards[cardID];
}

for (int i = 1; i < 200; i++) answer += cards[i];
Console.WriteLine(answer);