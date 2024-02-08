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
            string[]? board = await repository.AddO(space);
            if (board == null)
                return Results.BadRequest("Can't put marker there!");

            bool haveWon = await repository.HaveWon(space);
            if (haveWon)
                return TypedResults.Ok("CONGRATULATIONS PLAYER! YOU HAVE WON!");

            return TypedResults.Ok(board);
        }
    }
}
