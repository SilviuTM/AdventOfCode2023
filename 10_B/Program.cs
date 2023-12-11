var data = File.ReadAllLines(@".\input.txt");

char[,] directions = new char[data.Length, data[0].Length];

// Get Start and initialize matrix with -1
(int row, int col) startPos = (-1, -1);
for (int i = 0; i < directions.GetLength(0); i++)
    for (int j = 0; j < directions.GetLength(1); j++)
    {
        if (data[i][j] == 'S')
            startPos = (i, j);

        directions[i, j] = '-';
    }

// compute directions and handNorm
bool leftHand = false; int leftTurn = 0, rightTurn = 0;
ComputeDirection();


// check hand norm
leftHand = leftTurn > rightTurn;
FollowRoad();


// return number of filled tiles
int answer = 0;
for (int i = 0; i < directions.GetLength(0); i++)
    for (int j = 0; j < directions.GetLength(1); j++)
        if (directions[i, j] == 'I')
            answer++;
Console.WriteLine(answer);


void FillFreeSpace((int row, int col) curPos, char symbol)
{
    if (curPos.row < 0 || curPos.col < 0 || curPos.row >= data.Length || curPos.col >= data[0].Length) return;

    if (directions[curPos.row, curPos.col] == '-')
    {
        directions[curPos.row, curPos.col] = symbol;
        FillFreeSpace((curPos.row + 1, curPos.col), symbol);
        FillFreeSpace((curPos.row, curPos.col + 1), symbol);
        FillFreeSpace((curPos.row - 1, curPos.col), symbol);
        FillFreeSpace((curPos.row, curPos.col - 1), symbol);
    }
}

void ComputeDirection()
{
    (int row, int col) current = startPos, prev = startPos; char prevSymbol = '-';
    do
    {
        char curr = data[current.row][current.col];

        // up
        char up = current.row - 1 >= 0 ? data[current.row - 1][current.col] : '\0';
        (int row, int col) upPos = (current.row - 1, current.col);

        // down
        char down = current.row + 1 < data.Length ? data[current.row + 1][current.col] : '\0';
        (int row, int col) downPos = (current.row + 1, current.col);

        // left
        char left = current.col - 1 >= 0 ? data[current.row][current.col - 1] : '\0';
        (int row, int col) leftPos = (current.row, current.col - 1);

        // right
        char right = current.col + 1 < data[0].Length ? data[current.row][current.col + 1] : '\0';
        (int row, int col) rightPos = (current.row, current.col + 1);


        if (curr == 'S') // then any connected pipe works
        {
            if ((up == '7' || up == 'F' || up == '|' || up == 'S') && prevSymbol != 'v') { ChangeHand(prevSymbol, '^'); prev = current; current = upPos; prevSymbol = '^'; }
            else if ((down == 'J' || down == 'L' || down == '|' || down == 'S') && prevSymbol != '^') { ChangeHand(prevSymbol, 'v'); prev = current; current = downPos; prevSymbol = 'v'; }
            else if ((left == 'L' || left == 'F' || left == '-' || left == 'S') && prevSymbol != '>') { ChangeHand(prevSymbol, '<'); prev = current; current = leftPos; prevSymbol = '<'; }
            else if ((right == 'J' || right == '7' || right == '-' || right == 'S') && prevSymbol != '<') { ChangeHand(prevSymbol, '>'); prev = current; current = rightPos; prevSymbol = '>'; }
        }

        else if (curr == '|') // then only up and down works
        {
            if ((up == '7' || up == 'F' || up == '|' || up == 'S') && prevSymbol != 'v') { ChangeHand(prevSymbol, '^'); prev = current; current = upPos; prevSymbol = '^'; }
            else if ((down == 'J' || down == 'L' || down == '|' || down == 'S') && prevSymbol != '^') { ChangeHand(prevSymbol, 'v'); prev = current; current = downPos; prevSymbol = 'v'; }
        }
        else if (curr == '-') // then only left and right works
        {
            if ((left == 'L' || left == 'F' || left == '-' || left == 'S') && prevSymbol != '>') { ChangeHand(prevSymbol, '<'); prev = current; current = leftPos; prevSymbol = '<'; }
            else if ((right == 'J' || right == '7' || right == '-' || right == 'S') && prevSymbol != '<') { ChangeHand(prevSymbol, '>'); prev = current; current = rightPos; prevSymbol = '>'; }
        }

        else if (curr == 'J') // then only up and left works
        {
            if ((up == '7' || up == 'F' || up == '|' || up == 'S') && prevSymbol != 'v') { ChangeHand(prevSymbol, '^'); prev = current; current = upPos; prevSymbol = '^'; }
            else if ((left == 'L' || left == 'F' || left == '-' || left == 'S') && prevSymbol != '>') { ChangeHand(prevSymbol, '<'); prev = current; current = leftPos; prevSymbol = '<'; }
        }
        else if (curr == '7') // then only down and left works
        {
            if ((down == 'J' || down == 'L' || down == '|' || down == 'S') && prevSymbol != '^') { ChangeHand(prevSymbol, 'v'); prev = current; current = downPos; prevSymbol = 'v'; }
            else if ((left == 'L' || left == 'F' || left == '-' || left == 'S') && prevSymbol != '>') { ChangeHand(prevSymbol, '<'); prev = current; current = leftPos; prevSymbol = '<'; }
        }

        else if (curr == 'L') // then only up and right works
        {
            if ((up == '7' || up == 'F' || up == '|' || up == 'S') && prevSymbol != 'v') { ChangeHand(prevSymbol, '^'); prev = current; current = upPos; prevSymbol = '^'; }
            else if ((right == 'J' || right == '7' || right == '-' || right == 'S') && prevSymbol != '<') { ChangeHand(prevSymbol, '>'); prev = current; current = rightPos; prevSymbol = '>'; }
        }
        else if (curr == 'F') // then only down and right works
        {
            if ((down == 'J' || down == 'L' || down == '|' || down == 'S') && prevSymbol != '^') { ChangeHand(prevSymbol, 'v'); prev = current; current = downPos; prevSymbol = 'v'; }
            else if ((right == 'J' || right == '7' || right == '-' || right == 'S') && prevSymbol != '<') { ChangeHand(prevSymbol, '>'); prev = current; current = rightPos; prevSymbol = '>'; }
        }

        directions[prev.row, prev.col] = prevSymbol;
    }
    while (current != startPos);
}

void ChangeHand(char thisSy, char nextSy)
{
    if (thisSy == '^')
    {
        if (nextSy == '<') leftTurn++;
        else if (nextSy == '>') rightTurn++;
    }
    else if (thisSy == '<')
    {
        if (nextSy == 'v') leftTurn++;
        else if (nextSy == '^') rightTurn++;
    }
    else if (thisSy == 'v')
    {
        if (nextSy == '>') leftTurn++;
        else if (nextSy == '<') rightTurn++;
    }
    else if (thisSy == '>')
    {
        if (nextSy == '^') leftTurn++;
        else if (nextSy == 'v') rightTurn++;
    }
}

void FollowRoad()
{
    (int row, int col) current = startPos;
    do
    {
        if (directions[current.row, current.col] == 'v')
            if (leftHand) { 
                FillFreeSpace((current.row, current.col + 1), 'I'); FillFreeSpace((current.row, current.col - 1), 'R');  // the one adjacent
                FillFreeSpace((current.row + 1, current.col + 1), 'I'); FillFreeSpace((current.row + 1, current.col - 1), 'R');  // the one in front of that
            }
            else { 
                FillFreeSpace((current.row, current.col - 1), 'I'); FillFreeSpace((current.row, current.col + 1), 'R');
                FillFreeSpace((current.row + 1, current.col - 1), 'I'); FillFreeSpace((current.row + 1, current.col + 1), 'R');
            }

        else if (directions[current.row, current.col] == '^')
            if (leftHand) { 
                FillFreeSpace((current.row, current.col - 1), 'I'); FillFreeSpace((current.row, current.col + 1), 'R');
                FillFreeSpace((current.row - 1, current.col - 1), 'I'); FillFreeSpace((current.row - 1, current.col + 1), 'R');
            }
            else { 
                FillFreeSpace((current.row, current.col + 1), 'I'); FillFreeSpace((current.row, current.col - 1), 'R');
                FillFreeSpace((current.row - 1, current.col + 1), 'I'); FillFreeSpace((current.row - 1, current.col - 1), 'R');
            }

        else if (directions[current.row, current.col] == '<')
            if (leftHand) { 
                FillFreeSpace((current.row + 1, current.col), 'I'); FillFreeSpace((current.row - 1, current.col), 'R'); 
                FillFreeSpace((current.row + 1, current.col - 1), 'I'); FillFreeSpace((current.row - 1, current.col - 1), 'R');
            }
            else { 
                FillFreeSpace((current.row - 1, current.col), 'I'); FillFreeSpace((current.row + 1, current.col), 'R'); 
                FillFreeSpace((current.row - 1, current.col - 1), 'I'); FillFreeSpace((current.row + 1, current.col - 1), 'R');
            }

        else if (directions[current.row, current.col] == '>')
            if (leftHand) { 
                FillFreeSpace((current.row - 1, current.col), 'I'); FillFreeSpace((current.row + 1, current.col), 'R'); 
                FillFreeSpace((current.row - 1, current.col + 1), 'I'); FillFreeSpace((current.row + 1, current.col + 1), 'R');
            }
            else { 
                FillFreeSpace((current.row + 1, current.col), 'I'); FillFreeSpace((current.row - 1, current.col), 'R'); 
                FillFreeSpace((current.row + 1, current.col + 1), 'I'); FillFreeSpace((current.row - 1, current.col + 1), 'R');
            }


        if (directions[current.row, current.col] == 'v') current = (current.row + 1, current.col);
        else if (directions[current.row, current.col] == '^') current = (current.row - 1, current.col);
        else if (directions[current.row, current.col] == '>') current = (current.row, current.col + 1);
        else if (directions[current.row, current.col] == '<') current = (current.row, current.col - 1);
    }
    while (current != startPos);
}