var data = File.ReadAllLines(@".\input.txt");

int[,] matrix = new int[data.Length, data[0].Length];
int id = 0;

// Get Start and initialize matrix with -1
(int row, int col) startPos = (-1, -1);
for (int i = 0; i < matrix.GetLength(0); i++)
    for (int j = 0; j < matrix.GetLength(1); j++)
    {
        if (data[i][j] == 'S')
            startPos = (i, j);

        matrix[i, j] = -1;
    }

List<(int row, int col)> currentQueue = getConnected(startPos);
while (currentQueue.Count > 0)
{
    id++;

    // check current to-be-scanned queue
    currentQueue.Sort();
    for (int i = 0; i < currentQueue.Count - 1; i++) // if the same pipe appears twice, that's a loop
        if (currentQueue[i] == currentQueue[i+1])
        {
            Console.WriteLine(id);
            return;
        }

    // else, we scan the current queue to compute the next queue
    List<(int row, int col)> newQueue = new();
    foreach (var pos in currentQueue)
        if (matrix[pos.row, pos.col] == -1)
            newQueue.AddRange(getConnected(pos));

    currentQueue = newQueue;
}


List<(int row, int col)> getConnected((int row, int col) current)
{
    matrix[current.row, current.col] = id;
    List<(int row, int col)> queue = new();

    char curr = data[current.row][current.col];

    char up = current.row - 1 >= 0 ? data[current.row - 1][current.col] : '\0';
    char down = current.row + 1 < data.Length ? data[current.row + 1][current.col] : '\0';

    char left = current.col - 1 >= 0 ? data[current.row][current.col - 1] : '\0';
    char right = current.col + 1 < data[0].Length ? data[current.row][current.col + 1] : '\0';

    if (curr == 'S') // then any connected pipe works
    {
        if (up == '7' || up == 'F' || up == '|') queue.Add((current.row - 1, current.col));
        if (down == 'J' || down == 'L' || down == '|') queue.Add((current.row + 1, current.col));
        if (left == 'L' || left == 'F' || left == '-') queue.Add((current.row, current.col - 1));
        if (right == 'J' || right == '7' || right == '-') queue.Add((current.row, current.col + 1));
    }

    else if (curr == '|') // then only up and down works
    {
        if (up == '7' || up == 'F' || up == '|') queue.Add((current.row - 1, current.col));
        if (down == 'J' || down == 'L' || down == '|') queue.Add((current.row + 1, current.col));
    }
    else if (curr == '-') // then only left and right works
    {
        if (left == 'L' || left == 'F' || left == '-') queue.Add((current.row, current.col - 1));
        if (right == 'J' || right == '7' || right == '-') queue.Add((current.row, current.col + 1));
    }

    else if (curr == 'J') // then only up and left works
    {
        if (up == '7' || up == 'F' || up == '|') queue.Add((current.row - 1, current.col));
        if (left == 'L' || left == 'F' || left == '-') queue.Add((current.row, current.col - 1));
    }
    else if (curr == '7') // then only down and left works
    {
        if (down == 'J' || down == 'L' || down == '|') queue.Add((current.row + 1, current.col));
        if (left == 'L' || left == 'F' || left == '-') queue.Add((current.row, current.col - 1));
    }

    else if (curr == 'L') // then only up and right works
    {
        if (up == '7' || up == 'F' || up == '|') queue.Add((current.row - 1, current.col));
        if (right == 'J' || right == '7' || right == '-') queue.Add((current.row, current.col + 1));
    }
    else if (curr == 'F') // then only down and right works
    {
        if (down == 'J' || down == 'L' || down == '|') queue.Add((current.row + 1, current.col));
        if (right == 'J' || right == '7' || right == '-') queue.Add((current.row, current.col + 1));
    }

    return queue;
}