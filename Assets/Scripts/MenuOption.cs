using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOption : MonoBehaviour
{
    public bool ready = true, on = false, checkbox = false;
    public Color offColor;
    int t0;
    // Start is called before the first frame update
    void Start()
    {
        t0 = GetComponent<Text>().fontSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
            GetComponent<Text>().color = offColor;
        }
        else
        {
            GetComponent<Text>().color = new Color(0.5f, 0.5f, 0.5f);
        }
        if (on)
        {
            GetComponent<Text>().color = Color.HSVToRGB(Mathf.Cos(Time.frameCount / 250f) / 2f + 0.5f, 1, 1f);
        }
    }

    public bool IsOver()
    {
        if (Mathf.Abs(GetComponent<RectTransform>().position.x-Input.mousePosition.x)< GetComponent<RectTransform>().rect.width/2)
        {
            if (Mathf.Abs(GetComponent<RectTransform>().position.y - Input.mousePosition.y) < GetComponent<RectTransform>().rect.height / 2)
            {
                return true;
            }
        }
        return false;
    }

    public void change()
    {
        if (on)
        {
            on = false;
        } else
        {
            on = true;
        }
    }
}
