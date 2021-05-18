using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SendData : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI input;

    [SerializeField]
    GameObject MessagePrefab;

    [SerializeField]
    GameObject Content;
    RectTransform con;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnSend);
       con =  Content.GetComponent<RectTransform>();
        Debug.Log(Content.GetComponent<RectTransform>().rect.height);
        
    }

    int x = 350;
    int i = 0;
     void  OnSend()
    {
        i++;
        GameObject message = Instantiate(MessagePrefab,Content.transform);
        con.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, con.rect.height + 350);
        con.position += new Vector3(0,175,0);
        message.transform.position -= new Vector3(0, 350*i, 0);
        Content.transform.position += new Vector3(0, 350, 0);

        // = rectTr.rect;
      
      //  rect.height = 32f;
     //   Content.GetComponent<RectTransform>().rect.Set(rect.rect.x, rect.rect.y, rect.rect.width, 300);
        message.GetComponentInChildren<TextMeshProUGUI>().text = input.text;
        input.text = "";


    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(input. text);
    }
}
