using GerenciandoPessoas.Context;
using GerenciandoPessoas.Dto;
using GerenciandoPessoas.Entities;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["ConnectionStrings:Default"]);

var app = builder.Build();
var configuration = app.Configuration;

app.MapGet("/users", (ApplicationDbContext context) =>
{
    var pessoas = context.Pessoas
        .ToList();

    return Results.Ok(pessoas);
});

app.MapPost("/users", (UserRequestDto userRequestDto, ApplicationDbContext context) =>
{
    var pessoa = new Pessoa
    {
        Name = userRequestDto.Name,
        Email = userRequestDto.Email,
        Cpf = userRequestDto.Cpf,
        Age = userRequestDto.Age
    };

    context.Pessoas.Add(pessoa);
    context.SaveChanges();

    return Results.Created($"/users/{pessoa.Id}", pessoa);
});

app.MapPut("/users/{id}", ([FromRoute] int id, UserRequestDto userRequestDto, ApplicationDbContext context) =>
{
    var pessoa = context.Pessoas
        .Where(p => p.Id == id)
        .FirstOrDefault();

    if (pessoa == null) {
        return Results.NotFound(new { Message = "Pessoa não encontrada." });
    }

    pessoa.Name = userRequestDto.Name;
    pessoa.Email = userRequestDto.Email;
    pessoa.Cpf = userRequestDto.Cpf;
    pessoa.Age = userRequestDto.Age;

    context.SaveChanges();

    return Results.Ok(pessoa);
});

app.MapDelete("/users/{id}", ([FromRoute] int id, ApplicationDbContext context) =>
{
    var pessoa = context.Pessoas
        .Where(p => p.Id == id)
        .FirstOrDefault();

    if (pessoa == null)
    {
        return Results.NotFound(new { Message = "Pessoa não encontrada." });
    }

    context.Pessoas.Remove(pessoa);
    context.SaveChanges();

    return Results.Ok();
});

app.Run();
