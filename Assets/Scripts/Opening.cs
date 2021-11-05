using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opening : MonoBehaviour
{
    public float tempo, ajst;
    bool start = false;
    public GameObject target;
    public string[] dicas;
    // Start is called before the first frame update
    void Start()
    {
        tempo = Time.time;
        if(tag == "Canvas")
        {
            GameObject.Find("LMessage").GetComponent<Text>().text = dicas[Random.Range(0, 6)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        float index = 0;
        if(start)
        {
            tempo += Time.deltaTime/2f;
            foreach (Transform g in target.transform)
            {
                if (g.tag == "Image")
                {
                    g.GetComponent<Image>().color = fade(g.GetComponent<Image>().color, Time.time - tempo - index / ajst);
                    index++;
                }
                else if (g.tag == "Text")
                {
                    g.GetComponent<Text>().color = fade(g.GetComponent<Text>().color, Time.time - tempo - index / ajst);
                    index++;
                }
                else if (g.tag == "Card")
                {
                    g.GetComponent<PlayerHand>().fade = Time.time - tempo - index / ajst;
                    index++;
                }
                else if (g.tag == "Go")
                {
                    g.GetComponent<Go>().fade = Time.time - tempo - index / ajst;
                    index++;
                }

                foreach (Transform t in g)
                {
                    if (t.tag == "Image")
                    {
                        t.GetComponent<Image>().color = fade(t.GetComponent<Image>().color, Time.time - tempo - index / ajst);
                        index++;
                    }
                    else if (t.tag == "Text")
                    {
                        t.GetComponent<Text>().color = fade(t.GetComponent<Text>().color, Time.time - tempo - index / ajst);
                        index++;
                    }
                    else if (t.tag == "Card")
                    {
                        t.GetComponent<PlayerHand>().fade = Time.time - tempo - index / ajst;
                        index++;
                    }

                    foreach (Transform a in t)
                    {
                        if (a.tag == "Image")
                        {
                            a.GetComponent<Image>().color = fade(a.GetComponent<Image>().color, Time.time - tempo - index / ajst);
                            index++;
                        }
                        else if (a.tag == "Text")
                        {
                            a.GetComponent<Text>().color = fade(a.GetComponent<Text>().color, Time.time - tempo - index / ajst);
                            index++;
                        }
                        else if (a.tag == "Card")
                        {
                            a.GetComponent<PlayerHand>().fade = Time.time - tempo - index / ajst;
                            index++;
                        }
                    }
                }
            }
        } else
        {
            if(Time.time-tempo<3)
            {
                GameObject.Find("Dot").transform.localPosition = new Vector3(Mathf.Sin(Time.time*6f + Mathf.Sin(Time.time*4f)) * 25f, Mathf.Cos(Time.time*6f+Mathf.Sin(Time.time*4f)) * 25f - 20f, 0);
                GameObject.Find("Start").GetComponent<Text>().color = new Color(255, 255, 255, 0);
            } else
            {
                GameObject.Find("Dot").transform.localPosition = new Vector3(Mathf.Sin(Time.time * 6f + Mathf.Sin(Time.time * 4f)) * 25f, Mathf.Cos(Time.time * 6f + Mathf.Sin(Time.time * 4f)) * 25f - 20f, 0);
                GameObject.Find("Dot").GetComponent<Image>().color = new Color(255, 255, 255, (tempo - Time.time)/2f + 2.5f);
                GameObject.Find("Start").GetComponent<Text>().color = new Color(255, 255, 255, (Time.time - tempo)/2f - 1.5f);
            }
        }
    }

    public void begin()
    {
        start = true;

        if (PlayerPrefs.GetInt("tutorial") == 1)
        {
            GameObject.Find("Mensagem").GetComponent<Mensagem>().show("Bem vindo ao tutorial!\nToque nos lugares coloridos, o seu objetivo é combinar as cartas para fazer produtos");
        }
        target.GetComponent<Canvas>().enabled = true;
        if(tag=="Canvas")
        {
            GetComponent<Canvas>().enabled = false;
        }
        tempo = Time.time;
        GameObject.Find("BeginS").GetComponent<AudioSource>().Play();
    }

    Color fade(Color c, float f)
    {
        c = new Color(c.r, c.g, c.b, f);
        return c;
    }
}
