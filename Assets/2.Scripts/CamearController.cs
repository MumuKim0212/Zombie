using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;  // ī�޶� ���� ��� (�÷��̾�)
    public float smoothSpeed = 0.125f;  // ī�޶� �������� �ε巯��
    public Vector3 offset;  // ī�޶�� ��� ������ �Ÿ�
    public Joystick joystick;  // ���̽�ƽ ����
    public float rotationSpeed = 3f;  // ȸ�� �ӵ�

    public float minRotate = -60.0f;
    public float maxRotate = 60.0f;

    private float currentRotationX = 0f;
    private float currentRotationY = 0f;

    private void LateUpdate()
    {
        if (target == null || joystick == null)
            return;

        // ���̽�ƽ �Է¿� ���� ȸ�� ���� ������Ʈ
        currentRotationX += joystick.Horizontal * rotationSpeed;
        currentRotationY += joystick.Vertical * rotationSpeed;
        currentRotationY = Mathf.Clamp(currentRotationY, minRotate, maxRotate);  // ���� ȸ�� ����

        // ȸ�� ����
        Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // �ε巯�� �̵�
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // ī�޶� �׻� ����� �ٶ󺸵��� ��
        transform.LookAt(target);
    }
}