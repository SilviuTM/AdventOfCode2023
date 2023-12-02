var data = File.ReadAllLines(@".\input.txt");
int result = 0;
int currentValue, firstDigit, lastDigit;
string[] possibleDigitsS = new string[] { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
char[] possibleDigitsN = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

foreach (string line in data)
{
    firstDigit = lastDigit = -1;

    for (int i = 0; i < line.Length; i++)
    {
        int aux = checkDigits(i, line);
        if (aux != -1) // if something was found
        {
            if (firstDigit == -1) firstDigit = aux; // if firstDigit was not found yet (-1), then set it
            lastDigit = aux; // lastDigit has to be constantly changing, until the input line expires, so we don't need to check anything
        }
    }

    // add current number and proceed
    currentValue = firstDigit * 10 + lastDigit;
    result += currentValue;
}

Console.WriteLine(result);

int checkDigits(int curIndex, string curLine)
{
    for (int i = 1; i <= 9; i++)
    {
        // if current digit string doesn't go out of line length bound
        if (curIndex + possibleDigitsS[i].Length <= curLine.Length)
            // then check current position's substring to be equal to the current digit string
            if (curLine.Substring(curIndex, possibleDigitsS[i].Length).Equals(possibleDigitsS[i]))
                return i;

        // check if current character is equal to currently checked digit (as numeral)
        if (curLine[curIndex].Equals(possibleDigitsN[i]))
            return i;
    }

    // if we get here, then nothing was found, so return -1
    return -1;
}