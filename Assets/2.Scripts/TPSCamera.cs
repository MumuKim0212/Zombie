using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public Transform target;  // �÷��̾��� Transform
    public float distance = 5f;  // ī�޶�� �÷��̾� ������ �Ÿ�
    public float height = 2f;  // ī�޶��� ����
    public float rotationSpeed = 5f;  // ī�޶� ȸ�� �ӵ�

    private float currentRotationX = 0f;
    private float currentRotationY = 0f;

    void LateUpdate()
    {
        if (target == null) return;

        // ���콺 �Է� �ޱ�
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        // ���� ȸ�� ���� ������Ʈ
        currentRotationX -= mouseY;
        currentRotationY += mouseX;

        // ���� ȸ�� ���� ���� (-80������ 80�� ����)
        currentRotationX = Mathf.Clamp(currentRotationX, -80f, 80f);

        // ī�޶� ��ġ �� ȸ�� ���
        Quaternion rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0);
        Vector3 negDistance = new Vector3(0f, 0f, -distance);
        Vector3 position = rotation * negDistance + target.position + Vector3.up * height;

        // ī�޶� ��ġ �� ȸ�� ����
        transform.rotation = rotation;
        transform.position = position;

        // ī�޶� �׻� �÷��̾ �ٶ󺸵��� ����
        transform.LookAt(target.position + Vector3.up * height);
    }
}