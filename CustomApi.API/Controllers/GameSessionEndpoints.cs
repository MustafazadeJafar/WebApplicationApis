using CustomApi.Core.Entities;
namespace CustomApi.API.Controllers;

public static class GameSessionEndpoints
{
    public static void MapGameSessionEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/GameSession", () =>
        {
            return new [] { new GameSession() };
        })
        .WithName("GetAllGameSessions")
        .Produces<GameSession[]>(StatusCodes.Status200OK);

        routes.MapGet("/api/GameSession/{id}", (int id) =>
        {
            //return new GameSession { ID = id };
        })
        .WithName("GetGameSessionById")
        .Produces<GameSession>(StatusCodes.Status200OK);

        routes.MapPut("/api/GameSession/{id}", (int id, GameSession input) =>
        {
            return Results.NoContent();
        })
        .WithName("UpdateGameSession")
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/GameSession/", (GameSession model) =>
        {
            //return Results.Created($"/api/GameSessions/{model.ID}", model);
        })
        .WithName("CreateGameSession")
        .Produces<GameSession>(StatusCodes.Status201Created);

        routes.MapDelete("/api/GameSession/{id}", (int id) =>
        {
            //return Results.Ok(new GameSession { ID = id });
        })
        .WithName("DeleteGameSession")
        .Produces<GameSession>(StatusCodes.Status200OK);
    }
}
