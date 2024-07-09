using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YardPlayerController : MonoBehaviour
{
    public float speed = 2000f;
    public ParticleSystem particleSystem;
    public YardInitializer yardInitializer;
    public bool canMove = false;
    private Animator animator;
    public Vector3 targetScale = new Vector3(20, 20, 20);
    public float scaleDuration = 1f;  // 서서히 커지는 시간(초)
    public AudioSource audioSource;  // AudioSource 컴포넌트 참조
    public AudioClip collisionSound; // 충돌 시 재생할 효과음

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove && Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Aid"))
        {
            Destroy(collision.gameObject);
            particleSystem.Play();
            audioSource.PlayOneShot(collisionSound);
            StartCoroutine(ScaleOverTime(targetScale, scaleDuration));
            yardInitializer.DisplaySuccess();
            StartCoroutine(RotateOverTime(180f, 3f)); // 2초 동안 180도 회전
        }
    }

    private IEnumerator ScaleOverTime(Vector3 targetScale, float duration)
    {
        Vector3 initialScale = transform.localScale;
        float time = 0;

        while (time < duration)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale; // Ensure final scale is set
    }

    private IEnumerator RotateOverTime(float angle, float duration)
    {
        yield return new WaitForSeconds(2f);
        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0, angle, 0);
        float time = 0;

        while (time < duration)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation; // Ensure final rotation is set
    }
}
