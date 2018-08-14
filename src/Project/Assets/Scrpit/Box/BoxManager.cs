using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour, TListener {

    private int BoxType;//方块类型 用于判别可炸和不可炸
    private Transform trans;//方块位置 用于获取方块类型
    private GameObject Global;//目前MapManager脚本绑定在Floor上 后期可考虑改成全局单例类
    private MapManager Map;//MapManager脚本引用

    private ParticleSystem ExplosionSmoke;
    private void Start()
    {
        trans = GetComponent<Transform>();
        Global = GameObject.FindGameObjectWithTag("Global");//此处Tag后期注意修改
        Map = Global.GetComponent<MapManager>(); //获取MapManager的引用
        setBoxType(Map);

        if (BoxType == 1)
        {
            EventManager.Instance.AddListener(EVENT_TYPE.BOMB_EXPLODE, this); //只对可炸方块注册监听爆炸的监听器
            ExplosionSmoke = GetComponent<ParticleSystem>();
        }

    }
    /*
     * 获取当前管理方块的类型
     * 具体方法为降级当前位置类型至int
     * 传递至MapManager.GetBoxType（地图信息在Awake阶段加载）
     */
    private void setBoxType(MapManager Map)
    {
       
        BoxType = Map.GetBoxType((int)trans.position.x, (int)trans.position.z);
    }

    //方块爆炸函数
    //当方块处于爆炸区域内
    private void InExplode()
    {
        GetComponent<MeshRenderer>().enabled = false;
        ExplosionSmoke.Play();
        GetComponent<BoxCollider>().enabled = false;
        Destroy(gameObject,2.5f);

         
    }

    public bool OnEvent(EVENT_TYPE Event_Type, Component Sender, Object param, Dictionary<string, object> value)
    {
        switch (Event_Type) //可以接收多种事件
        {
            case (EVENT_TYPE.BOMB_EXPLODE): //指定事件发生时
                InExplode(); //执行爆炸函数
                //发送方块被摧毁事件
                EventManager.Instance.PostNotification(EVENT_TYPE.BOX_DESTROY, this, null, value);
                Dictionary<string, object> TempDic = new Dictionary<string, object>();
                TempDic.Add("MapCol", (int)transform.position.x);
                TempDic.Add("MapRow", (int)transform.position.z);
                TempDic.Add("MapType", 0);
                EventManager.Instance.PostNotification(EVENT_TYPE.MAP_UPDATE_INFO, this, null, TempDic);
                TempDic.Clear();
                return true; 
            default: return false;
        }
    }

    public Object getGameObject()
    {
        return gameObject;
    }


}
