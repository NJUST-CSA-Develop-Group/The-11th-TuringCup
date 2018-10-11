using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData data)
    {
        transform.Find("selector").gameObject.SetActive(true);
        transform.Find("Text").GetComponent<Text>().color = Color.black;
    }

    public void OnPointerExit(PointerEventData data)
    {
        transform.Find("selector").gameObject.SetActive(false);
        transform.Find("Text").GetComponent<Text>().color = Color.white;
    }
}
