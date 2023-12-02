var data = File.ReadAllLines(@".\input.txt");
int cursor;
int answer = 0;

foreach (string line in data)
{
    cursor = 0;
    int lineRed = 0, lineGreen = 0, lineBlue = 0;
    int id = GetNextNumber(line);

    // check all colors and store the max for each
    while (cursor < line.Length)
    {
        int curCount = GetNextNumber(line);
        string curColor = GetNextWord(line);

        if (curColor.Equals("red") && curCount > lineRed) lineRed = curCount;
        if (curColor.Equals("green") && curCount > lineGreen) lineGreen = curCount;
        if (curColor.Equals("blue") && curCount > lineBlue) lineBlue = curCount;
    }

    // add the "power value" (multiplication)
    answer += (lineBlue * lineGreen * lineRed);
}

// show result
Console.WriteLine(answer);

int GetNextNumber(string line)
{
    int number = 0;
    // skip until you find a number
    while (line[cursor] < '0' || line[cursor] > '9') cursor++;

    // get digits until not digits
    while (line[cursor] >= '0' && line[cursor] <= '9') { number = number * 10 + (line[cursor] - '0'); cursor++; }

    // return result
    return number;
}

string GetNextWord(string line)
{
    string word = "";
    // skip until you find a letter
    while (line[cursor] < 'a' || line[cursor] > 'z') cursor++;

    // get letters until not letters
    while (line[cursor] >= 'a' && line[cursor] <= 'z')
    {
        word += line[cursor];
        cursor++;

        if (cursor == line.Length) break;
    }

    // return result
    return word;
}