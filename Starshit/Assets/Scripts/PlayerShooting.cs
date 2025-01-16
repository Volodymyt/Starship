using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shootPoint;
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
            if (Input.GetKey(KeyCode.Mouse0))
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
