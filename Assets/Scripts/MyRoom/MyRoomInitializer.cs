using System.Collections;
using UnityEngine;
using TMPro;

public class GameStart : MonoBehaviour
{
    public GameObject player;
    public float shrinkDuration = 2.0f; // 플레이어가 작아지는 데 걸리는 시간
    private Vector3 originalScale;
    private Vector3 targetScale = new Vector3(0.7f, 0.7f, 0.7f); // 목표 크기

    private UIDirector uiDirector;
    private PlayerController playerController;

    public bool isMessageConfirmed = false;  // 확인 버튼 클릭 여부

    public AudioSource audioSource;  // 오디오 소스
    public AudioClip shrinkSound;  // 작아질 때 재생할 오디오 클립

    void Start()
    {
        originalScale = player.transform.localScale;
        uiDirector = FindObjectOfType<UIDirector>();

        //게임 시작 플레이어 이동 비활성화
        playerController = player.GetComponent<PlayerController>();
        playerController.EnableMovement(false);

        // 오디오 소스 초기화
        audioSource = gameObject.AddComponent<AudioSource>();

        StartCoroutine(StartGameSequence());
    }

    IEnumerator StartGameSequence()
    {
        // 0.5초 후 시작
        yield return new WaitForSeconds(0.5f);

        // 첫 번째 메시지 표시
        uiDirector.ShowPlayerMessage("어.. 컨디션이 안좋네..", 2.0f);
        yield return new WaitForSeconds(2.0f);

        // 두 번째 메시지로 변경
        uiDirector.ShowPlayerMessage("몸이 왜이러지....", 2.0f);
        yield return new WaitForSeconds(2.0f);

        // 플레이어 크기 조정
        float elapsedTime = 0.0f;

        // 오디오 재생
        if (shrinkSound != null)
        {
            audioSource.clip = shrinkSound;
            audioSource.Play();
        }

        while (elapsedTime < shrinkDuration)
        {
            player.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / shrinkDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        player.transform.localScale = targetScale; // 최종 크기 설정

        // 세 번째 메시지로 변경
        uiDirector.ShowPlayerMessage("뭐야! 몸이 작아졌잖아!", 2.0f);
        yield return new WaitForSeconds(2.0f);

        // 화면 중앙에 게임 안내 메시지 표시
        string[] messages = { "키를 되돌릴 수 있는 약을 4개 찾아야 합니다.", "서둘러 찾으세요!" };
        uiDirector.ShowPlayerMessages(messages, 2.0f);

        // 모든 메시지가 표시된 후까지 대기
        yield return new WaitUntil(() => uiDirector.isMessageConfirmed);

        // 메시지 완료 후 플레이어 이동 활성화
        playerController.EnableMovement(true);
    }
}
