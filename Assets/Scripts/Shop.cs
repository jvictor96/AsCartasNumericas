using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public string nome, tipo;
    public int preco, n;
    public bool comprado;
    Vector2 p0;
    // Start is called before the first frame update
    void Start()
    {
        p0 = transform.position;
        refresh();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = p0 + new Vector2(-GameObject.Find("Slider").GetComponent<Slider>().value,0);
    }

    public void comprar()
    {
        if(!comprado)
        {
            int m = PlayerPrefs.GetInt("moedas");
            if (PlayerPrefs.GetInt("moedas") >= preco)
            {
                PlayerPrefs.SetInt("moedas", m - preco);
                GameObject.Find("Moedas").GetComponent<Text>().text = "Você tem " + PlayerPrefs.GetInt("moedas").ToString() + " moeda";
                if (PlayerPrefs.GetInt("moedas") != 1)
                {
                    GameObject.Find("Moedas").GetComponent<Text>().text += "s";
                }
                PlayerPrefs.SetInt(tipo + n, 1);
                PlayerPrefs.Save();
            }
        } else
        {
            PlayerPrefs.SetInt(tipo, n);
            PlayerPrefs.Save();
            if(tipo == "fundo")
            {
                GameObject.Find("BackG").GetComponent<Fundo>().refresh();
            }
        }

        foreach (Transform t in GameObject.Find("Icones").transform)
        {
            t.GetComponent<Shop>().refresh();
        }
        foreach (Transform t in GameObject.Find("Cartas").transform)
        {
            t.GetComponent<Shop>().refresh();
        }
        foreach (Transform t in GameObject.Find("Fundos").transform)
        {
            t.GetComponent<Shop>().refresh();
        }
    }

    public void refresh()
    {
        if (PlayerPrefs.GetInt(tipo + n) == 1)
        {
            transform.GetChild(5).GetComponent<Text>().text = "Usar";
            comprado = true;
        }
        else
        {
            transform.GetChild(5).GetComponent<Text>().text = "Comprar";
            transform.GetChild(1).GetComponent<Text>().text = "$" + preco;
            transform.GetChild(2).GetComponent<Text>().text = "$" + preco;
        }
        if (PlayerPrefs.GetInt(tipo) == n)
        {
            transform.GetChild(5).GetComponent<Text>().text = "Usando";
        }
    }
}
