using System.Threading.Channels;

namespace memento_pattern
{
    //A demonstration of the Memento pattern in C#
    internal class Program
    {
        static void Main(string[] args)
        {
            var player = new Player();

            player.MoveForward();
            player.MoveForward();
            player.MoveForward();
            player.MoveBackward();
            player.MoveBackward();
            player.MoveForward();
            player.MoveForward();

            /* OUTPUT:
             
                Player has moved 2 and is now as position 2
                Player has moved 5 and is now as position 7
                Player has moved 4 and is now as position 11
                Player is now at position 7
                Player is now at position 2
                Player has moved 1 and is now as position 3
                Player has moved 4 and is now as position 7
             
             */
        }
    }

    public class Player
    {
        //Any collection will work here; just needs to be able to "remember" sequential postions
        List<Position> positionMemory = new();
        Position.Piece piece = new();

        public void MoveForward()
        {
            var position = piece.GetPosition();

            positionMemory.Add(position);

            int n = new Random().Next(1, 6);

            piece.Move(n);
        }

        public void MoveBackward()
        {
            var position = positionMemory.Last();

            positionMemory.Remove(position);
            piece.SetPosition(position);
        }
    }

    public struct Position
    {
        //This is private because we don't want it change externally since that would falsefy state; only inside this Position
        private int Cell { get; set; }

        public class Piece
        {
            public Position position = new Position() { Cell = 0 };

            public void Move(int n)
            {
                position.Cell += n;
                Console.WriteLine("Player has moved " + n + " and is now as position " + position.Cell);
            }

            //Create a new Position so if state is copied across Positions, each piece will move independently
            public Position GetPosition() => new Position() { Cell = position.Cell };

            public void SetPosition(Position p)
            {
                position.Cell = p.Cell;
                Console.WriteLine("Player is now at position " + p.Cell);
            }
        }
    }
}
