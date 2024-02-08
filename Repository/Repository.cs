using Microsoft.EntityFrameworkCore;
using TicTacToeWeb.Data;
using TicTacToeWeb.Models;

namespace TicTacToeWeb.Repository
{
    public class Repository : IRepository
    {
        private DataContext dataContext;
        private int spot = 0;
        private int index = 0;
        bool haveWon = false;
        char mark = ' ';

        public Repository(DataContext db)
        {
            dataContext = db;
        }

        public async Task<string[]> CreateBoard()
        {
            Board board = new Board();
            await dataContext.AddAsync(board);
            await dataContext.SaveChangesAsync();
            return board.Spaces;
        }

        public async Task<Board?> GetBoard()
        {
            return await dataContext.Board.FirstAsync();
        }

        public async Task<string[]?> AddO(int space)
        {
            Board board = await dataContext.Board.FirstAsync();
            if (board == null)
                return null;

            CheckBoard(space);

            if (spot == 0)
                return null;

            if (board.Spaces[index][spot] == 'O' || board.Spaces[index][spot] == 'X')
                return null;

            board.Spaces[index] = board.Spaces[index].Insert(spot, "O");
            board.Spaces[index] = board.Spaces[index].Remove(spot + 1, 1);
            mark = 'O';

            await dataContext.SaveChangesAsync();
            return board.Spaces;
        }

        public async Task<string[]?> AddX(int space)
        {
            Board board = await dataContext.Board.FirstAsync();
            if (board == null)
                return null;

            CheckBoard(space);

            if (spot == 0)
                return null;

            if (board.Spaces[index][spot] == 'O' || board.Spaces[index][spot] == 'X')
                return null;

            board.Spaces[index] = board.Spaces[index].Insert(spot, "X");
            board.Spaces[index] = board.Spaces[index].Remove(spot + 1, 1);
            mark = 'X';

            await dataContext.SaveChangesAsync();
            return board.Spaces;
        }

        private async void CheckBoard(int space)
        {
            switch (space)
            {
                case 1:
                    spot = 1;
                    index = 0;
                    break;

                case 2:
                    spot = 5;
                    index = 0;
                    break;

                case 3:
                    spot = 9;
                    index = 0;
                    break;

                case 4:
                    spot = 1;
                    index = 1;
                    break;

                case 5:
                    spot = 5;
                    index = 1;
                    break;

                case 6:
                    spot = 9;
                    index = 1;
                    break;
                
                case 7:
                    spot = 1;
                    index = 2;
                    break;

                case 8:
                    spot = 5;
                    index = 2;
                    break;

                case 9:
                    spot = 9;
                    index = 2;
                    break;
            }
        }

        public async Task<bool> HaveWon(int space)
        {
            Board board = await dataContext.Board.FirstAsync();

            //Checking the rows
            if (spot == 1)
            {
                if (board.Spaces[index][5] == mark && board.Spaces[index][9] == mark)
                    return true;
            }
            else if (spot == 5)
            {
                if (board.Spaces[index][1] == mark && board.Spaces[index][9] == mark)
                    return true;
            }
            else if (spot == 9)
            {
                if (board.Spaces[index][1] == mark && board.Spaces[index][5] == mark)
                    return true;
            }

            //Checking the columns
            if (index == 0)
            {
                if (board.Spaces[1][spot] == mark && board.Spaces[2][spot] == mark)
                    return true;
            }
            else if (index == 1)
            {
                if (board.Spaces[0][spot] == mark && board.Spaces[2][spot] == mark)
                    return true;
            }
            else if (index == 2)
            {
                if (board.Spaces[0][spot] == mark && board.Spaces[1][spot] == mark)
                    return true;
            }
            return false;
        }
    }
}