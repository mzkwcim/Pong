using System.Threading;
using System.Threading.Tasks;
ArcadeGame game = new ArcadeGame();
await game.RunGame();
class ArcadeGame
{
    int ballX = 50;
    int ballY = 15;
    int ballDX = 1;
    int ballDY = 1;
    int leftPaddleY = 12;
    int rightPaddleY = 12;
    int scorePlayer1 = 0;
    int scorePlayer2 = 0;

    public void BallUpdate()
    {
        ballX += ballDX;
        ballY += ballDY;

        if (ballX <= 0 || ballX >= 119)
        {
            ballDX *= -1;
        }
        else if (ballY <= 0 || ballY >= 29)
        {
            ballDY *= -1;
        }
        else if ((ballX == 1 && ballY >= leftPaddleY && ballY < leftPaddleY + 6) ||
                 (ballX == 118 && ballY >= rightPaddleY && ballY < rightPaddleY + 6))
        {
            ballDX *= -1;
        }
    }
    public async Task ChoseMove()
    {
        await Task.Run(() =>
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.W:
                        leftPaddleY = MoveUpLeft(leftPaddleY);
                        break;
                    case ConsoleKey.S:
                        leftPaddleY = MoveDownLeft(leftPaddleY);
                        break;
                    case ConsoleKey.UpArrow:
                        rightPaddleY = MoveUpRight(rightPaddleY);
                        break;
                    case ConsoleKey.DownArrow:
                        rightPaddleY = MoveDownRight(rightPaddleY);
                        break;
                }
            }
        });    
    }
    public void DeleteBall()
    {
        Console.SetCursorPosition(ballX, ballY);
        Console.Write(" ");
    }
    public void DrawBall()
    {
        Console.SetCursorPosition(ballX, ballY);
        Console.Write("#");
    }
    public void DrawScore()
    {
        Console.SetCursorPosition(50, 0);
        Console.Write("Player 1: {0}", scorePlayer1);

        Console.SetCursorPosition(50, 29);
        Console.Write("Player 2: {0}", scorePlayer2);
    }
    public void EraserLeftDown(int end)
    {
        for (int i = 0; i < end; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write(" ");
        }
    }
    public void EraserLeftUp(int end)
    {
        for (int i = end; i < 30; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write(" ");
        }
    }
    public void EraserRightDown(int end)
    {
        for (int i = 0; i < end; i++)
        {
            Console.SetCursorPosition(119, i);
            Console.Write(" ");
        }
    }
    public void EraserRightUp(int end)
    {
        for (int i = end; i < 30; i++)
        {
            Console.SetCursorPosition(119, i);
            Console.Write(" ");
        }
    }
    public void LeftBoard(int start)
    {
        for (int i = start; i < start + 6; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write("#");
        }
    }
    public int MoveUpLeft(int highLeft)
    {
        int[] position = new int[] { 0, highLeft };
        if (position[1] <= 24 && position[1] > 0)
        {
            EraserLeftUp(position[1] - 1);
            LeftBoard(position[1] - 1);
            Console.SetCursorPosition(position[0], position[1] - 1);
            return position[1] - 1;
        }
        else if (position[1] == 0)
        {
            return position[1];
        }
        else
        {
            return 0;
        }
    }
    public int MoveUpRight(int highRight)
    {
        int[] position = new int[] { 119, highRight };
        if (position[1] <= 24 && position[1] > 0)
        {
            EraserRightUp(position[1] - 1);
            RightBoard(position[1] - 1);
            Console.SetCursorPosition(position[0], position[1] - 1);
            return position[1] - 1;
        }
        else if (position[1] == 0)
        {
            return position[1];
        }
        else
        {
            return 0;
        }
    }
    public int MoveDownLeft(int highLeft)
    {
        int[] position = new int[] { 0, highLeft };
        if (position[1] < 24 && position[1] >= 0)
        {
            EraserLeftDown(position[1] + 1);
            LeftBoard(position[1] + 1);
            Console.SetCursorPosition(position[0], position[1] + 1);
            return highLeft + 1;
        }
        else if (position[1] == 24)
        {
            EraserLeftDown(24);
            LeftBoard(position[1]);
            Console.SetCursorPosition(0, position[1]);
            return position[1];
        }
        else
        {
            return 0;
        }
    }
    public int MoveDownRight(int highRight)
    {
        int[] position = new int[] { 0, highRight };
        if (position[1] < 24 && position[1] >= 0)
        {
            EraserRightDown(position[1] + 1);
            RightBoard(position[1] + 1);
            Console.SetCursorPosition(position[0], position[1] + 1);
            return highRight + 1;
        }
        else if (position[1] == 24)
        {
            EraserRightDown(24);
            RightBoard(position[1]);
            Console.SetCursorPosition(0, position[1]);
            return position[1];
        }
        else
        {
            return 0;
        }
    }
    public void RightBoard(int start)
    {
        for (int i = start; i < start + 6; i++)
        {
            Console.SetCursorPosition(119, i);
            Console.Write("#");
        }
    }
    public async Task RunGame()
    {
        Console.CursorVisible = false;
        while (true)
        {
            BallUpdate();
            ScoreUpdate();
            DrawBall();
            LeftBoard(leftPaddleY);
            RightBoard(rightPaddleY);
            DrawScore();
            await ChoseMove();
            await Task.Delay(50);
            DeleteBall();
        }
    }
    public void ScoreUpdate()
    {
        if (ballX == 0)
        {
            scorePlayer2++;
            ballX = 50;
            ballDX *= -1;
        }
        else if (ballX == 119)
        {
            scorePlayer1++;
            ballX = 50;
            ballDX *= -1;
        }
    }
}