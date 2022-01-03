Map myGame = new Map();
while (true)
{
    myGame.printMap();
    myGame.move();
}


//Pobieranie mapy do gry
class Map
{
    int x = 1;
    int y = 1;
    bool out_of_bounds = false;
    int last_x = 1;
    int last_y = 1;
    const int size = 20;
    int level = 0;

    //mapfile to ścieżka z której będzie pobierana mapa do gry
    //string mapfile;
    //static string[] downloadmap = File.ReadAllLines(@"");
    //string[,] map = new string[downloadmap.Length, downloadmap[0].Length];
    string[,] map = new string[size, size];
    public void printMap()
    {
        out_of_bounds = false;
        string[] loaded_map = File.ReadAllLines(@$"..\..\..\mapa.txt");//..\..\..\labirynt{level}.txt żeby w projekcie odpalać pliki z katalogu projektu a nie net5.0
        for (int i = 0; i < map.GetLength(0); i++)
        {
            if (out_of_bounds)
            {
                x = last_x; y = last_y;
                out_of_bounds = false;
            }

            string horizontal_segment = loaded_map[i];
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (horizontal_segment[j] == '1')
                {
                    if (i == x & j == y)
                        out_of_bounds = true;
                    map[i, j] = "\u2588";
                }
                else if (horizontal_segment[j] == '8')
                {
                    if (i == x & j == y)
                    {
                        map[i, j] = "*";
                        level += 1;
                        x = 1; y = 1;
                    }
                    map[i, j] = "$";
                }
                /*else if (horizontal_segment[j] == '9')
                {
                    if (i == x & j == y)
                    {
                        map[i, j] = "*";
                        end_game = true;
                    }
                    map[i, j] = "$";
                }*/
                else if (i == x & j == y) map[i, j] = "*";
                else if (i == last_x & j == last_y) map[i, j] = "*";
                else map[i, j] = " ";

                Console.Write(map[i, j]);

            }
            Console.Write(Environment.NewLine);

        }
    }

    public void move()
    {
        last_x = x; last_y = y;
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        if (keyInfo.Key == ConsoleKey.W) x--;
        if (keyInfo.Key == ConsoleKey.S) x++;
        if (keyInfo.Key == ConsoleKey.A) y--;
        if (keyInfo.Key == ConsoleKey.D) y++;

        //zeby postać nie wychodziła poza obszar tablicy
        if (x < 1)
            x = 1;
        else if (y < 1)
            y = 1;
        else if (x > size - 2)
            x = size - 2;
        else if (y > size - 2)
            y = size - 2;
        Console.Clear();
    }
}
