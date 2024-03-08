using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Celda : MonoBehaviour
{

    [SerializeField] private int x, y;
    [SerializeField] private bool bomb;
    [SerializeField] private TextMeshProUGUI texto;
    [SerializeField] private GameObject imgMina;
    [SerializeField] private GameObject imgFlag;
    [SerializeField] private bool boolFlag = false;

    private bool coroutineRunning = false;

    public void SetBomb(bool bomb)
    {
        this.bomb = bomb;
    }

    public bool IsBomb()
    {
        return this.bomb;
    }

    public void SetX(int x)
    {
        this.x = x;
    }

    public int GetX()
    {
        return this.x;
    }
    public void SetY(int y)
    {
        this.y = y;
    }

    public int GetY()
    {
        return this.y;
    }

    public void SetText(string text)
    {
        this.texto.text = text;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.texto.gameObject.SetActive(false);
        imgFlag.gameObject.SetActive(false);
        imgMina.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(1) && boolFlag == false) 
        //{
        //    boolFlag = true;
        //    imgFlag.gameObject.SetActive(true);
        //}
        //else if (Input.GetMouseButtonDown(1) &&  boolFlag == true)
        //{
        //    boolFlag = false;
        //    imgFlag.gameObject.SetActive(false);
        //}

    }

    private void OnMouseDown()
    {
        if (coroutineRunning)
        {
            return;
        }

        {
            this.texto.gameObject.SetActive(true);
            if (this.IsBomb())
            {
                coroutineRunning = true;
                GetComponent<SpriteRenderer>().material.color = Color.red;
                imgMina.gameObject.SetActive(true);
                Generator.Instance.setWinner(false);
                StartCoroutine(CambiarADerrotaScene("DerrotaScene"));

            }
            else
            {
                int bombasAlrededor = Generator.Instance.GetBombsAround(x, y);
                texto.text = bombasAlrededor.ToString();
                Generator.Instance.AddTest();
                if (((Generator.Instance.getWidth() * Generator.Instance.getHeigth()) - Generator.Instance.getNBombs() == Generator.Instance.getNTest()) && Generator.Instance.isWinner())
                {
                    Debug.Log("Has ganado");
                    SceneManager.LoadScene("VictoriaScene");
                }
            }
        }



    }
    private IEnumerator CambiarADerrotaScene(string escena)
    {
        // Establecer la variable a true mientras se ejecuta la corutina
        coroutineRunning = true;

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(escena);

    }
}
