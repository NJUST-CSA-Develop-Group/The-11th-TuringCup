using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public GameObject infoPrefab;
    public Texture2D[] Avatars;
    public Texture2D[] MapImages;
    bool loaded = false;
    Transform[] _transform;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        Invoke("StartGame", 5.0f);
        transform.Find("Inside/MapInfo/MapImage").GetComponent<RawImage>().texture = MapImages[MatchManager.man.map_id];
        transform.Find("Inside/MapInfo/Text").GetComponent<Text>().text = MapDesp.Desp[MatchManager.man.map_id];
        float scale = Mathf.Min(Screen.width / 1920f, Screen.height / 1080f);
        transform.Find("Inside").localScale = new Vector3(scale, scale, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!loaded)
        {
            SetInfo();
            loaded = true;
        }
    }

    void SetInfo()
    {
        _transform = new Transform[4];
        foreach(GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            GameObject _gameObject = Instantiate(infoPrefab, transform);
            int index = p.GetComponent<PlayerScoreManager>().playerID - 1;
            _transform[index] = _gameObject.transform;
            _transform[index].localPosition += new Vector3(index * Screen.width / 4, 0, 0);
            _transform[index].localScale = new Vector3(Screen.width / 1920f, Screen.width / 1920f, 1f);
            _transform[index].Find("Avatar").GetComponent<RawImage>().texture = Avatars[index];
            _transform[index].Find("Name").GetComponent<Text>().text = p.GetComponent<TuringOperate>().AIScript.GetTeamName();
        }
    }

    void StartGame()
    {
        gameObject.SetActive(false);
        transform.parent.Find("Status/GameTimes").GetComponent<Animator>().SetBool("start", true);
        transform.parent.Find("Status/GodMode").GetComponent<Animator>().SetBool("visible", true);
        EventManager.Instance.PostNotification(EVENT_TYPE.GAME_START, this);
        GameObject.FindGameObjectWithTag("MainCamera").transform.parent.GetComponent<CameraMovement>().AllowMouse = true;
    }
}
