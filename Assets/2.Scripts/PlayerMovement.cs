using UnityEngine;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 앞뒤 움직임의 속도
    public float rotateSpeed = 180f; // 좌우 회전 속도
    public float jumpForce = 300f;

    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디
    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터

    private bool isJump = false;

    private void Start()
    {
        // 사용할 컴포넌트들의 참조를 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    // FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    private void FixedUpdate()
    {
        // 물리 갱신 주기마다 움직임, 회전, 애니메이션 처리 실행
        Rotate();
        Move();
        Jump();

        // 입력값에 따라 애니메이터의 Move 파라미터값 변경
        if (Mathf.Abs(playerInput.move) > Mathf.Abs(playerInput.rotate))
            playerAnimator.SetFloat("Move", playerInput.move);
        else
            playerAnimator.SetFloat("Move", playerInput.rotate);
    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void Move()
    {
        // 상대적으로 이동할 거리 계산
        //Vector3 moveDistance = playerInput.move * transform.forward * moveSpeed * Time.deltaTime;
        Vector3 moveDistance = playerInput.move * transform.forward * moveSpeed * Time.deltaTime;
        Vector3 rotateDistance = playerInput.rotate * transform.right * moveSpeed * Time.deltaTime;
        // 게임 오브젝트 위치 변경
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
        playerRigidbody.MovePosition(playerRigidbody.position + rotateDistance);
    }

    // 입력값에 따라 캐릭터를 좌우로 회전
    private void Rotate()
    {
        // 카메라의 전방 벡터를 사용하여 플레이어 회전
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;  // y축 회전만 고려
        Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (isJump == false && playerInput.jump)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce);
            isJump = true;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Floor")
            isJump = false;
    }
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.collider.tag == "Floor")
    //        isJump = true;
    //}

}