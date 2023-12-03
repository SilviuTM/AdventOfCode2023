var data = File.ReadAllLines(@".\input.txt");
char[,] matrix = new char[data.Length, data[0].Length];
char EMPTY = '.';
char GEAR = '*';
int answer = 0;

bool isDigit(char c) { return c >= '0' && c <= '9'; }
int acquireNumber(int row, int col)
{
    int res = 0;

    // first, we go to the very left of the number
    while (col > 0 && isDigit(matrix[row, col])) col--;

    // edge case, if our number was not on the edge, we need to go back one step to be back on our number
    if (!isDigit(matrix[row, col])) col++;

    // now, we compute the number (moving to the right) while erasing it from the matrix
    while (col < data[row].Length && isDigit(matrix[row, col]))
    {
        res = res * 10 + (matrix[row, col] - '0');
        matrix[row, col] = EMPTY;
        col++;
    }

    return res;
}


// first we copy the input to a char string for easier manipulation
for (int i = 0; i < data.Length; i++)
    for (int j = 0; j < data[i].Length; j++)
        matrix[i, j] = data[i][j];

// now we go through each character (which is not on any edge), and if it's a GEAR, we count how many numbers it neighbours
// if that count is 2, then we add its gear power to the answer
for (int i = 1; i < data.Length - 1; i++)
    for (int j = 1; j < data[i].Length - 1; j++)
    {
        if (matrix[i, j] == GEAR)
        {
            int count = 0, power = 1;

            if (isDigit(matrix[i - 1, j])) { power *= acquireNumber(i - 1, j); count++; } // up-middle
            if (isDigit(matrix[i - 1, j - 1])) { power *= acquireNumber(i - 1, j - 1); count++; } // up-left
            if (isDigit(matrix[i - 1, j + 1])) { power *= acquireNumber(i - 1, j + 1); count++; } // up-right

            if (isDigit(matrix[i, j - 1])) { power *= acquireNumber(i, j - 1); count++; } // left
            if (isDigit(matrix[i, j + 1])) { power *= acquireNumber(i, j + 1); count++; } // right

            if (isDigit(matrix[i + 1, j])) { power *= acquireNumber(i + 1, j); count++; } // down-middle
            if (isDigit(matrix[i + 1, j - 1])) { power *= acquireNumber(i + 1, j - 1); count++; } // down-left
            if (isDigit(matrix[i + 1, j + 1])) { power *= acquireNumber(i + 1, j + 1); count++; } // down-right

            if (count == 2) answer += power;
        }
    }

Console.WriteLine(answer);