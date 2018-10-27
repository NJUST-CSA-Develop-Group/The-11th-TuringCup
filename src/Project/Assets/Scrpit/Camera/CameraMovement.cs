using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    #region 相机移动参数
    public float moveSpeed = 10.0f; //一般移动速度
    public float rotateSpeed = 90.0f; //旋转速度
    public float shiftRate = 3.0f; //加速比率
    public float minDistance = 0.5f; //允许最小距离
    #endregion

    #region 各个方向的速度分量 用于移动计算
    private Vector3 direction = Vector3.zero;
    private Vector3 moveZ;
    private Vector3 moveX;
    private Vector3 moveY;
    #endregion

    private Transform tourCamera; //当前摄像机的位置数据

    private RaycastHit objectHit;

    public bool AllowMouse = false;
    private bool toggle = true;
    private bool toggleMode = false;

    void Start()
    {
        if (tourCamera == null) tourCamera = gameObject.transform;
    }

    void LateUpdate()
    {
#if !UNITY_ANDROID
        GetDirection();
#endif

        // 防止穿透物体
        /*while (Physics.Raycast(tourCamera.position, direction, out objectHit, minDistance))
        {
            float angle = Vector3.Angle(direction, objectHit.normal);
            float magnitude = Vector3.Magnitude(direction) * Mathf.Cos(Mathf.Deg2Rad * (180 - angle));
            direction += objectHit.normal * magnitude;
        }*/
        Vector3 aim = tourCamera.position + direction * moveSpeed * Time.deltaTime;
        aim.x = Mathf.Max(6.5f - 10, Mathf.Min(6.5f + 10, aim.x));
        aim.z = Mathf.Max(6.5f - 10, Mathf.Min(6.5f + 10, aim.z));
        aim.y = Mathf.Max(2f, Mathf.Min(15f, aim.y));
        tourCamera.position = aim;
        //tourCamera.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    private void GetDirection()
    {
#region 加速移动 按下shift时加速
        if (Input.GetKeyDown(KeyCode.LeftShift)) moveSpeed *= shiftRate;
        if (Input.GetKeyUp(KeyCode.LeftShift)) moveSpeed /= shiftRate;
#endregion

#region 键盘移动
        //复位 消除上一帧状态
        moveZ = Vector3.zero;
        moveX = Vector3.zero;
        moveY = Vector3.zero; 

        //获取键盘输入
        if (Input.GetKey(KeyCode.W)) moveZ = tourCamera.forward;
        if (Input.GetKey(KeyCode.S)) moveZ = -tourCamera.forward;
        if (Input.GetKey(KeyCode.A)) moveX = -tourCamera.right;
        if (Input.GetKey(KeyCode.D)) moveX = tourCamera.right;
        if (Input.GetKey(KeyCode.Q)) moveY = Vector3.up;
        if (Input.GetKey(KeyCode.E)) moveY = Vector3.down;

        direction = moveZ + moveX + moveY; //整合最终移动方向
        if (GetComponent<CameraControl>().Aim != null) //仅可在全局视角下移动
        {
            direction = new Vector3(0, 0, 0);
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            toggleMode = true;
        }
        if (toggleMode && Input.GetKeyUp(KeyCode.Tab))
        {
            toggleMode = false;
            toggle = !toggle;
        }

        #region 鼠标旋转
        if (AllowMouse && toggle)//(Input.GetMouseButton(1))
        {
            //相机朝向转动
            tourCamera.RotateAround(tourCamera.position, Vector3.up, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime);
            tourCamera.RotateAround(tourCamera.position, tourCamera.right, -Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime);
        }
#endregion
    }

    /// <summary>
    /// 计算一个Vector3绕旋转中心旋转指定角度后 得到的新的向量
    /// </summary>
    /// <param name="source">被旋转的向量</param>
    /// <param name="axis">旋转轴</param>
    /// <param name="angle">旋转角度</param>
    /// <returns>旋转后得到的新的Vector3</returns>
    public Vector3 V3RotateAround(Vector3 source, Vector3 axis, float angle)
    {
        Quaternion q = Quaternion.AngleAxis(angle, axis); //旋转系数
        return q * source; //返回目标点
    }
}
