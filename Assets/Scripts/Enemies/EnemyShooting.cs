using System.Collections;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private GameObject _bullet, _particleSystem;
    [SerializeField] private Animator _animator;
    [SerializeField] private Laser _laser;
    [SerializeField] private SoundsStoper _soundStoper;
    [SerializeField] private Transform _shootPoint, _endPosition;
    [SerializeField] private LayerMask _playerLayerMask;
    [SerializeField] private bool _isLaserEnemy = false;
    [SerializeField] private float _shootForce, _rechargeTime, _laserUseTime, _laserRechargeTime;

    private float _shootingTime, _laserShootingTime = 0, _laserWorkTime;

    private void Start()
    {
        _shootingTime = _rechargeTime;

        _soundStoper = GameObject.FindGameObjectWithTag("Sooting Source").GetComponent<SoundsStoper>();
    }

    private void Update()
    {
        if (_isLaserEnemy)
        {
            if (_laserShootingTime <= 0)
            {
                if (_laserWorkTime > 0)
                {
                    Vector2 direction = _endPosition.position - _shootPoint.position;
                    Vector2 boxSize = new Vector2(direction.magnitude, 1f);
                    float boxDirection = Vector2.SignedAngle(Vector2.right, direction);
                    Vector2 castDirection = _endPosition.transform.position.normalized;

                    RaycastHit2D[] objects = Physics2D.BoxCastAll(
                        _shootPoint.position,
                        boxSize,
                        boxDirection,
                        castDirection,
                        direction.magnitude,
                        _playerLayerMask
                    );

                    if (objects.Length > 0)
                    {
                        _laser.EnableLaser();
                        _laser.UpdateLaser();
                        _laserWorkTime -= Time.deltaTime;
                    }
                    else
                    {
                        _laser.DisableLaser();
                    }
                }
                else
                {
                    _laserShootingTime = _laserRechargeTime;
                    _laserWorkTime = _laserUseTime;
                    _laser.DisableLaser();
                }
            }
            else
            {
                _laserShootingTime -= Time.deltaTime;
                _laser.DisableLaser();
            }
        }
        else
        {
            if (_shootingTime <= 0)
            {
                RaycastHit2D[] objects = Physics2D.RaycastAll(_shootPoint.position, _endPosition.position - _shootPoint.position, Vector3.Distance(_shootPoint.position, _endPosition.position), _playerLayerMask);

                if (objects.Length > 0)
                {
                    _soundStoper.PlaySound();
                    Shoot();
                    StartCoroutine(ShootAnimation());
                    _shootingTime = _rechargeTime;
                }
            }
            else
            {
                _shootingTime -= Time.deltaTime;
            }
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(_shootPoint.up * _shootForce, ForceMode2D.Impulse);
    }

    private IEnumerator ShootAnimation()
    {
        _animator.SetTrigger("Shoot");
        _particleSystem.SetActive(true);

        yield return new WaitForSeconds(0.6f);

        _particleSystem.SetActive(false);
        _animator.SetTrigger("Idel");
    }
}
