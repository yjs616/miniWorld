using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class KitchenPlayerController : MonoBehaviour
{
    public float moveSpeed = 4.0f;
    public float turnSpeed = 7.0f;
    public float jumpForce = 4.0f;

    public CinemachineVirtualCamera playerCamera;
    public CinemachineVirtualCamera fullCamera;
    public ParticleSystem powerUpEffect;   //에너지 파티클 효과
    public AudioClip walkingSound; // 걷기 소리

    private Animator animator;
    private Rigidbody rb;
    private Vector3 movement;
    private KitchenUIDirector uiDirector;
    private AudioSource audioSource; // 오디오 소스

    private bool isJumping = false;   //플레이어가 점프 중인지 여부 판단
    private bool canMove = false;     //플레이어가 이동할 수 있는지 여부
    private bool canThrowBall = false; // 공 던질 수 있는 상태

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        uiDirector = FindObjectOfType<KitchenUIDirector>();
        audioSource = GetComponent<AudioSource>(); // 오디오 소스 초기화

        playerCamera.Priority = 10;
        fullCamera.Priority = 0;

        /* DishGame에서 돌아온 경우 점프력 증가 및 파티클 효과 재생
        if (PlayerPrefs.GetInt("FromDishGame", 0) == 1)
        {
            PlayerPrefs.SetInt("FromDishGame", 0);  //다시 0으로 설정
            Debug.Log("실행됨");
            PowerUpJump(100.0f, 5.0f);
        }*/
    }

    private void Update()
    {
        if (!canMove) return; // 이동 불가능 상태면 Update 종료

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movement = new Vector3(horizontal, 0, vertical).normalized;
        bool isWalking = movement != Vector3.zero;
        animator.SetBool("isWalking", isWalking);

        // 걷기 소리 재생 및 정지
        if (isWalking && !audioSource.isPlaying)
        {
            audioSource.clip = walkingSound;
            audioSource.loop = true;
            audioSource.Play();
        }
        else if (!isWalking && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        Vector3 moveDirection = movement * moveSpeed * Time.deltaTime;
        transform.position += moveDirection;

        if (isWalking)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            animator.SetTrigger("jump");
            Debug.Log("Jump force: " + jumpForce); // 디버그 메시지 추가
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true; // 점프 중으로 설정 (이중 점프 금지)
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 어떤 표면과 충돌하더라도 점프 상태 해제
        isJumping = false;
    }

    //점프력 올림
    public void PowerUpJump(float newJumpForce, float duration)
    {
        StartCoroutine(PowerUpJumpCoroutine(newJumpForce, duration));
    }

    private IEnumerator PowerUpJumpCoroutine(float newJumpForce, float duration)
    {
        powerUpEffect.Play();           //에너지 파티클 효과 재싱 
        jumpForce = newJumpForce;       //점프력 증가
        yield return new WaitForSeconds(duration);  //3초동안 파티클 유지
        powerUpEffect.Stop();                   //파티클 종료
    }

    //움직일 수 있는지 여부
    public void EnableMovement(bool enable)
    {
        canMove = enable;
    }

    // 공 던질 수 있는 상태 설정
    public void EnableThrowBall(bool enable)
    {
        canThrowBall = enable;
    }

    // 공 던질 수 있는 상태 반환
    public bool CanThrowBall()
    {
        return canThrowBall;
    }
}
