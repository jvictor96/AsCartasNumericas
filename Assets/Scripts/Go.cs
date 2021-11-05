using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Go : MonoBehaviour
{
    public int bigValue, n, naMao, lastScore, lastHand, score=0;
    public List<int> baralho, deck = new List<int>();
    public float fade;
    public bool tutorial = false, survival = false, venceu = false;

    void Awake()
    {
        if (PlayerPrefs.GetInt("tutorial") == 1)
        {
            tutorial = true;
        }
        else
        {
            gameObject.GetComponent<Tutorial>().enabled = false;
        }

        if (PlayerPrefs.GetInt("survival") == 1)
        {
            survival = true;
            deck = GameObject.Find("Modos").transform.GetChild(PlayerPrefs.GetInt("modo")).GetComponent<Level>().baralho;
        }
        else
        {
            GetComponent<SurvivalEngine>().enabled = false;
            GameObject.Find("Score").GetComponent<Text>().enabled = false;
            for (int i = 0; i < PlayerPrefs.GetInt("tamanho"); i++)
            {
                deck.Add(PlayerPrefs.GetInt("carta" + i.ToString()));
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        hardReset();
    }

    // Update is called once per frame
    void Update()
    {
        float r = GameObject.Find("TableTop").GetComponent<UIControl>().reaction;
        if (r > 1)
        {
            transform.GetChild(0).GetComponent<Image>().color = new Color(0.5f * (Mathf.Sin(r * 20) + 1) / 2, 0.8f * (Mathf.Sin(r * 20) + 1) / 2, 1f);
        }
        else if (r < -1)
        {
            transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 0.8f * (Mathf.Sin(r * 20) + 1) / 2, 0.5f * (Mathf.Sin(r * 20) + 1) / 2);
        } else
        {
            transform.GetChild(0).GetComponent<Image>().color = GameObject.Find("Card1").transform.GetChild(0).GetComponent<Skin>().baseColor;
        }
        if (GetComponent<Tutorial>().target == 9)
        {
            transform.GetChild(0).GetComponent<Image>().color = Color.HSVToRGB(Mathf.Cos(Time.frameCount / 250f) / 2f + 0.5f, 1, 1f);
        }

        fadeIn();
    }

    void DeckSpoiler()
    {
        int index = 0, k = 0;
        int[] contador = new int[1050];
        GameObject clone;

        foreach (Transform g in GameObject.Find("Relatorio").transform)
        {
            Destroy(g.gameObject);
        }
        foreach (int g in GameObject.Find("TableTop").GetComponent<Go>().baralho)
        {
            contador[g]++;
        }
        foreach (int x in contador)
        {
            if (x > 0)
            {
                clone = Instantiate(GameObject.Find("mini"));
                clone.transform.SetParent(GameObject.Find("Relatorio").transform);
                clone.transform.localPosition = new Vector3(k % 5 * 75 - 150, -k / 5 * 75 - 60, 0);
                clone.transform.GetChild(1).GetComponent<Text>().text = index.ToString();
                clone.transform.GetChild(2).GetComponent<Text>().text = x.ToString();
                k++;
            }
            index++;
        }
        if (GameObject.Find("TableTop").GetComponent<Go>().baralho.Count == 0)
        {
            GameObject.Find("Relatorio").GetComponent<Text>().text = "";
        }
    }

    public void smartclick() {
        if(tutorial)
        {
            GetComponent<Tutorial>().play("TableTop");
        } else if(venceu&&survival)
        {
            GetComponent<SurvivalEngine>().addCards();
        } else
        {
            click();
        }
    }


    public void click()
    {

        n = 0;
        bigValue = 0;
        foreach (Transform g in GameObject.Find("Hand").transform)
        {
            if(g.GetComponent<PlayerHand>().select)
            {
                n++;
                if (g.GetComponent<PlayerHand>().i > bigValue)
                {
                    bigValue = g.GetComponent<PlayerHand>().i;
                }
            }
        }

        if(n > 2) {
            lastScore = Check();
            GetComponent<UIControl>().vidas -= lastScore;

            if (GetComponent<UIControl>().vidas <= 0)
            {
                GetComponent<UIControl>().lose();
            }

            GetComponent<UIControl>().react(Check() == 0);

            naMao = 0;
            foreach (Transform g in GameObject.Find("Hand").transform)
            {
                if (g.GetComponent<PlayerHand>().select)
                {
                    score += 1;
                    if (baralho.Count > 0)
                    {
                        g.GetComponent<PlayerHand>().i = baralho[0];
                        baralho.RemoveAt(0);
                    }
                    else
                    {
                        g.GetComponent<PlayerHand>().done = true;
                    }
                    g.GetComponent<PlayerHand>().select = false;
                    g.GetComponent<PlayerHand>().setup();
                    g.transform.Rotate(1, 1, 1);
                }

                if (!g.GetComponent<PlayerHand>().done)
                {
                    naMao++;
                }
            }
            GameObject.Find("Score").GetComponent<Text>().text = score.ToString() + " pontos";

            venceu = naMao <= 2 & GetComponent<UIControl>().vidas > 0;

            if (venceu)
            {
                GetComponent<UIControl>().win();
            }
        }



    }
    public bool IsOver()
    {
        if (Mathf.Abs(GetComponent<RectTransform>().position.x - Input.mousePosition.x) < GetComponent<RectTransform>().rect.width / 2)
        {
            if (Mathf.Abs(GetComponent<RectTransform>().position.y - Input.mousePosition.y) < GetComponent<RectTransform>().rect.height / 2)
            {
                return true;
            }
        }
        return false;
    }
    int Check()
    {
        int stack = 1;
        foreach (Transform g in GameObject.Find("Hand").transform)
        {
            if (g.GetComponent<PlayerHand>().select)
            {
                stack *= g.GetComponent<PlayerHand>().i;
            }
        }
        lastHand = stack/bigValue;
        if (Mathf.Abs(stack / bigValue - bigValue) > 1000)
        {
            return 4;
        }
        if (Mathf.Abs(stack / bigValue - bigValue) > 100)
        {
            return 3;
        }
        if (Mathf.Abs(stack / bigValue - bigValue) > 10)
        {
            return 2;
        }
        if(Mathf.Abs(stack / bigValue - bigValue) > 0)
        {
            return 1;
        }
        return 0;
    }

    public void reset()
    {
        baralho = shuffle(new List<int>(deck));
        venceu = false;
        foreach (Transform g in GameObject.Find("Hand").transform)
        {
            g.GetComponent<PlayerHand>().i = baralho[0];
            g.GetComponent<PlayerHand>().done = false;
            g.GetComponent<PlayerHand>().select = false;
            baralho.RemoveAt(0);
            g.GetComponent<PlayerHand>().setup();
        }
        GameObject.Find("MenuArcade").GetComponent<Canvas>().enabled = false;
        GameObject.Find("MenuBuilding").GetComponent<Canvas>().enabled = false;
    }

    public void hardReset()
    {
        score = 0;
        GetComponent<UIControl>().vidas = 4;
        GameObject.Find("closeA").GetComponent<Text>().enabled = true;
        GameObject.Find("closeD").GetComponent<Text>().enabled = true;
        reset();
    }

    List<int> shuffle(List<int> input)
    {
        int index, size;
        List<int> output = new List<int>();
        if(!survival)
        {
            for (int i = 0; i < 9; i++)
            {
                output.Add(input[0]);
                input.RemoveAt(0);
            }
        }
        size = input.Count;
        for (int i = 0; i < size; i++)
        {
            index = Random.Range(0, input.Count - 1);
            output.Add(input[index]);
            input.RemoveAt(index);
        }
        return output;
    }

    public void fadeIn()
    {
        Color c = transform.GetChild(0).GetComponent<Image>().color;
        c = new Color(c.r, c.g, c.b, fade);
        transform.GetChild(0).GetComponent<Image>().color = c;
    }
}
