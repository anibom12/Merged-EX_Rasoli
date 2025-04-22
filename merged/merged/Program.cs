using System;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () => "welcome");
List<(int start, int end)> inputRange = new List<(int, int)>();

app.MapPost("/AddRange", (List<numbers> addRange) =>
{
   
    foreach (var range in addRange)
    {
        inputRange.Add((range.start, range.end));
    }
    return inputRange;
});


app.MapGet("/Result", () =>
{
    
    inputRange.Sort((a, b) => a.start.CompareTo(b.start));
    List<(int start, int end)> merged = new List<(int, int)>();

    foreach (var range in inputRange)
    {
        if (merged.Count == 0 || merged.Last().end < range.start)
        {
            merged.Add(range);
        }
        else
        {
            merged[merged.Count - 1] = (merged.Last().start, Math.Max(merged.Last().end, range.end));
        }
    }
    return merged.Select(r => $"[{r.start},{r.end}]");


});

app.Run();
public record numbers(int start, int end);
