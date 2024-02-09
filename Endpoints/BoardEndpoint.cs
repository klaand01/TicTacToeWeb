using Microsoft.AspNetCore.Mvc;
using TicTacToeWeb.Models;
using TicTacToeWeb.Repository;

namespace TicTacToeWeb.Endpoints
{
    public static class GameBoardEndpoint
    {
        public static void ConfigureGameEndpoint(this WebApplication app)
        {
            var boardGroup = app.MapGroup("board");
            boardGroup.MapGet("/newboard", CreateBoard);
            boardGroup.MapGet("/", GetBoard);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> CreateBoard(IRepository repository)
        {
            return TypedResults.Ok(repository.CreateBoard());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetBoard(IRepository repository)
        {
            //Checking the board
            if (await repository.BoardExists() == false)
                return Results.BadRequest("Must initialize the board first");

            Board? board = await repository.GetBoard();
            if (board == null)
                return Results.BadRequest("There is no board");

            return TypedResults.Ok(board);
        }
    }
}