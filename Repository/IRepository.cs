using TicTacToeWeb.Models;

namespace TicTacToeWeb.Repository
{
    public interface IRepository
    {
        Task<string[]> CreateBoard();
        Task<Board?> GetBoard();
        Task<bool> HaveWon(int space);

        Task<string[]?> AddO(int space);
        Task<string[]?> AddX(int space);
    }
}