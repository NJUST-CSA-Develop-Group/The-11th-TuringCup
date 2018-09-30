using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneLoad : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GetComponent<Animator>().SetInteger("pos", 1);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
