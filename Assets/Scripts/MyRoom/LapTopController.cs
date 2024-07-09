using System.Collections;
using UnityEngine;
using Cinemachine;

public class LapTopController : MonoBehaviour
{
    public CinemachineVirtualCamera laptopCamera;
    public CinemachineVirtualCamera playerCamera;

    public UIDirector uiDirector;
    private bool isShowingHint = false;

    public AudioSource audioSource;  // 오디오 소스를 추가
    public AudioClip collisionSound; // 충돌 효과음을 위한 오디오 클립을 추가

    void Start()
    {
        // 초기 카메라 설정: PlayerCamera를 기본으로 활성화
        playerCamera.Priority = 10;
        laptopCamera.Priority = 0;

        uiDirector = FindObjectOfType<UIDirector>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();  // 오디오 소스가 없으면 추가
        }
        audioSource.clip = collisionSound;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isShowingHint)
        {
            PlayCollisionSound();
            StartCoroutine(ShowLaptopHint());
        }
    }

    private IEnumerator ShowLaptopHint()
    {
        isShowingHint = true;           // 한번만 보기 허용

        // 카메라 전환
        laptopCamera.Priority = 10;
        playerCamera.Priority = 0;

        yield return new WaitForSeconds(2f);

        // 노트북 메시지 표시
        uiDirector.ShowInfoMessage("[메모] 약은 항상 서랍장에", 2f);

        // 일정 시간 대기
        yield return new WaitForSeconds(2f);

        // 캐릭터 대화 메시지 표시
        uiDirector.ShowPlayerMessage("맞다 서랍장 위에 올려둔 것 같은데!", 2f);     // 띄어지는 시간

        // 일정 시간 대기 (다음을 위한 대기 시간)
        yield return new WaitForSeconds(1f);

        // 다시 카메라 전환
        laptopCamera.Priority = 0;
        playerCamera.Priority = 10;

        // isShowingHint = false;
    }

    private void PlayCollisionSound()
    {
        if (audioSource != null && collisionSound != null)
        {
            audioSource.Play();
        }
    }
}
