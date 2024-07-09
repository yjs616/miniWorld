using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIceController : MonoBehaviour
{
    private ParticleSystem particleSystem;
    public IceGameDirector gameDirector; // 게임 디렉터 스크립트 연결
    public AudioSource audioSource;  // AudioSource 컴포넌트 참조
    public AudioClip collisionSound; // 충돌 시 재생할 효과음

    // Start is called before the first frame update
    void Start()
    {
        // 파티클 시스템 컴포넌트 가져오기
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("IceBall") )
        {
            // 파티클 재생
            particleSystem.Play();

            // 효과음 재생
            audioSource.PlayOneShot(collisionSound);

            // 게임 디렉터에게 타겟 맞춤 처리 알림
            gameDirector.TargetHit(gameObject);       
        }
    }
}
