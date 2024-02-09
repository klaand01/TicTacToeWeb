using Microsoft.AspNetCore.Mvc;
using TicTacToeWeb.Repository;

namespace TicTacToeWeb.Endpoints
{
    public static class PlayerXEndpoint
    {
        public static void ConfigurePlayerXEndpoint(this WebApplication app)
        {
            var playerXGroup = app.MapGroup("board/playerx");
            playerXGroup.MapGet("/{space}", AddMarkerX);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> AddMarkerX(IRepository repository, int space)
        {
            //Checking the board
            if (await repository.BoardExists() == false)
                return Results.BadRequest("Must initialize the board first");

            //Checking turn
            if (await repository.PlayerTurn('X') == false)
                return Results.BadRequest("Not your turn");

            //Checking index
            string[]? board = await repository.AddMarker("X", space);
            if (board == null)
                return Results.BadRequest("Can't put marker there!");

            //Checking win condition
            bool haveWon = await repository.HaveWon(space);
            if (haveWon)
                return TypedResults.Ok("CONGRATULATIONS PLAYER! YOU HAVE WON!");

            return TypedResults.Ok(board);
        }
    }
}
