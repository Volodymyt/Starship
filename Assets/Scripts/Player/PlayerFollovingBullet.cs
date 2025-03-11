using System.Linq;
using UnityEngine;

public class PlayerFollovingBullet : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private GameObject _particleSystem;
    [SerializeField] private SoundsStoper _soundsStoper;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private string _tag;
    [SerializeField] private float _followSpeed, _rotationSpeed, _range, _damageRange, _damage;

    private void Start()
    {
        _soundsStoper = GameObject.FindGameObjectWithTag("Explosion Source").GetComponent<SoundsStoper>();
    }

    private void Update()
    {
        Following();
    }

    private void Following()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, _range, _layerMask);

        if (objects.Length > 0)
        {
            _target = objects
                .OrderBy(t => Vector2.Distance(t.transform.position, transform.position))
                .FirstOrDefault()
                ?.transform;
        }

        if (_target != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, _target.position);

            Vector2 direction = (_target.position - transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            _rigidbody.velocity = direction * _followSpeed;
        }
    }

    public void SetNewDamage(float NewDamage)
    {
        _damage += NewDamage;
    }

    public float ReturnDamage()
    {
        return _damage;
    }

    public void LoadDamage(float Damage)
    {
        _damage = Damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_tag) || collision.gameObject.CompareTag("Meteorite"))
        {
            _soundsStoper.PlaySound();

            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _damageRange, _layerMask);

            foreach (Collider2D enemy in enemies)
            {
                enemy.gameObject.GetComponent<Heals>().TakeDamage(_damage);
            }

            Instantiate(_particleSystem, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
