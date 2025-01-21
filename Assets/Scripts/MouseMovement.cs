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
        // �����̶������Ĳ��Ҳ��ɼ�
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (InventorySystem.Instance.isOpen == false)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // ���Ͽ������¿�
            xRotation -= mouseY;

            // ��ֹ������ת
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // ���󿴡����ҿ�
            YRotation += mouseX;

            // Ӧ����ת
            transform.localRotation = Quaternion.Euler(xRotation, YRotation, 0f);
        }
    }
}
