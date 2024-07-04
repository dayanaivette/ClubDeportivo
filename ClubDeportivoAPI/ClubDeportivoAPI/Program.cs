using ClubDeportivoAPI.Models;
using ClubDeportivoAPI.Services;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ClubDeportivoDbSettings>(builder.Configuration.GetSection("ClubDeportivoDbSettings"));
builder.Services.AddSingleton<RegistrosClubServices>();

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

app.MapGet("/registros", async (RegistrosClubServices clubService) => {
    var registro = await clubService.GetAsync();
    return registro;
});
app.MapGet("/registros/{id}", async (RegistrosClubServices clubService, string id) => {
    var registro = await clubService.GetAsync(id);
    return registro is null ? Results.NotFound() : Results.Ok(registro);
});
app.MapPost("/registros", async (RegistrosClubServices clubService, ClubDeportivo registro) => {
    await clubService.CreateAsync(registro);
    return registro;
});
app.MapPut("/registros/{id}", async (RegistrosClubServices clubService, string id, ClubDeportivo registro) => {
    await clubService.UpdateAsync(id, registro);
    return registro;
});
app.MapDelete("/registros/{id}", async (RegistrosClubServices clubService, string id) => {
    await clubService.RemoveAsync(id);
    return Results.Ok();
});

app.Run();
