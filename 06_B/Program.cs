var data = File.ReadAllLines(@".\input.txt");

string aux = "";
long time, dist;
bool isDigit(char c) { return c >= '0' && c <= '9'; }

// input read
foreach (char c in data[0])
    if (isDigit(c)) aux += c;
time = long.Parse(aux);

aux = "";
foreach (char c in data[1])
    if (isDigit(c)) aux += c;
dist = long.Parse(aux);

Console.WriteLine("Time: " + time);
Console.WriteLine("Dist: " + dist);

// problem solver
long answer = 0;
for (long myTime = 0; myTime < time; myTime++)
    if (myTime * (time - myTime) > dist) // when i find the first good time, i calculate the other end as well (time - myTime)
                                         // then add the amount of numbers between these two (a - b + 1)
    {
        answer = (time - myTime) - myTime + 1;
        break;
    }

Console.WriteLine(answer);