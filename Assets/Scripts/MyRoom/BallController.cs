using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement; // 씬 관리 네임스페이스 추가

public class BallController : MonoBehaviour
{
    public Transform ballPosition;
    public CinemachineVirtualCamera ballCamera;
    public CinemachineVirtualCamera playerCamera;
    public float throwForce = 30f;
    public GameObject door;
    public ParticleSystem collisionEffect; // 충돌 파티클
    
    public AudioSource audioSource;       // 오디오 소스 추가
    public AudioClip doorCollisionSound;  // 문 충돌 효과음을 위한 오디오 클립 추가

    private Rigidbody rb;
    private bool canThrowBall = false; // 공을 던질 수 있는 상태
    private bool hitDoor = false; // 공이 문에 맞았는지 여부
    private bool isFirst = true;
    private UIDirector uiDirector;

    private PlayerController playerController; // PlayerController 참조

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballCamera.Priority = 0; // ballCamera 우선순위 0
        uiDirector = FindObjectOfType<UIDirector>(); // UIDirector 참조
        playerController = FindObjectOfType<PlayerController>(); // PlayerController 참조

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // 오디오 소스가 없으면 추가
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hitDoor)
        {
            if (playerController.CanThrowBall()) // 공 던질 수 있는 상태인지 확인
            {
                MoveToBallPosition();
                SwitchToBallCamera();
                isFirst = false;
            }
        }
        else if (collision.gameObject.CompareTag("Door"))
        {
            hitDoor = true;
            PlayDoorCollisionSound(); // 문과 충돌 시 효과음 재생
            StartCoroutine(HandleDoorCollision(collision.gameObject.GetComponent<DoorController>()));
        }
        else
        {
            if (!isFirst && !hitDoor)
            {
                canThrowBall = true;
                MoveToBallPosition();
            }
        }
    }

    // 공 위치 옮기기
    void MoveToBallPosition()
    {
        transform.position = ballPosition.position;
        transform.rotation = ballPosition.rotation;
        rb.useGravity = false; // 중력 영향 X
        rb.velocity = Vector3.zero;
    }

    // 카메라 전환
    void SwitchToBallCamera()
    {
        ballCamera.Priority = 10; // BallCamera 우선순위 10 (카메라 전환)
        playerCamera.Priority = 0;
        canThrowBall = true; // 공 던질 수 있는 상태
    }

    IEnumerator HandleDoorCollision(DoorController doorController)
    {
        yield return StartCoroutine(doorController.RotateDoor());
        uiDirector.ShowPlayerMessage("나이스 명중! \n시간이 얼마 없어. 빨리가자.", 2.0f);
        yield return new WaitForSeconds(3.0f);
        // SavePlayerData();
        SceneManager.LoadScene("DiningRoom");
    }

    private void Update()
    {
        if (canThrowBall && Input.GetMouseButtonDown(0))
        {
            ShootBall();
        }
    }

    void ShootBall()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 directionToMouse = ray.direction;
        rb.useGravity = true;
        rb.AddForce(directionToMouse * throwForce, ForceMode.Impulse);
        canThrowBall = false;
    }

    private void PlayDoorCollisionSound()
    {
        if (audioSource != null && doorCollisionSound != null)
        {
            audioSource.PlayOneShot(doorCollisionSound);
        }
    }

    /*
    private void SavePlayerData()
    {
        PlayerPrefs.SetFloat("PlayerJumpForce", PlayerController.Instance.jumpForce);
        PlayerPrefs.Save();
    }*/
}
