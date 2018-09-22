using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene_Tab : MonoBehaviour
{
    public string DefaultState;

    // Use this for initialization
    void Start()
    {
        GetComponent<Animator>().SetTrigger(DefaultState);
        Invoke("ResetTrigger", 0.2f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ResetTrigger()
    {
        GetComponent<Animator>().ResetTrigger(DefaultState);
    }

    public void SetVisible()
    {
        transform.Find("Tab").gameObject.SetActive(true);
    }
}
