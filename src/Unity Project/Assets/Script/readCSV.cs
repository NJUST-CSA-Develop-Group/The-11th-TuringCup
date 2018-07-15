using System . Collections;
using System . Collections . Generic;
using UnityEngine;
using System . IO;
using System;


// 该类用于处理CSV文件的读取
public class readCSV  {
    static readCSV readcsv; 
    public List<string [ ]> m_ArrayData;

    public readCSV()
    {
        m_ArrayData = new List<string [ ]> ( );
    }

    // 以String返回指定位置的值
    public string getString ( int row, int col )
    {
        return m_ArrayData [ row ] [ col ];
    }

    // 以int返回指定位置的值
    public int getInt( int row, int col )
    {
        return int . Parse ( m_ArrayData [ row ] [ col ] );
    }

    // 加载文件，以字符形式存储在m_ArrayData中
    public void loadFile( string path, string fileName )
    {
        m_ArrayData . Clear ( );
        StreamReader sr = null;
        try
        {
            sr = File . OpenText ( path + "//" + fileName );
        }
        catch
        {
            Debug . Log ( "The file could not be read!" );
        }
        string line;
        while ( ( line = sr . ReadLine ( ) ) != null )
        {
            m_ArrayData . Add ( line . Split ( ',' ) );
        }
        sr . Close ( );
        sr . Dispose ( );
    }

}
