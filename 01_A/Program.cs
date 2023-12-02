var data = File.ReadAllLines(@".\input.txt");
int result = 0;
int currentValue;

foreach (string line in data)
{
    currentValue = 0;

    // check left to right, stop after finding digit
    for (int i = 0; i < line.Length; i++)
        if (line[i] >= '0' && line[i] <= '9')
        {
            currentValue = line[i] - '0';
            break;
        }

    // check right to left, stop after finding digit (and add to the right of the number)
    for (int i = line.Length - 1; i >= 0; i--)
        if (line[i] >= '0' && line[i] <= '9')
        {
            currentValue = currentValue * 10 + (line[i] - '0');
            break;
        }

    // add current number and proceed
    result += currentValue;
}

Console.WriteLine(result);