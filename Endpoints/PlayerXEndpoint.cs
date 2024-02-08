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
            string[]? board = await repository.AddX(space);
            if (board == null)
                return Results.BadRequest("Can't put marker there!");

            bool haveWon = await repository.HaveWon(space);
            if (haveWon)
                return TypedResults.Ok("CONGRATULATIONS PLAYER! YOU HAVE WON!");

            return TypedResults.Ok(board);
        }
    }
}
