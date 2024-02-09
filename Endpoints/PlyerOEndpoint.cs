using Microsoft.AspNetCore.Mvc;
using TicTacToeWeb.Repository;

namespace TicTacToeWeb.Endpoints
{
    public static class PlyerOEndpoint
    {
        public static void ConfigurePlayerOEndpoint(this WebApplication app)
        {
            var playerOGroup = app.MapGroup("board/playero");
            playerOGroup.MapGet("/{space}", AddMarkerO);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> AddMarkerO(IRepository repository, int space)
        {
            //Checking the board
            if (await repository.BoardExists() == false)
                return Results.BadRequest("Must initialize the board first");

            //Checking turn
            if (await repository.PlayerTurn('O') == false)
                return Results.BadRequest("Not your turn");

            //Checking index
            string[]? board = await repository.AddMarker("O", space);
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
