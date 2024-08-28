using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public Transform target;  // 플레이어의 Transform
    public float distance = 5f;  // 카메라와 플레이어 사이의 거리
    public float height = 2f;  // 카메라의 높이
    public float rotationSpeed = 5f;  // 카메라 회전 속도

    private float currentRotationX = 0f;
    private float currentRotationY = 0f;

    void LateUpdate()
    {
        if (target == null) return;

        // 마우스 입력 받기
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        // 현재 회전 각도 업데이트
        currentRotationX -= mouseY;
        currentRotationY += mouseX;

        // 수직 회전 각도 제한 (-80도에서 80도 사이)
        currentRotationX = Mathf.Clamp(currentRotationX, -80f, 80f);

        // 카메라 위치 및 회전 계산
        Quaternion rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0);
        Vector3 negDistance = new Vector3(0f, 0f, -distance);
        Vector3 position = rotation * negDistance + target.position + Vector3.up * height;

        // 카메라 위치 및 회전 적용
        transform.rotation = rotation;
        transform.position = position;

        // 카메라가 항상 플레이어를 바라보도록 설정
        transform.LookAt(target.position + Vector3.up * height);
    }
}