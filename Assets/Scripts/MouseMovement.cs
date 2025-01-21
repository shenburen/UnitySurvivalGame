using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    float xRotation = 0f;
    float YRotation = 0f;

    void Start()
    {
        // 将鼠标固定在中心并且不可见
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (InventorySystem.Instance.isOpen == false)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // 向上看、向下看
            xRotation -= mouseY;

            // 防止过度旋转
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // 向左看、向右看
            YRotation += mouseX;

            // 应用旋转
            transform.localRotation = Quaternion.Euler(xRotation, YRotation, 0f);
        }
    }
}
