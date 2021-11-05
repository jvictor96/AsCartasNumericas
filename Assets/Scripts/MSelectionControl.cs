using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MSelectionControl : MonoBehaviour
{
    int Ntrilha, stars;
    float time;
    List<int> baralhoLocal;
    string trilha;
    GameObject previsualizador;

    // Start is called before the first frame update
    void Start()
    {
        stars = CountStars();
        time = Time.time;
        PlayerPrefs.SetInt("Trilha da Aventura",1);
        Ntrilha = PlayerPrefs.GetInt("trilha");
        refresh();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeLevel(int intut)
    {
        if(intut>0 && GameObject.Find("Trilhas").transform.childCount>Ntrilha+1)
        {
                Ntrilha += intut;
                refresh();
        } else if(Ntrilha>0 && intut<0)
        {
                Ntrilha += intut;
            refresh();
        }
    }

    void refresh()
    {
        int index = 0;
        trilha = GameObject.Find("Trilhas").transform.GetChild(Ntrilha).name;
        GameObject.Find("Titulo").GetComponent<Text>().text = trilha;
        
        foreach(Transform t in transform) { Destroy(t.gameObject); }
        GameObject lvls = Instantiate(GameObject.Find("Models").transform.GetChild(Ntrilha).gameObject);
        lvls.transform.SetParent(transform);
        lvls.transform.localScale = new Vector3(1, 1, 1);
        lvls.transform.localPosition = new Vector3(0, -250, 0);

        foreach (Transform t in lvls.transform.GetChild(1))
        {
            t.gameObject.GetComponent<Level>().baralho = GameObject.Find("Trilhas").transform.GetChild(Ntrilha).GetChild(index).GetComponent<Level>().baralho;
            t.gameObject.GetComponent<Level>().universe = GameObject.Find("Trilhas").transform.GetChild(Ntrilha).GetChild(index).GetComponent<Level>().universe;
            t.gameObject.GetComponent<Level>().finishes = PlayerPrefs.GetInt(trilha + index.ToString());
            GameObject.Find("Trilhas").transform.GetChild(Ntrilha).GetChild(index).GetComponent<Level>().finishes = PlayerPrefs.GetInt(trilha + index.ToString());

            if ((index == 0&& PlayerPrefs.GetInt(trilha) > 0)|| PlayerPrefs.GetInt(trilha + (index - 1).ToString()) > 0)
            {
                t.gameObject.GetComponent<MenuOption>().ready = true;
                t.GetChild(0).GetComponent<Image>().color = Color.white;
                t.GetChild(1).GetComponent<Image>().color = Color.white;
                t.GetChild(2).GetComponent<Image>().color = Color.white;
            } else
            {
                t.gameObject.GetComponent<MenuOption>().ready = false;
                t.GetChild(0).GetComponent<Image>().color = Color.gray;
                t.GetChild(1).GetComponent<Image>().color = Color.gray;
                t.GetChild(2).GetComponent<Image>().color = Color.gray;
            }

            if (index >= 6 && lvls.transform.GetChild(1).GetChild(index - 3).GetComponent<Level>().finishes > 0)
            {
                t.gameObject.GetComponent<MenuOption>().ready = true;
                t.GetChild(0).GetComponent<Image>().color = Color.white;
                t.GetChild(1).GetComponent<Image>().color = Color.white;
                t.GetChild(2).GetComponent<Image>().color = Color.white;
            }

            for (int i = 0; i < 2; i++)
            {
                if (t.gameObject.GetComponent<Level>().finishes > i)
                {
                    t.GetChild(i).GetComponent<Image>().color = Color.yellow;
                }
            }

            GameObject.Find("Anterior").GetComponent<Text>().enabled = Ntrilha > 0;
            GameObject.Find("Proximo").GetComponent<Text>().enabled = Ntrilha + 1 < GameObject.Find("Trilhas").transform.childCount;

            index++;
        }
    }
    
    int CountStars()
    {
        int n = 0;
        string trilha;
        for (int i = 0; i < GameObject.Find("Trilhas").transform.childCount; i++)
        {
            trilha = GameObject.Find("Trilhas").transform.GetChild(i).name;
            for (int j = 0; j < 8; j++)
            {
                n += PlayerPrefs.GetInt(trilha + j.ToString());
            }
        }

        if (n > 2)
        {
            Social.ReportProgress(AsCartas.GPGSIds.achievement_aspirante, 100.0f, (bool success) => {
                if (success)
                {
                    PlayerPrefs.SetInt("Aspirante", 1);
                }
            });
        }

        if (n > 10)
        {
            Social.ReportProgress(AsCartas.GPGSIds.achievement_pro, 100.0f, (bool success) => {
                if (success)
                {
                    PlayerPrefs.SetInt("Pro", 1);
                }
            });
        }
        return n;
    }

    public void startLevel(int inner)
    {
        int index = 0;
        if(Time.time-time>1)
        {
            baralhoLocal = transform.GetChild(0).GetChild(1).GetChild(inner).GetComponent<Level>().baralho;
            if (transform.GetChild(0).GetChild(1).GetChild(inner).GetComponent<Level>().finishes > 0)
            {
                foreach (int i in transform.GetChild(0).GetChild(1).GetChild(inner).GetComponent<Level>().universe)
                {
                    baralhoLocal.Add(i);
                }
            }

            if (transform.GetChild(0).GetChild(1).GetChild(inner).GetComponent<MenuOption>().ready)
            {
                PlayerPrefs.SetInt("tamanho", baralhoLocal.Count);
                foreach (int i in baralhoLocal)
                {
                    PlayerPrefs.SetInt("carta" + index.ToString(), i);
                    index++;
                }
                PlayerPrefs.SetInt("trilha", Ntrilha);
                PlayerPrefs.SetInt("nivel", inner);
                PlayerPrefs.SetInt("finishes", transform.GetChild(0).GetChild(1).GetChild(inner).GetComponent<Level>().finishes);
                if (inner == 0 && Ntrilha == 0 && PlayerPrefs.GetInt("finishes") == 0)
                {
                    PlayerPrefs.SetInt("tutorial", 1);
                }
                else
                {
                    PlayerPrefs.SetInt("tutorial", 0);
                }
                PlayerPrefs.SetInt("survival", 0);
                SceneManager.LoadScene("Game");
            }
        }
    }

    public void DeckSpoile(int i)
    {
        int index = 0, n = 0;
        int[] contador = new int[1050];
        GameObject clone;

        baralhoLocal = new List<int> (GameObject.Find("NBoxes").transform.GetChild(i).GetComponent<Level>().baralho);
        if(GameObject.Find("NBoxes").transform.GetChild(i).GetComponent<Level>().finishes == 0)
        {
            baralhoLocal.RemoveAt(baralhoLocal.Count - 1);
            baralhoLocal.RemoveAt(baralhoLocal.Count - 1);
        }

        foreach (Transform g in GameObject.Find("Relatorio").transform)
        {
            Destroy(g.gameObject);
        }
        foreach (int g in baralhoLocal)
        {
            contador[g]++;
        }
        foreach (int x in contador)
        {
            if (x > 0)
            {
                clone = Instantiate(GameObject.Find("mini"));
                clone.transform.SetParent(previsualizador.transform.Find("Relatorio").transform);
                clone.transform.localPosition = new Vector3(n % 5 * 50 - 100, -n / 5 * 50 - 45, 0);
                clone.transform.GetChild(1).GetComponent<Text>().text = index.ToString();
                clone.transform.GetChild(2).GetComponent<Text>().text = x.ToString();
                n++;
            }
            index++;
        }
    }
}
