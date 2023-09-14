
// default settings

using AAPI.Data;
using AAPI.Data.Providers;
using Microsoft.AspNetCore.Http.HttpResults;

var AnimeProviders = new IAnimeProvider[] 
{
    new AnilibriaProvider(0, "AniLibria","AAPI.Resources.Anilibria.png"),
    new AnimevostProvider(1, "Animevost", "AAPI.Resources.Animevost.png")
};

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/providers", () =>
{
    return Results.Ok(AnimeProviders);
});


app.MapGet("/providers/{providerId}/icon", async (int providerId) =>
{
    var provider = AnimeProviders.FirstOrDefault(s => s.ProviderId == providerId);
    if(provider == null)
        return Results.NotFound();
    try
    {
        var result = await provider.GetProviderIcon();
       
        return Results.File(result, "image/png",$"{provider.ProviderName}Icon");
    }
    catch (Exception e)
    {
        // TODO Logging errors
        Console.WriteLine(e);
        return Results.Problem("ERROR: " + e.Message);
    }
});

app.MapGet("/providers/{providerId}/updates", async (int providerId) =>
{
    var provider = AnimeProviders.FirstOrDefault(s => s.ProviderId == providerId);
    if(provider == null)
        return Results.NotFound();
    try
    {
        var result = await provider.GetLastUpdate().ConfigureAwait(true);
        
        return Results.Ok(result);
    }
    catch (Exception e)
    {
        // TODO Logging errors
        Console.WriteLine(e);
        return Results.Problem("ERROR: " + e.Message);
    }
});

app.Run();