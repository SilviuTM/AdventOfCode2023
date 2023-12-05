var data = File.ReadAllLines(@".\input.txt");

List<List<(long destination, long source, long range)>> almanacOfRanges = new();    // 1. Seed to Soil
                                                                                    // 2. Soil to Fertilizer
                                                                                    // 3. Fertilizer to Water
                                                                                    // 4. Water to Light
                                                                                    // 5. Light to Temperature
                                                                                    // 6. Temperature to Humidity
                                                                                    // 7. Humidity to Location
int i = 2;
// compute almanac
while (i < data.Length)
{
    // if empty line ignore
    if (data[i].Length == 0) i++;

    // otherwise if new conversion type, add its list to almanac
    else if (data[i].Contains(':')) {
        almanacOfRanges.Add(new());
        i++;
    }

    // otherwise, add new range to current conversion type
    else
    {
        string[] numbers = data[i].Split(' ');
        almanacOfRanges[^1].Add((long.Parse(numbers[0]), long.Parse(numbers[1]), long.Parse(numbers[2])));
        i++;
    }
}

long minimum = long.MaxValue;

var seeds = data[0].Split(' ').ToList();
seeds.RemoveAt(0);

//check each seed
foreach (var seed in seeds)
{
    long currentValue = long.Parse(seed);

    // through each conversion
    for (int conversion = 0; conversion < almanacOfRanges.Count; conversion++)
        currentValue = ConvertValueThroughConversion(conversion, currentValue);

    if (currentValue < minimum) minimum = currentValue;
}

Console.WriteLine(minimum);

long ConvertValueThroughConversion(int conversion, long value)
{
    // check each range in current conversion
    foreach (var range in almanacOfRanges[conversion])
        if (range.source <= value && value < range.source + range.range) // if correct range, compute
            // returns destination + offset
            return range.destination + (value - range.source);

    // if it wasn't found in any range, then value remains identical
    return value;
}