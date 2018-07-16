using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {
    public GameObject prefab;
    public Material deadMaterial;
    public string team_name;
    public Texture avatarTexture;
    public Texture skill1Texture;
    public Texture skill2Texture;
    public Texture skill3Texture;

    GameObject prefabInstantiate;
    UnityEngine.UI.RawImage avatar;
    UnityEngine.UI.RawImage skill1;
    UnityEngine.UI.RawImage skill2;
    UnityEngine.UI.RawImage skill3;
    Transform blood;
    // Use this for initialization
    void Start () {
        prefabInstantiate = Instantiate(prefab, transform);
        prefabInstantiate.transform.localScale = new Vector3(Screen.width / 1920f, Screen.width / 1920f, 1f);
        avatar = prefabInstantiate.transform.Find("avatar").GetComponent<UnityEngine.UI.RawImage>();
        avatar.texture = avatarTexture;
        Transform skills = prefabInstantiate.transform.Find("skill");
        skill1 = skills.Find("skill1").GetComponent<UnityEngine.UI.RawImage>();
        skill2 = skills.Find("skill2").GetComponent<UnityEngine.UI.RawImage>();
        skill3 = skills.Find("skill3").GetComponent<UnityEngine.UI.RawImage>();
        skill1.texture = skill1Texture;
        skill2.texture = skill2Texture;
        skill3.texture = skill3Texture;
        blood = prefabInstantiate.transform.Find("bloodBar").Find("blood");
        prefabInstantiate.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = team_name;
        skill1.enabled = false;
        skill2.enabled = false;
        skill3.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void SetHealth(int health)
    {
        blood.localScale = new Vector3(health / 100f, 1f, 1f);
    }

    public void Die()
    {
        avatar.material = new Material(deadMaterial);
    }

    public void SetSkill(int index,bool type)
    {
        switch (index)
        {
            case 1:
                skill1.enabled = type;
                break;
            case 2:
                skill2.enabled = type;
                break;
            case 3:
                skill3.enabled = type;
                break;
        }
    }
}
