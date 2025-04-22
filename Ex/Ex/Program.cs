var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Welcome");

Dictionary<string, List<string>> user = new Dictionary<string, List<string>>();

app.MapPost("/SaveEx",(List<UserData>users) =>
{
    if (users == null || users.Count == 0)

            return Results.BadRequest("User List cannot be Empty");
    foreach(var userData in users)
    {
        if (!user.ContainsKey(userData.name))
            user[userData.name] = new List<string>();


        if (!user[userData.name].Contains(userData.ex))
            user[userData.name].Add(userData.ex);
    }
    var userSave = users.Select(u => $"{u.name}: {u.ex}");
    return Results.Ok($"User saved successfully {string.Join("/", userSave)}");
});


app.MapGet("/EX/{name}", (string name) =>
{
    if (user.ContainsKey(name))

        return Results.Ok($"EX's name for {name} is:{string.Join(" ",user[name])}");
    return Results.NotFound($"There is not Ex for {name}");
});

app.MapPut("/UpdateEx/{name}/{oldEX}/{newEX}", (string name ,string oldEX,string newEX) =>
{
    if (!user.ContainsKey(name))
        return Results.NotFound($"user {name} not found");

    var exList = user[name];
    var index = exList.IndexOf(oldEX);

    if (index == -1)
        return Results.NotFound($"There is not EX with name : {oldEX} for {name}");


    exList[index] = newEX;
    return Results.Ok($"Update new EX with name{newEX} for {name}");
});
app.MapDelete("/deleteEX/{name}/{ex}", (string name,string ex) =>
{
    
    if (!user.ContainsKey(name))
        return Results.NotFound($"User {name} Not Found");
    else if (!user[name].Contains(ex))
    {
        return Results.NotFound($"There is not ex {ex} for name {name}");
    }
    user[name].Remove(ex);
    return Results.Ok("Removal was successful");
});

app.Run();
public record UserData(string name, string ex);

