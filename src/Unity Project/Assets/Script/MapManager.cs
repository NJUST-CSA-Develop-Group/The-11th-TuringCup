using System.Collections;
using System.Collections.Generic;
using System . IO;
using UnityEngine;

public class MapManager : MonoBehaviour {

    public List<string [ ]> m_ArrayData;

    public List<int[ ]> Map;    // 存储全局地图信息，左上为 (0, 0)
    public GameObject InstBoomCube;     // 可炸方块
    public GameObject InstUnboomCube;       // 不可炸方块

    // 地图坐标对与position的换算
    private Vector3 MapToPosition(int i, int j )
    {
        Vector3 vector3;
        vector3 . y = -0.5f;
        vector3 . x = i - 7;
        vector3 . z = 7 - j;
        return vector3;
    }

    // 加载文件，以字符形式存储在m_ArrayData中
    private void loadFile( string path, string fileName )
    {
        Debug . Log ( "inload" );
        m_ArrayData . Clear ( );
        Debug . Log ( "afterClear" );
        StreamReader sr = null;
        Debug . Log ( "beforetry" );
        try
        {
            sr = File . OpenText ( path + "//" + fileName );
        }
        catch
        {
            Debug . Log ( "The file could not be read!" );
        }
        string line;
        Debug . Log ( "loaded" );
        while ( ( line = sr . ReadLine ( ) ) != null )
        {
            m_ArrayData . Add ( line . Split ( ',' ) );
        }
        Debug . Log ( "added" );
        sr . Close ( );
        sr . Dispose ( );
    }

    // 以int返回指定位置的值
    public int getInt( int row, int col )
    {
        return int . Parse ( m_ArrayData [ row ] [ col ] );
    }

    // Use this for initialization
    void Start () {

        m_ArrayData = new List<string [ ]> ( );

        Debug . Log ( "start" );

        loadFile ( Application . dataPath + "/Maps", "01.csv" );
        Debug . Log ( "loadFile" );
        
        // 初始化创建方块对象
        for ( int i = 0; i < 2; i++ )        // 测试时只有两行，实际需要14
        {
            for ( int j = 0; j < 14; j++ )
            {
                Debug . Log ( getInt ( i, j ) );
                if (getInt(i, j) == 1 )      // 1表示可炸块
                {
                    GameObject . Instantiate ( InstBoomCube, MapToPosition( i, j ), gameObject . transform . rotation );
                    Debug . Log ( "BoomCube" );
                }
                else if ( getInt(i, j) == 2 )        // 2表示不可炸块
                {
                    GameObject . Instantiate ( InstUnboomCube, MapToPosition ( i, j ), gameObject . transform . rotation );
                    Debug . Log ( "unboomcube" );
                }
            }
        }
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
