using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _bullet, _followingBullet, _particleSystem;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _shootSound, _explosiveBulletShoot;
    [SerializeField] private Button shootButton, explosiveShootButton, laserButton;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Laser _laser;
    [SerializeField] private Image _bulletFill, _explosionBulletFill, _laserFill;
    [SerializeField] private float _shootForce, _rechargeTime, _followBulletRechargeTime, _laserRechargeTime, _laserUseTime;
    [SerializeField] private bool _gunInSpaceShip = false, _firstGun = false;

    private bool isLaserActive = false, isShootingActive = false;
    private float _shootingTime, _followBulletShootingTime, _laserShootingTime = 0, _laserWorkTime;

    private void Start()
    {
        _shootingTime = _rechargeTime;
        _laserWorkTime = _laserUseTime;

        explosiveShootButton.onClick.AddListener(() => OnExplosiveShootButtonPressed());

        EventTrigger shootTrigger = shootButton.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry shootPointerDown = new EventTrigger.Entry();
        shootPointerDown.eventID = EventTriggerType.PointerDown;
        shootPointerDown.callback.AddListener((data) => { OnShootButtonDown(); });
        shootTrigger.triggers.Add(shootPointerDown);

        EventTrigger.Entry shootPointerUp = new EventTrigger.Entry();
        shootPointerUp.eventID = EventTriggerType.PointerUp;
        shootPointerUp.callback.AddListener((data) => { OnShootButtonUp(); });
        shootTrigger.triggers.Add(shootPointerUp);

        if (laserButton != null)
        {
            EventTrigger laserTrigger = laserButton.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry laserPointerDown = new EventTrigger.Entry();
            laserPointerDown.eventID = EventTriggerType.PointerDown;
            laserPointerDown.callback.AddListener((data) => { OnLaserButtonDown(); });
            laserTrigger.triggers.Add(laserPointerDown);

            EventTrigger.Entry laserPointerUp = new EventTrigger.Entry();
            laserPointerUp.eventID = EventTriggerType.PointerUp;
            laserPointerUp.callback.AddListener((data) => { OnLaserButtonUp(); });
            laserTrigger.triggers.Add(laserPointerUp);
        }
    }

    private void Update()
    {
        if (_firstGun)
        {
            _bulletFill.fillAmount = (_rechargeTime - _shootingTime) / _rechargeTime;
        }

        if (_gunInSpaceShip)
        {
            _explosionBulletFill.fillAmount = (_followBulletRechargeTime - _followBulletShootingTime) / _followBulletRechargeTime;

            if (_laserWorkTime > 0 && _laserShootingTime <= 0)
            {
                _laserFill.fillAmount = (0 + _laserWorkTime) / _laserUseTime;
            }
            else
            {
                _laserFill.fillAmount = 1 - (_laserShootingTime / _laserRechargeTime);
            }
        }

        if (!_gunInSpaceShip)
        {
            if (isShootingActive && _shootingTime <= 0)
            {
                if (_firstGun)
                {
                    _audioSource.PlayOneShot(_shootSound);
                }

                Shoot(_bullet);
                _shootingTime = _rechargeTime;
                StartCoroutine(ShootAnimation());
            }
        }

        if (_shootingTime > 0)
        {
            _shootingTime -= Time.deltaTime;
        }

        if (_followBulletShootingTime > 0)
        {
            _followBulletShootingTime -= Time.deltaTime;
        }

        if (_laserShootingTime <= 0)
        {
            if (_laserWorkTime > 0)
            {
                if (isLaserActive)
                {
                    _laser.EnableLaser();
                    _laser.UpdateLaser();
                    _laserWorkTime -= Time.deltaTime;
                }
            }
            else
            {
                _laserShootingTime = _laserRechargeTime;
                _laserWorkTime = _laserUseTime;

                if (_laser != null)
                {
                    _laser.DisableLaser();
                }
            }
        }
        else
        {
            _laserShootingTime -= Time.deltaTime;
            _laser.DisableLaser();
        }
    }

    public void OnShootButtonDown()
    {
        isShootingActive = true;
    }

    public void OnShootButtonUp()
    {
        isShootingActive = false;
    }

    public void OnShootButtonPressed()
    {
        if (!_gunInSpaceShip && _shootingTime <= 0)
        {
            Shoot(_bullet);
            _shootingTime = _rechargeTime;
            StartCoroutine(ShootAnimation());
        }
    }

    public void OnExplosiveShootButtonPressed()
    {
        if (_followBulletShootingTime <= 0)
        {
            _audioSource.PlayOneShot(_explosiveBulletShoot);
            Shoot(_followingBullet);
            _followBulletShootingTime = _followBulletRechargeTime;
        }
    }

    public void OnLaserButtonDown()
    {
        if (_laserShootingTime <= 0 && _laserWorkTime > 0)
        {
            isLaserActive = true;
        }
    }

    public void OnLaserButtonUp()
    {
        isLaserActive = false;
        _laser.DisableLaser();
    }

    public void SetNewRechargeTime(float NewRechargeTime)
    {
        _rechargeTime -= NewRechargeTime;
    }

    public float ReturnRechargeTime()
    {
        return _rechargeTime;
    }

    public void LoadRechargeTime(float Value)
    {
        _rechargeTime = Value;
    }

    public void SetNewFollowBulletRechargeTime(float NewRechargeTime)
    {
        _followBulletRechargeTime -= NewRechargeTime;
    }

    public float ReturnFollowBulletRechargeTime()
    {
        return _followBulletRechargeTime;
    }

    public void LoadFollowBulletRechargeTime(float Value)
    {
        _followBulletRechargeTime = Value;
    }

    public void SetNewLaserRechargeTime(float NewRechargeTime)
    {
        _laserRechargeTime -= NewRechargeTime;
    }

    public float ReturnLaserRechargeTime()
    {
        return _laserRechargeTime;
    }

    public void LoadLaserRrechargeTime(float Value)
    {
        _laserRechargeTime = Value;
    }

    private void Shoot(GameObject Bullet)
    {
        GameObject bullet = Instantiate(Bullet, _shootPoint.position, _shootPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(_shootPoint.up * _shootForce, ForceMode2D.Impulse);
    }

    private IEnumerator ShootAnimation()
    {
        _animator.SetTrigger("Shoot");
        _particleSystem.SetActive(true);

        yield return new WaitForSeconds(0.25f);

        _particleSystem.SetActive(false);
        _animator.SetTrigger("Idel");
    }
}
