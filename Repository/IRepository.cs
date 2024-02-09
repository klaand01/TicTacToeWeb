using TicTacToeWeb.Models;

namespace TicTacToeWeb.Repository
{
    public interface IRepository
    {
        Task<string[]> CreateBoard();
        Task<Board?> GetBoard();
        Task<bool> BoardExists();
        Task<bool> HaveWon(int space);
        Task<bool> PlayerTurn(char marker);
        Task<string[]?> AddMarker(string marker, int space);
    }
}