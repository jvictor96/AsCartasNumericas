using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skin : MonoBehaviour
{
    public Color baseColor;
    public bool metal;
    GameObject b1, b2;
    float cd1=0, cd2=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(metal)
        {
            cd1 -= Time.deltaTime;
            cd2 -= Time.deltaTime;
            if (cd1 < 0)
            {
                if (b1) { Destroy(b1); }
                cd1 = 2;
                b1 = Instantiate(GameObject.Find("Brilho"));
                b1.transform.SetParent(transform);
                b1.transform.localPosition = new Vector3(Random.Range(-40,40), Random.Range(-60, 60), 0);
            }
            if (cd2 < 0)
            {
                if (b2) { Destroy(b2); }
                cd2 = 3;
                b2 = Instantiate(GameObject.Find("Brilho"));
                b2.transform.SetParent(transform);
                b2.transform.localPosition = new Vector3(Random.Range(-40, 40), Random.Range(-60, 60), 0);
            }

            if(b1)
            {
                b1.GetComponent<Image>().color = Clarify(baseColor, cd1);
                b1.transform.position += new Vector3(0, Time.deltaTime * 15f, 0);
                b1.transform.Rotate(0, 0, Time.deltaTime * 30);
            }

            b2.GetComponent<Image>().color = Clarify(baseColor, cd2 - 1f);
            b2.transform.position += new Vector3(0, Time.deltaTime * 15f, 0);
            b2.transform.Rotate(0, 0, Time.deltaTime * 30);

        }
    }
    Color fadeIn(Color c, float fade)
    {
        c = new Color(c.r, c.g, c.b, fade);
        return c;
    }
    Color Clarify(Color c, float fade)
    {
        c = new Color(Mathf.Sqrt(c.r), Mathf.Sqrt(c.g), Mathf.Sqrt(c.b), fade);
        return c;
    }
}
