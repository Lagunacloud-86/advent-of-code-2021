using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04
{
    public class BingoBoard
    {
        public Int32[,] Board { get; private set; }
        public Boolean[,] Marks { get; private set; }

        public BingoBoard()
        {
            Board = new Int32[5, 5];
            Marks = new bool[5, 5];

        }

        public void Mark(Int32 value)
        {
            for (Int32 column = 0; column < 5; ++column)
                for (Int32 row = 0; row < 5; ++row)
                {
                    if (Board[column, row] == value)
                        Marks[column, row] = true;
                }
        }

        public void ResetMarks()
        {
            for (Int32 column = 0; column < 5; ++column)
                for (Int32 row = 0; row < 5; ++row)
                    Marks[column, row] = false;
        }


        public void SetBoard(Int32 row, Int32 column, Int32 value)
        {
            Board[column, row] = value;
        }

        public void SetBoard(Int32[,] entries)
        {
            for (Int32 column = 0; column < 5; ++column)
                for (Int32 row = 0; row < 5; ++row)
                    Board[column, row] = entries[column, row];
        }

        public Boolean HasBingo(Boolean useDiagonal = true)
        {
            if (useDiagonal && Internal_CheckDiagonal1())
                return true;

            if (useDiagonal && Internal_CheckDiagonal2())
                return true;

            for (Int32 i = 0; i < 5; ++i)
                if (Internal_CheckRow(i))
                    return true;

            for (Int32 i = 0; i < 5; ++i)
                if (Internal_CheckColumn(i))
                    return true;

            return false;
        }

        private Boolean Internal_CheckDiagonal1()
        {
            Boolean diagonal = true;
            for (Int32 i = 0; i < 5; ++i)
            {
                if(!Marks[i, i])
                {
                    diagonal = false;
                    break;
                }    
            }

            return diagonal;
        }

        private Boolean Internal_CheckDiagonal2()
        {
            Boolean diagonal = true;
            for (Int32 i = 0; i < 5; ++i)
            {
                if (!Marks[i, 4 - i])
                {
                    diagonal = false;
                    break;
                }
            }

            return diagonal;
        }

        private Boolean Internal_CheckRow(Int32 row)
        {
            Boolean diagonal = true;
            for (Int32 i = 0; i < 5; ++i)
            {
                if (!Marks[i, row])
                {
                    diagonal = false;
                    break;
                }
            }

            return diagonal;
        }
      
        private Boolean Internal_CheckColumn(Int32 column)
        {
            Boolean diagonal = true;
            for (Int32 i = 0; i < 5; ++i)
            {
                if (!Marks[column, i])
                {
                    diagonal = false;
                    break;
                }
            }

            return diagonal;
        }

    }
}
