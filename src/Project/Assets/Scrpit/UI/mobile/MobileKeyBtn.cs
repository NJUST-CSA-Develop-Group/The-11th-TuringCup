using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MobileKeyBtn : MonoBehaviour
{
    public string KeyName;
    private CrossPlatformInputManager.VirtualButton virtualButton;

    void OnEnable()
    {
        virtualButton = new CrossPlatformInputManager.VirtualButton(KeyName);
        CrossPlatformInputManager.RegisterVirtualButton(virtualButton);
    }

    void OnDisable()
    {
        virtualButton.Remove();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetKeyDown()
    {
        virtualButton.Pressed();
    }

    public void SetKeyUp()
    {
        virtualButton.Released();
    }
}
