var data = File.ReadAllLines(@".\input.txt");

bool[] isEmptyRow = new bool[data.Length];
bool[] isEmptyCol = new bool[data[0].Length];

// compute empty rows
for (int i = 0; i < data.Length; i++)
{
    bool expansion = true;
    for (int j = 0; j < data[i].Length; j++)
        if (data[i][j] == '#')
        {
            expansion = false;
            break;
        }

    isEmptyRow[i] = expansion;
}

// compute empty columns
for (int j = 0; j < data[0].Length; j++)
{
    bool expansion = true;
    for (int i = 0; i < data.Length; i++)
        if (data[i][j] == '#')
        {
            expansion = false;
            break;
        }

    isEmptyCol[j] = expansion;
}

// get galaxies
List<(int row, int col)> galaxies = new();
for (int i = 0; i < data.Length; i++)
    for (int j = 0; j < data[0].Length; j++)
        if (data[i][j] == '#')
            galaxies.Add((i, j));


// compute distance
long answer = 0;
for (int i = 0; i < galaxies.Count; i++)
    for (int j = i + 1; j < galaxies.Count; j++)
    {
        long distance = GetDistance(galaxies[i], galaxies[j]);
        answer += distance;
    }

Console.WriteLine(answer);

long GetDistance((int row, int col) g1, (int row, int col) g2)
{
    int row1 = Math.Min(g1.row, g2.row);
    int row2 = Math.Max(g1.row, g2.row);

    int col1 = Math.Min(g1.col, g2.col);
    int col2 = Math.Max(g1.col, g2.col);

    long d = 0;
    for (int i = row1; i < row2; i++)
        d += 1 + (isEmptyRow[i] ? 999999 : 0);

    for (int i = col1; i < col2; i++)
        d += 1 + (isEmptyCol[i] ? 999999 : 0);

    return d;
}