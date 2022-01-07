using System.Threading;
using System.IO;
using System.Text;
using static System.Console;
using System.Collections.Generic;
SetWindowSize(211, 50); //ustawia konsole pod git wielkosc
Game g = new Game();
g.Start();

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
        Console.Clear();
        Map myGame = new Map();
        while (true)
        {
            myGame.createMap();
            if (myGame.IsGameEnded())
                break;
            myGame.renderGame();
            myGame.move();
        }
        string koniec_gry_wygrales = @"
██╗░░██╗░█████╗░███╗░░██╗██╗███████╗░█████╗░  ░██████╗░██████╗░██╗░░░██╗
██║░██╔╝██╔══██╗████╗░██║██║██╔════╝██╔══██╗  ██╔════╝░██╔══██╗╚██╗░██╔╝
█████═╝░██║░░██║██╔██╗██║██║█████╗░░██║░░╚═╝  ██║░░██╗░██████╔╝░╚████╔╝░
██╔═██╗░██║░░██║██║╚████║██║██╔══╝░░██║░░██╗  ██║░░╚██╗██╔══██╗░░╚██╔╝░░
██║░╚██╗╚█████╔╝██║░╚███║██║███████╗╚█████╔╝  ╚██████╔╝██║░░██║░░░██║░░░
╚═╝░░╚═╝░╚════╝░╚═╝░░╚══╝╚═╝╚══════╝░╚════╝░  ░╚═════╝░╚═╝░░╚═╝░░░╚═╝░░░

░██╗░░░░░░░██╗██╗░░░██╗░██████╗░██████╗░░█████╗░██╗░░░░░███████╗░██████╗
░██║░░██╗░░██║╚██╗░██╔╝██╔════╝░██╔══██╗██╔══██╗██║░░░░░██╔════╝██╔════╝
░╚██╗████╗██╔╝░╚████╔╝░██║░░██╗░██████╔╝███████║██║░░░░░█████╗░░╚█████╗░
░░████╔═████║░░░╚██╔╝░░██║░░╚██╗██╔══██╗██╔══██║██║░░░░░██╔══╝░░░╚═══██╗
░░╚██╔╝░╚██╔╝░░░░██║░░░╚██████╔╝██║░░██║██║░░██║███████╗███████╗██████╔╝
░░░╚═╝░░░╚═╝░░░░░╚═╝░░░░╚═════╝░╚═╝░░╚═╝╚═╝░░╚═╝╚══════╝╚══════╝╚═════╝░
Gratulacje Wygrałeś!!! "+"\n"+"\n";
        Clear();
        SetCursorPosition(0, 0);
        Write(koniec_gry_wygrales);

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
        SetCursorPosition(0, 0);
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
            SetCursorPosition(0, i+9);
            WriteLine($"{prefix}<< {currentOption} >>");
        }
        ResetColor();
    }

    public int MenuMove()
    {
        ConsoleKey keyPressed;
        do
        {
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
    int x = 1; int y = 1;
    int last_x = 1; int last_y = 1;
    bool out_of_bounds = false; 
    const int size_x = 14; const int size_y = 50;
    int rotation = 0; string player = "v"; //*
    string sciana = "\u2588"; bool game_over_end;   
    int level = 0;
    
    //mapfile to ścieżka z której będzie pobierana mapa do gry
    //string mapfile;
    //static string[] downloadmap = File.ReadAllLines(@"");
    //string[,] map = new string[downloadmap.Length, downloadmap[0].Length];

    string[,] map = new string[size_x, size_y];
    string[,] game = new string[34, 210];
    string[,] gui = new string[14, 210];
    public void createMap()
    {
        out_of_bounds = false;
        string[] loaded_map = File.ReadAllLines(@$"..\..\..\mapa.txt");
        for (int i = 0; i < map.GetLength(0); i++)
        {
            if (out_of_bounds)
            {
                map[last_x, last_y] = player;//"*";
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
                        //map[i, j] = "*";
                        level += 1;
                        x = 1; y = 1;
                    }
                    map[i, j] = "$";
                }
                else if (horizontal_segment[j] == '9')
                {
                    if (i == x & j == y)
                    {
                        //map[i, j] = "*";
                        game_over_end = true;
                    }
                    map[i, j] = "W";
                }
                else if (i == x & j == y) map[i, j] = player;
                else map[i, j] = " ";
            }
        }
    }
    public bool IsGameEnded()
    {
        return game_over_end;
    }
    public void move()
    {
        SetCursorPosition(0, 0);
        last_x = x; last_y = y;
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        /*
        if (keyInfo.Key == ConsoleKey.W) { 
            x--; }
        if (keyInfo.Key == ConsoleKey.S) {
            x++; }
        if (keyInfo.Key == ConsoleKey.A) {
            y--; }
        if (keyInfo.Key == ConsoleKey.D) { 
            y++; }
        if (keyInfo.Key == ConsoleKey.W) { player = ">";
            y++; }
        if (keyInfo.Key == ConsoleKey.S) { player = "<";
            y--; }
        if (keyInfo.Key == ConsoleKey.A) { player = "\u25B2";
            x--; }
        if (keyInfo.Key == ConsoleKey.D) { player = "v";
            x++; }
        */
        if (keyInfo.Key == ConsoleKey.RightArrow) { player = ">";
            rotation = 2; }
        if (keyInfo.Key == ConsoleKey.LeftArrow) { player = "<";
            rotation = 3; }
        if (keyInfo.Key == ConsoleKey.UpArrow) { player = "\u25B2";
            rotation = 1; }
        if (keyInfo.Key == ConsoleKey.DownArrow) { player = "v";
            rotation = 0; }

        switch(rotation)
        {
            case 2: {
                if (keyInfo.Key == ConsoleKey.W)
                    y++;
                break;
            }
            case 3: {
                if (keyInfo.Key == ConsoleKey.W)
                    y--;
                break;
            }
            case 1: {
                if (keyInfo.Key == ConsoleKey.W)
                    x--;
                break;
            }
            case 0: {
                if (keyInfo.Key == ConsoleKey.W)
                    x++;
                break;
            }
            default: break;
        }
        
        //zeby postać nie wychodziła poza obszar tablicy
        if (x < 1)
                x = 1;
            else if (y < 1)
                y = 1;
            else if (x > size_x - 2)
                x = size_x - 2;
            else if (y > size_y - 2)
                y = size_y - 2;
        
    }
    public void renderGame()
    {
        float a = 0;
        for (int i = 0; i < game.GetLength(0); i++) //34
        {
            for (int j = 0; j < game.GetLength(1); j++)
            {
                //Rysowanie Rzeczy przed graczem
                if ((j > 80 && j < 140) && (i > 7 && i < 23))
                {
                    //ściana przed graczem
                    if (rotation==2 && map[x, y+1] == sciana || rotation == 3 && map[x, y - 1] == sciana 
                        || rotation == 1 && map[x-1, y] == sciana || rotation == 0 && map[x + 1, y] == sciana)
                        game[i, j] = "\u2593"; //Niemogłem się zdecydować jak rysować ściane przed nami, jak był ten sam znak co przy perspektywnie tak sobie to wyglądało
                    else if ((rotation == 2 && map[x, y + 1] == "W" || rotation == 3 && map[x, y - 1] == "W"
                        || rotation == 1 && map[x - 1, y] == "W" || rotation == 0 && map[x + 1, y] == "W")) {
                        if ((j > 100 && j < 120) && i > 12)
                            game[i, j] = " ";
                        else
                            game[i, j] = "#";
                    }
                    else
                        game[i, j] = " ";
                }
                /*lewa strona*/
                //lewa górna ściana
                else if (i <= 10 && j <= 30) game[i, j] = "\u2588";
                else if (i <= 10 && (j > 30 && j <= 30 + i + a)) game[i, j] = "\u2588";
                //lewa środkowa ściana
                else if ((i > 10 && i <= 20) && j <= 80) game[i, j] = "\u2588";
                //lewa dolna ściana
                else if (i > 20 && j <= 30) game[i, j] = "\u2588";
                else if (i > 20 && j <= 150 - a) game[i, j] = "\u2588";

                /*prawa strona*/
                //prawa górna ściana
                else if (i <= 10 && j >= 210 - 20) game[i, j] = "\u2588";
                else if (i <= 10 && (j >= (210 - 20) - (a + 9))) game[i, j] = "\u2588";
                //prawa środkowa
                else if ((i > 10 && i <= 20) && j >= 210 - 70) game[i, j] = "\u2588";
                //lewa dolna ściana
                else if ((i > 20 && i < 30) && j >= 210 - 30) game[i, j] = "\u2588";
                else if (i > 20 && j >= 51 + (a * 1.25)) game[i, j] = "\u2588"; 

                else game[i, j] = " ";
                Console.Write(game[i, j]);
            }
            a += 3.5f;
            Console.Write(Environment.NewLine);
        }
        

        for (int i = 0; i < gui.GetLength(0); i++)
        {
            for (int j = 0; j < gui.GetLength(1); j++)
            {
                if (i == 0 && j < 60 || i > 0 && j < 1 || i == gui.GetLength(0) - 1 && j < 60 || i < gui.GetLength(0) && j==60)
                    gui[i, j] = "\u2592";
                else if ((x == i && y == j) || (x + 1 == i && y + 1 == j) || (x + 1 == i && y == j) || (x + 1 == i && y - 1 == j)
                    || (x - 1 == i && y + 1 == j) || (x == i && y + 1 == j) || (x - 1 == i && y - 1 == j)
                    || (x - 1 == i && y == j) || (x == i && y - 1 == j))//pole widzenia na mapie (manualne)//j < 50)
                    gui[i, j] = map[i, j];
                else
                    gui[i, j] = "\\";
                Console.Write(gui[i, j], Encoding.ASCII);
            }
            Console.Write(Environment.NewLine);
        }
        
    }
}
