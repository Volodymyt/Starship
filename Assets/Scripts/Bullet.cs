using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private string _tag;

    private void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    private void OnApplicationQuit()
    {
        _damage = 1;
    }

    private void OnDisable()
    {
        _damage = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_tag))
        {
            collision.gameObject.GetComponent<Heals>().TakeDamage(_damage);
            Destroy(gameObject);
        }
        
        if (collision.gameObject.CompareTag("Meteorite"))
        {
            Destroy(gameObject);
        }
    }

    public void SetNewDamage(float NewDamage)
    {
        _damage += NewDamage;
    }

    public float ReturnDamega()
    {
        return _damage;
    }

    public void LoadDamage(float Damage)
    {
        _damage = Damage;
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}
