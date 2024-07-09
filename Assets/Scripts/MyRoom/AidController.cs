using System.Collections;
using UnityEngine;

public class AidController : MonoBehaviour
{
    private PlayerController playerController;
    private UIDirector uiDirector;
    public ParticleSystem aidEffect;

    public AudioSource audioSource;   // 오디오 소스를 추가
    public AudioClip collisionSound;  // 충돌 효과음을 위한 오디오 클립을 추가

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        uiDirector = FindObjectOfType<UIDirector>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();  // 오디오 소스가 없으면 추가
        }
        audioSource.clip = collisionSound;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayCollisionSound();
            StartCoroutine(ShowBatteryMessages());
        }
    }

    private IEnumerator ShowBatteryMessages()
    {
        aidEffect.Play();
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);

        string[] messages = { "이제 나가야겠다", "근데..어떻게 나가지?", "힘으로 가자" };
        uiDirector.ShowPlayerMessages(messages, 2.0f);

        // 플레이어가 배터리를 획득했을 때 공 던질 수 있는 상태로 설정
        playerController.EnableThrowBall(true);
    }

    private void PlayCollisionSound()
    {
        if (audioSource != null && collisionSound != null)
        {
            audioSource.Play();
        }
    }
}
