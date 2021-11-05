using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectScript : MonoBehaviour
{
    public List<string> options;
    public int i;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMouseDown()
    {
        if (i<options.Count-1)
        {
            i++;
        } else
        {
            i = 0;
        }
        GetComponent<Text>().text = options[i];
    }
}
