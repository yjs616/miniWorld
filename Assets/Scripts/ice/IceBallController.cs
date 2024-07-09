using UnityEngine;
using System.Collections; // IEnumerator를 사용하기 위해 추가

public class IceBallController : MonoBehaviour
{
    public void Shoot(Vector3 dir)
    {
        GetComponent<Rigidbody>().AddForce(dir, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        //force를 무력화 시킴
        GetComponent<Rigidbody>().isKinematic = true;
        //GetComponent<ParticleSystem>().Play();

        Destroy(gameObject);    //충돌하면 파괴됨
    }
}
