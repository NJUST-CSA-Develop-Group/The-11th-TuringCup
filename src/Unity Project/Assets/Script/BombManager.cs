using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour {

    float tempTime = 0.00f;
    // 用于调整炸弹颜色的计时器
    float boomTime = 1.00f;
    // 用于爆炸倒计时的定时器

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // 炸弹爆炸倒计时，1s
        if ( boomTime > 0 )
            boomTime -= 0.01f;
        else
            OnBooming ( );

        // 设置炸弹闪烁，在颜色计时器的两个区间分别渲染不同颜色
        if ( tempTime < 1 )
        {
            tempTime += 0.05f;
            this . gameObject . GetComponent<Renderer> ( ) . material . SetColor ( "_EmissionColor", Color . HSVToRGB ( 0, 0, 0 ) );
            RendererExtensions . UpdateGIMaterials ( this . gameObject . GetComponent<Renderer> ( ) );
        }
        else if (tempTime < 2)
        {
            tempTime += 0.05f;
            this . gameObject . GetComponent<Renderer> ( ) . material . SetColor ( "_EmissionColor", Color . HSVToRGB ( 1, 1, 1 ) );
            RendererExtensions . UpdateGIMaterials ( this . gameObject . GetComponent<Renderer> ( ) );
        }
        else
            tempTime = 0;
        
    }

    // 测试执行爆炸逻辑状态函数
    private void OnMouseUp()
    {
        float radius = 0.7f;
        // 爆炸半径
        Vector3 explosionPos = transform . position;
        // 爆炸的位置
        Collider [ ] colliders = Physics . OverlapSphere ( explosionPos, radius );
        // 在爆炸范围内的对象

        // 遍历并调用被炸对象的
        foreach(Collider hit in colliders )
        {
            hit . SendMessage ( "OnBoom" );
            Destroy ( gameObject );
        }
    }

    // 执行爆炸逻辑状态函数
    void OnBooming()
    {
        float radius = 0.7f;
        // 爆炸半径
        Vector3 explosionPos = transform . position;
        // 爆炸的位置
        Collider [ ] colliders = Physics . OverlapSphere ( explosionPos, radius );
        // 在爆炸范围内的对象

        //遍历并调用被炸对象的
        foreach ( Collider hit in colliders )
        {
            hit . SendMessage ( "OnBoom" );
            Destroy ( gameObject );
        }
    }

}
