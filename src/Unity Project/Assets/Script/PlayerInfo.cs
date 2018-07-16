using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

    public GameObject prefab;
    public Texture avatar;
    public string team_name;

    GameObject prefabInstantiate;// 射击模型实例
    UnityEngine.UI.RawImage m_avatar;
    UnityEngine.UI.Text m_team_name;
	// Use this for initialization
	void Start () {
        prefabInstantiate = Instantiate(prefab, transform);// 实例化预制
        m_avatar = prefabInstantiate.transform.Find("avatar").GetComponent<UnityEngine.UI.RawImage>();
        m_team_name = prefabInstantiate.transform.Find("Text").GetComponent<UnityEngine.UI.Text>();
        SetInfo();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetInfo()
    {
        m_avatar.texture = avatar;
        m_team_name.text = team_name;
    }
}
