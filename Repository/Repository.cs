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
        Board board;

        public Repository(DataContext db)
        {
            dataContext = db;
        }

        public async Task<string[]> CreateBoard()
        {
            //If the board is already initialized
            if (board != null)
                return board.Spaces;

            board = new Board();
            await dataContext.AddAsync(board);
            await dataContext.SaveChangesAsync();
            return board.Spaces;
        }

        public async Task<string[]> CleanBoard()
        {
            Board newBoard = new Board();
            await dataContext.AddAsync(newBoard);
            dataContext.Board.Remove(await dataContext.Board.FirstAsync());
            await dataContext.SaveChangesAsync();
            return newBoard.Spaces;
        }

        public async Task<Board?> GetBoard()
        {
            return await dataContext.Board.FirstAsync();
        }

        public async Task<bool> BoardExists()
        {
            return await dataContext.Board.CountAsync() > 0;
        }

        public async Task<bool> PlayerTurn(char marker)
        {
            board = await dataContext.Board.FirstAsync();
            mark = marker;
            int nrOs = 0;
            int nrXs = 0;
            bool myTurn = true;

            //Checking how many Os and Xs there are
            for (int i = 0; i < 3; i++)
            {
                for (int j = 1; j < 10; j += 4)
                {
                    if (board.Spaces[i][j] == 'O')
                        nrOs++;
                    else if (board.Spaces[i][j] == 'X')
                        nrXs++;
                }
            }

            //Checking if the same player is trying to add a marker
            if (nrOs > nrXs && marker == 'O')
                myTurn = false;
            else if (nrXs > nrOs && marker == 'X')
                myTurn = false;
            
            return myTurn;
        }

        public async Task<string[]?> AddMarker(string marker, int space)
        {
            CheckBoard(space);

            if (spot == 0)
                return null;

            if (board.Spaces[index][spot] == 'O' || board.Spaces[index][spot] == 'X')
                return null;

            board.Spaces[index] = board.Spaces[index].Insert(spot, marker);
            board.Spaces[index] = board.Spaces[index].Remove(spot + 1, 1);
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