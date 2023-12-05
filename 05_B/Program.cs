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
    // if empty line then let's take a moment to simplify the ranges from previous conversion type before moving on to the next
    if (data[i].Length == 0)
    {
        almanacOfRanges[^1].Sort((x, y) => x.source < y.source ? -1 : 1); // sort so we can see if this and next in order are neighbours
        List<(long destination, long source, long range)> appendListOfRanges = new(); // this is used if they're not, it adds the missing range between them
        for (int t = 0; t < almanacOfRanges[^1].Count - 1; t++)
        {
            var cur = almanacOfRanges[^1][t];
            var next = almanacOfRanges[^1][t + 1];

            if (cur.source + cur.range < next.source) // if missing area, add
                appendListOfRanges.Add((cur.source + cur.range, cur.source + cur.range, next.source - (cur.source + cur.range))); // first 2 values are equal because 1:1 mapping
        }
        almanacOfRanges[^1].AddRange(appendListOfRanges); // add missing area ranges
        almanacOfRanges[^1].Sort((x, y) => x.source < y.source ? -1 : 1); // sort again

        i++;
    }

    // otherwise if new conversion type, add its list to almanac
    else if (data[i].Contains(':'))
    {
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

var aux = data[0].Split(' ').ToList();
aux.RemoveAt(0);
var seeds = aux.Select(x => long.Parse(x)).ToList();

long seedLeeway;
//check each seed pair
for (int seedK = 0; seedK < seeds.Count; seedK += 2)
{
    // okay, so we don't want to check 3 trillion seeds. we know that we work with ranges, and seeds are one after another.
    // this effectively means that a lot of consecutive seeds will go through the exactly same conversion ranges as the previous seed
    // do we need to check those in that case?
    // of course not, so we can compute this leeway value which tells us how many of the following seeds will follow the same conversion range
    // this means we can skip those seeds and move on to those that will follow a different set of conversion ranges
    // we repeat this process to heavily cut down on computation time
    seedLeeway = long.MaxValue;

    // check each seed in the pair range
    for (long seedOffset = 0; seedOffset < seeds[seedK + 1]; seedOffset++)
    {
        long currentValue = seeds[seedK] + seedOffset; // this is current seed

        // through each conversion
        for (int conversion = 0; conversion < almanacOfRanges.Count; conversion++)
            currentValue = ConvertValueThroughConversion(conversion, currentValue);

        if (currentValue < minimum) minimum = currentValue;

        seedOffset += seedLeeway;
        seedLeeway = long.MaxValue;
    }
}

Console.WriteLine(minimum);

long ConvertValueThroughConversion(int conversion, long value)
{
    // check each range in current conversion
    foreach (var range in almanacOfRanges[conversion])
        if (range.source <= value && value < range.source + range.range) // if correct range, compute
        {
            // computes leeway
            if (range.source + range.range - value - 1 < seedLeeway)
                seedLeeway = range.source + range.range - value - 1;

            // returns destination + offset
            return range.destination + (value - range.source);
        }

    // if it wasn't found in any range, then it's above everything else, which means its range is (maxsource, infinity), since this is endless, leeway stays the same
    return value;
}