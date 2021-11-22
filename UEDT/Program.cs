using System;

namespace Elder_Doom
{
    class Gra
    {
        public int x = 2;
        public int y = 2;
        public const int size_x = 49;
        public const int size_y = 210;
        public int plz = 1;
        public void printMap()
        {
            float test = 0;
            string[,] map = new string[size_x, size_y];
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if ((i <= 19 && j <= i + test) || (i <= 19 && j >= (size_y - 10) - (test * 1.5f) /*- 4*/)) map[i, j] = "*"; //git

                    else if (i <= 19 && (j >= i + test + 1 && j <= i + test + 3) || (i <= 19 && j > (size_y - 14) - (test * 1.5f))) map[i, j] = "\u2580"; //git //git

                    else if ((i >= 20 && i <= 30) && j <= 122 - j || (i >= 20 && i <= 30) && j >= (size_y + 74) - j) map[i, j] = "*"; //git

                    else if (i > 30 && j <= (114 - (test / 1.25f) - (i / 10)) || i > 30 && j >= (size_y - 120) + (test / 1.25f) + (i / 10)) map[i, j] = "*"; //git

                    else if (map[i, j - 1] == "*" || map[i, (size_y - 8) - j] == "*") map[i, j] = "\u2580"; //git
                    else if (map[i, j - 2] == "*" || map[i, (size_y - 9) - j] == "*") map[i, j] = "\u2580"; //git

                    else if (i <= 6 && (map[i, j] != "*" && map[i, j] != "\u2580")) map[i, j] = "-";
                    else map[i, j] = " ";


                    Console.Write(map[i, j]);

                }
                test += 2f;
                Console.Write(Environment.NewLine);
            }
        }
        public void move()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.W) x--;
            if (keyInfo.Key == ConsoleKey.S) x++;
            if (keyInfo.Key == ConsoleKey.A) y--;
            if (keyInfo.Key == ConsoleKey.D) y++;

            //zeby postać nie wychodziła poza obszar
            if (x < 1)
                x = 1;
            else if (y < 1)
                y = 1;
            else if (x > size_x - 2)
                x = size_x - 2;
            else if (y > size_y - 2)
                y = size_y - 2;

            Console.Clear();
        }
        //public void 
    }
    class Program
    {
        static void Main(string[] args)
        {
            Gra test = new Gra();
            while (true)
            {
                Console.SetWindowSize(211, 50);
                test.printMap();
                test.move();
            }
        }
    }
}
