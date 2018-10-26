using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class WWWFileLoad
{
#if UNITY_ANDROID
    public delegate void Callback(bool result, string err);
    public static IEnumerator LoadFile(string url, string aimPath, Callback callback)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        //Debug.Log(www.isDone);
        //string result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
        //Debug.Log(result);
        if (www.isHttpError || www.isHttpError)
        {
            callback(false, www.error);
        }
        else
        {
            File.WriteAllBytes(aimPath, www.downloadHandler.data);
            callback(true, null);
        }
        yield return null;
    }
#endif
}
