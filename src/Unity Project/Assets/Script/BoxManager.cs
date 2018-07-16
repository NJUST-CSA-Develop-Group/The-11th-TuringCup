using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour {

    public GameObject boomPS;
    public ParticleSystem particleSystem;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnBoom()
    {
        Vector3 v = new Vector3 ( );
        boomPS . GetComponent<Transform> ( ) . position = gameObject . GetComponent<Transform> ( ) . position;
        particleSystem . Play ( );
        BoxManager . Destroy ( gameObject );
       
    }
}
