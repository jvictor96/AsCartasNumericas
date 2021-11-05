using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PlayerHand : MonoBehaviour
{
    public int i = 0, n;
    float time;
    public float fade;
    public bool select = false, done;
    GameObject icon;
    public string poder;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = GameObject.Find("Cartas").transform.GetChild(PlayerPrefs.GetInt("skin")).GetComponent<Image>().sprite;
        transform.GetChild(0).GetComponent<Skin>().baseColor = GameObject.Find("Cartas").transform.GetChild(PlayerPrefs.GetInt("skin")).GetComponent<Skin>().baseColor;
        transform.GetChild(0).GetComponent<Skin>().metal = GameObject.Find("Cartas").transform.GetChild(PlayerPrefs.GetInt("skin")).GetComponent<Skin>().metal;
    }

    // Update is called once per frame
    void Update()
    {
        float r = GameObject.Find("TableTop").GetComponent<UIControl>().reaction;
        if(i>=0)
        {
            transform.GetChild(1).GetComponent<Text>().text = i.ToString();
            transform.GetChild(2).GetComponent<Text>().text = i.ToString();
        } else
        {
            transform.GetChild(1).GetComponent<Text>().text = poder;
            transform.GetChild(2).GetComponent<Text>().text = poder;
        }
        if (done)
        {
            transform.GetChild(1).GetComponent<Text>().text = "X";
            transform.GetChild(2).GetComponent<Text>().text = "X";
        }
        if (select)
        {
            transform.GetChild(0).GetComponent<Image>().color = Color.HSVToRGB((Mathf.Sin(time)+1)/2f,0.7f,1);
            transform.localPosition = new Vector3(transform.localPosition.x, -240, 0); 
        }
        else
        {
            transform.GetChild(0).GetComponent<Image>().color = transform.GetChild(0).GetComponent<Skin>().baseColor;
            transform.localPosition = new Vector3(transform.localPosition.x, -250, 0);
        }
        if (r > 1)
        {
            transform.GetChild(0).GetComponent<Image>().color = new Color(0.5f * (Mathf.Sin(r * 20) + 1) / 2, 0.8f * (Mathf.Sin(r * 20) + 1) / 2, 1f);
        }
        else if (r < -1)
        {
            transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 0.8f * (Mathf.Sin(r * 20) + 1) / 2, 0.5f * (Mathf.Sin(r * 20) + 1) / 2);
        }

        if (GameObject.Find("TableTop").GetComponent<Tutorial>().target==n)
        {
            transform.GetChild(0).GetComponent<Image>().color = Color.HSVToRGB(Mathf.Cos(Time.frameCount / 250f) / 2f + 0.5f, 1, 1f);
        }

        fadeIn();
        UmaPirueta();
    }

    void UmaPirueta()
    {
        if (transform.localRotation.x != 0)
        {
            transform.Rotate(new Vector3(Time.deltaTime * 350, Time.deltaTime * 350, Time.deltaTime * 350));
            if (transform.localRotation.y < 0 && -transform.localRotation.y < Time.deltaTime * 350)
            {
                transform.SetPositionAndRotation(transform.position, new Quaternion(0, 0, 0, 0));
            }
        }
    }


    public void smartClick()
    {
        GameObject tt = GameObject.Find("TableTop");
        if (tt.GetComponent<Go>().tutorial)
        {
            tt.GetComponent<Tutorial>().play(name);
            GameObject.Find("CartaS").GetComponent<AudioSource>().Play();
        }
        else if (tt.GetComponent<Go>().survival && tt.GetComponent<SurvivalEngine>().selec >= 3&&!select&& tt.GetComponent<Go>().venceu) { 
        } else { 
            click();
            GameObject.Find("CartaS").GetComponent<AudioSource>().Play();
        }
    }
    public void click()
    {
        if (select)
        {
            select = false;
        }
        else if (!done & GameObject.Find("TableTop").GetComponent<UIControl>().vidas > 0)
        {
            select = true;
            time = Time.time;
        }
    }

    public void setup()
    {
        Destroy(transform.GetChild(3).gameObject);
        if(i > 100)
        {
            icon = Instantiate(GameObject.Find("Estrela"));
        } else
        {
            icon = Instantiate(GameObject.Find("Icones").transform.GetChild(PlayerPrefs.GetInt("icone")).gameObject);
        }
        icon.transform.SetParent(gameObject.transform);
        icon.transform.localScale = new Vector3(1,1,1);
        icon.transform.localPosition= new Vector3(0,0,0);
    }

    public void fadeIn()
    {
        Color c = transform.GetChild(0).GetComponent<Image>().color;
        c = new Color(c.r, c.g, c.b, fade);
        transform.GetChild(0).GetComponent<Image>().color = c;
    }
}