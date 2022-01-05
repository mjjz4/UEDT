using System.Threading;
using System.IO;
using System.Text;
using static System.Console;
using System.Collections.Generic;
using Mindmagma.Curses;
Map myGame = new();
Game g = new();
Thread watek = new(myGame.move);
g.Start();

/* Daremne próby anty skakani czcionki
 * |
 * |
 * v
 * watek.Start();
while (true)
{
    myGame.printMap();
    Thread.Sleep(1000);
    Console.Clear();
    if (watek.IsAlive)
    {
        watek.Start();
    }
    else
    {
        continue;
    }

}*/

// Próba nie migania
var Screen = NCurses.InitScreen();
NCurses.NoDelay(Screen, true);
NCurses.NoEcho();
//

class Game
{
    public void Start()
    {
        Title = "Under Elder Doom Tale";
        StartMenu();
    }

    private void StartMenu()
    {
        string tytul = @"
██╗   ██╗███╗   ██╗██████╗ ███████╗██████╗     ███████╗██╗     ██████╗ ███████╗██████╗     ██████╗  ██████╗  ██████╗ ███╗   ███╗    ████████╗ █████╗ ██╗     ███████╗
██║   ██║████╗  ██║██╔══██╗██╔════╝██╔══██╗    ██╔════╝██║     ██╔══██╗██╔════╝██╔══██╗    ██╔══██╗██╔═══██╗██╔═══██╗████╗ ████║    ╚══██╔══╝██╔══██╗██║     ██╔════╝
██║   ██║██╔██╗ ██║██║  ██║█████╗  ██████╔╝    █████╗  ██║     ██║  ██║█████╗  ██████╔╝    ██║  ██║██║   ██║██║   ██║██╔████╔██║       ██║   ███████║██║     █████╗  
██║   ██║██║╚██╗██║██║  ██║██╔══╝  ██╔══██╗    ██╔══╝  ██║     ██║  ██║██╔══╝  ██╔══██╗    ██║  ██║██║   ██║██║   ██║██║╚██╔╝██║       ██║   ██╔══██║██║     ██╔══╝  
╚██████╔╝██║ ╚████║██████╔╝███████╗██║  ██║    ███████╗███████╗██████╔╝███████╗██║  ██║    ██████╔╝╚██████╔╝╚██████╔╝██║ ╚═╝ ██║       ██║   ██║  ██║███████╗███████╗
 ╚═════╝ ╚═╝  ╚═══╝╚═════╝ ╚══════╝╚═╝  ╚═╝    ╚══════╝╚══════╝╚═════╝ ╚══════╝╚═╝  ╚═╝    ╚═════╝  ╚═════╝  ╚═════╝ ╚═╝     ╚═╝       ╚═╝   ╚═╝  ╚═╝╚══════╝╚══════╝
Witaj w UEDT użyj 'W'\'S' lub strzałek '^'\'v' żeby poruszać się po menu oraz 'enter' by zatwierdzić wybór
";
        string[] opcje = { "Start", "Opcje", "Wyjście" };
        Menu MainMenu = new Menu(tytul, opcje);
        int selectedIndex = MainMenu.MenuMove();

        switch (selectedIndex)
        {
            case 0:
                StartTheGame();
                break;
            case 1:
                Opcje();
                break;
            case 2:
                WriteLine("\n Zamykanie...");
                Environment.Exit(0);
                break;
        }
    }
    private void StartTheGame()
    {

    }

    private void Opcje()
    {

    }

}

class Menu
{
    private int SelectIndex;
    private string[] Options;
    private string Prompt;
    public Menu(string prompt, string[] options)
    {
        Prompt = prompt;
        Options = options;
        SelectIndex = 0;
    }

    public void DisplayOptions()
    {
        WriteLine(Prompt);
        for (int i = 0; i < Options.Length; i++)
        {
            string currentOption = Options[i];
            string prefix;

            if (i == SelectIndex)
            {
                prefix = "*";
                ForegroundColor = ConsoleColor.Black;
                BackgroundColor = ConsoleColor.White;
            }
            else
            {
                prefix = " ";
                ForegroundColor = ConsoleColor.White;
                BackgroundColor = ConsoleColor.Black;
            }

            WriteLine($"{prefix}<< {currentOption} >>");
        }
        ResetColor();
    }

    public int MenuMove()
    {
        ConsoleKey keyPressed;
        do
        {
            Clear();
            DisplayOptions();

            ConsoleKeyInfo keyInfo = ReadKey(true);
            keyPressed = keyInfo.Key;
            if (keyPressed == ConsoleKey.UpArrow | keyPressed == ConsoleKey.W)
            {
                SelectIndex--;
                if (SelectIndex == -1) SelectIndex = Options.Length - 1;
            }
            else if (keyPressed == ConsoleKey.DownArrow | keyPressed == ConsoleKey.S)
            {
                SelectIndex++;
                if (SelectIndex == Options.Length) SelectIndex = 0;
            }
        } while (keyPressed != ConsoleKey.Enter);

        return SelectIndex;
    }
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
                else if (i == x & j == y) map[i, j] = ".";
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
    }

}
