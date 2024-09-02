using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;  // 카메라가 따라갈 대상 (플레이어)
    public float smoothSpeed = 0.125f;  // 카메라 움직임의 부드러움
    public Vector3 offset;  // 카메라와 대상 사이의 거리
    public Joystick joystick;  // 조이스틱 참조
    public float rotationSpeed = 3f;  // 회전 속도

    public float minRotate = -60.0f;
    public float maxRotate = 60.0f;

    private float currentRotationX = 0f;
    private float currentRotationY = 0f;

    private void LateUpdate()
    {
        if (target == null || joystick == null)
            return;

        // 조이스틱 입력에 따라 회전 각도 업데이트
        currentRotationX += joystick.Horizontal * rotationSpeed;
        currentRotationY += joystick.Vertical * rotationSpeed;
        currentRotationY = Mathf.Clamp(currentRotationY, minRotate, maxRotate);  // 수직 회전 제한

        // 회전 적용
        Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // 부드러운 이동
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // 카메라가 항상 대상을 바라보도록 함
        transform.LookAt(target);
    }
}