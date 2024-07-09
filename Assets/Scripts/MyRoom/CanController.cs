using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanController : MonoBehaviour
{
    private PlayerController playerController;
    private UIDirector uiDirector;
    public ParticleSystem canEffect;
    public AudioSource audioSource;  // 오디오 소스를 추가
    public AudioClip effectSound;    // 오디오 클립을 추가

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        uiDirector = FindObjectOfType<UIDirector>();
        
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();  // 오디오 소스가 없으면 추가
        }
        audioSource.clip = effectSound;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canEffect.Play();
            playerController.PowerUpJump(7.0f, 2.0f);  // 2초 동안 파티클 유지
            uiDirector.ShowPlayerMessage("점프력 2배!", 2.0f);
            PlayEffectSound();
            Destroy(gameObject, 2.0f);  // 2초 후 오브젝트 파괴
        }
    }

    void PlayEffectSound()
    {
        audioSource.time = 20f;  // 오디오 클립의 20초 구간에서 시작
        audioSource.Play();
        StartCoroutine(StopEffectSoundAfterTime(2.0f));  // 2초 후 오디오 정지
    }

    IEnumerator StopEffectSoundAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        audioSource.Stop();
    }
}
