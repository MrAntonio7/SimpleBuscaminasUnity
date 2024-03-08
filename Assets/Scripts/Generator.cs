using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Generator : MonoBehaviour
{

    public static Generator Instance;
    //public GameObject canvas;
    [SerializeField] private GameObject celda;
    [SerializeField] private int width, height;
    [SerializeField] private int nBombs;
    private GameObject[][] map;
    private int x, y;
    private int nTest = 0;
    private bool winner = true;
    [SerializeField] private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam.orthographicSize = 5;
    }

    public void Facil()
    {
        Generador(8, 8, 10);
        cam.orthographicSize = 5;
    }
    public void Normal()
    {
        Generador(12, 12, 30);
        cam.orthographicSize = 7;
    }
    public void Dificil()
    {
        Generador(20, 20, 80);
        cam.orthographicSize = 11;
    }
    public void Generador(int width, int height, int nBombs)
    {
        //Inicializa la instancia
        Instance = this;

        //Creamos el canvas con las celdas
        map = new GameObject[width][];

        for (int i = 0; i < map.Length; i++)
        {
            map[i] = new GameObject[height];
        }


        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {

                map[i][j] = Instantiate(celda, new Vector3(i, j, 0), Quaternion.identity);
                map[i][j].GetComponent<Celda>().SetX(i);
                map[i][j].GetComponent<Celda>().SetY(j);
            }

        }


        //Situamos la cam
        Camera.main.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);


        //Situamos las bombas de forma random
        for (int i = 0; i < nBombs; i++)
        {

            x = Random.Range(0, width);
            y = Random.Range(0, height);
            if (!map[x][y].GetComponent<Celda>().IsBomb())
            {
                map[x][y].GetComponent<Celda>().SetBomb(true);
            }
            else
            {
                i--;
            }

        }
    }




    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddTest()
    {
        nTest++;
    }
    public void setWidth(int width)
    {
        this.width = width;
    }
    public int getWidth()
    {
        return width;
    }
    public void setHeight(int height)
    {
        this.height = height;
    }
    public int getHeigth()
    {
        return height;
    }
    public void setNBombs(int bomb)
    {
        this.nBombs = bomb;
    }
    public int getNBombs()
    {
        return this.nBombs;
    }
    public void setNTest(int test)
    {
        this.nTest = test;
    }
    public int getNTest()
    {
        return this.nTest;
    }

    public int GetBombsAround(int x, int y)
    {
        int contador = 0;

        //Casilla superior izq
        if (x > 0 && y < height - 1 && map[x - 1][y + 1].GetComponent<Celda>().IsBomb())
        {
            contador++;
        }

        //Casilla superior
        if (y < height - 1 && map[x][y + 1].GetComponent<Celda>().IsBomb())
        {
            contador++;
        }

        //Casilla superior derecha
        if (x < width - 1 && y < height - 1 && map[x + 1][y + 1].GetComponent<Celda>().IsBomb())
        {
            contador++;
        }

        //Casilla a la izquierda
        if (x > 0 && map[x - 1][y].GetComponent<Celda>().IsBomb())
        {
            contador++;
        }

        //Casilla a la derecha
        if (x < width - 1 && map[x + 1][y].GetComponent<Celda>().IsBomb())
        {
            contador++;
        }

        //Casilla inferior izq
        if (x > 0 && y > 0 && map[x - 1][y - 1].GetComponent<Celda>().IsBomb())
        {
            contador++;
        }

        //Casilla inferior
        if (y > 0 && map[x][y - 1].GetComponent<Celda>().IsBomb())
        {
            contador++;
        }

        //Casilla inferior derecha
        if (x < width - 1 && y > 0 && map[x + 1][y - 1].GetComponent<Celda>().IsBomb())
        {
            contador++;
        }

        return contador;

    }

    //public int GetBombsAround(int x, int y)
    //{
    //    int contador = 0;
    //    for (int i = Mathf.Max(0, x - 1); i <= Mathf.Min(width - 1, x + 1); i++)
    //    {
    //        for (int j = Mathf.Max(0, y - 1); j <= Mathf.Min(height - 1, y + 1); j++)
    //        {
    //            if (!(i == x && j == y) && map[i][j].GetComponent<Celda>().IsBomb())
    //            {
    //                contador++;
    //            }
    //        }
    //    }
    //    return contador;
    //}
    public void setWinner(bool win)
    {
        winner = win;
    }

    public bool isWinner()
    {
        return this.winner;
    }

}
