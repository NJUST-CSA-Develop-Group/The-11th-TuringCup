// Copyright (c) CSA, NJUST. All rights reserved.
// Licensed under the Mozilla license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect : MonoBehaviour//背景虚化特效
{
    [Range(0, 10)]
    public int iterations = 3;

    [Range(0.0f, 1.0f)]
    public float blurSpread = 0.6f;

    public Shader blurShader;

    private Material mtrl;

    protected void Start()
    {
        mtrl = new Material(blurShader);
    }
    
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        RenderTexture buffer = RenderTexture.GetTemporary(source.width /4, source.height /4, 0);
        mtrl.SetFloat("Offset", 1.0f);
        Graphics.Blit(source, buffer,mtrl);
        for (int i = 0; i < iterations; i++)
        {
            RenderTexture buffer2 = RenderTexture.GetTemporary(source.width /4, source.height/4, 0);
            float off = 0.5f + i * blurSpread;
            mtrl.SetFloat("_Offset", off);
            Graphics.Blit(buffer, buffer2, mtrl);
            RenderTexture.ReleaseTemporary(buffer);
            buffer = buffer2;
        }
        Graphics.Blit(buffer, destination);
        RenderTexture.ReleaseTemporary(buffer);
    }
}
