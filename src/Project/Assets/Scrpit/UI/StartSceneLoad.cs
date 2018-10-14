using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneLoad : MonoBehaviour
{
    private string[] _childs =
    {
        "background",
        "logo",
        "buttonGroup0",
        "buttonGroup1",
        "over",
        "Return"
    };
    private string[] _mapchilds =
    {
        "maps/Test",
        "maps/Machine",
        "maps/Match"
    };

    // Use this for initialization
    void Start()
    {
        transform.Find("buttonGroup0").GetComponent<Animator>().SetInteger("pos", 1);
        foreach (string name in _childs)
        {
            transform.Find(name).localScale = new Vector3(Screen.height / 1080.0f, Screen.height / 1080.0f, 1);
        }
        foreach (string name in _mapchilds)
        {
            float scale = Mathf.Min(Screen.height / 1080.0f, Screen.width / 1920.0f);
            transform.Find(name).localScale = new Vector3(scale, scale, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
