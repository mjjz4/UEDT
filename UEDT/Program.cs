using System;
using System.IO;

//Pobieranie mapy do gry
class Map
{
    int x;
    int y;
    //mapfile to ścieżka z której będzie pobierana mapa do gry
    string mapfile;
    static string[] downloadmap = File.ReadAllLines(@"");
    string[,] map = new string[downloadmap.Length, downloadmap[0].Length];
    public void printMap()
    {
        //kod do ściagania mapy
    }

    public void move()
    {
        //kod do chodzenia po mapie
    }
}