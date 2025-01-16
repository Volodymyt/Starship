using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shootPoint, _endPosition;
    [SerializeField] private LayerMask _playerLayerMask;
    [SerializeField] private float _shootForce, _rechargeTime;

    private float _shootingTime;

    private void Start()
    {
        _shootingTime = _rechargeTime;
    }

    private void Update()
    {
        if (_shootingTime <= 0)
        {
            RaycastHit2D[] objects = Physics2D.RaycastAll(_shootPoint.position, _endPosition.position - _shootPoint.position, Vector3.Distance(_shootPoint.position, _endPosition.position), _playerLayerMask);

            if (objects.Length > 0)
            {
                Shoot();
                _shootingTime = _rechargeTime;
            }
        }
        else
        {
            _shootingTime -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(_shootPoint.up * _shootForce, ForceMode2D.Impulse);
    }
}
