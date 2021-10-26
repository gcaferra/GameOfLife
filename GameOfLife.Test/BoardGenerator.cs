namespace GameOfLife.Test
{
    public class BoardGenerator
    {
        public Board Generate()
        {
            return new Board(new[,]
            {
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
            });
        }
    }
}