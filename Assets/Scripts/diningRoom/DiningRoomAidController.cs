using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiningRoomAidController : MonoBehaviour
{
    private DiningRoomUIDirector uiDirector;
    public ParticleSystem aidEffect;
    public AudioSource audioSource;  // AudioSource 컴포넌트 참조
    public AudioClip collisionSound; // 충돌 시 재생할 효과음
    string message = "나이스! 부엌으로 가자!";

    // Start is called before the first frame update
    void Start()
    {
        uiDirector = FindObjectOfType<DiningRoomUIDirector>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PlayEffect());

            // Kitchen 씬으로 전환
            SceneManager.LoadScene("Kitchen");
        }
    }

    private IEnumerator PlayEffect()
    {
        // 파티클 재생
        aidEffect.Play();

        // 효과음 재생
        audioSource.PlayOneShot(collisionSound);

        yield return new WaitForSeconds(0.5f);

        // 배터리 파괴
        Destroy(gameObject);

        // 메시지 표시
        uiDirector.ShowPlayerMessage(message, 2.0f);

        // 메시지 표시 시간 기다림
        yield return new WaitForSeconds(2f);
    }
}
