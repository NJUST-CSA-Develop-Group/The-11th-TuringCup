using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    public void OnPointerExit(PointerEventData data)
    {
        transform.Find("selector").gameObject.SetActive(false);
    }
}
