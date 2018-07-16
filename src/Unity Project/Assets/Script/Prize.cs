using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prize : MonoBehaviour {
    public GameObject prefab;
    public string team_name;
    public Texture avatarTexture;
    public Texture orderTexture;

    GameObject prefabInstantiate;
    UnityEngine.UI.RawImage avatar;
    UnityEngine.UI.RawImage order;
    // Use this for initialization
    void Start () {
        prefabInstantiate = Instantiate(prefab, transform);
        avatar = prefabInstantiate.transform.Find("avatar").GetComponent<UnityEngine.UI.RawImage>();
        order = prefabInstantiate.transform.Find("order").GetComponent<UnityEngine.UI.RawImage>();
        prefabInstantiate.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = team_name;
        avatar.texture = avatarTexture;
        order.texture = orderTexture;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
