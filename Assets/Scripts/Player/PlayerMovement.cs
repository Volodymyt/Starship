using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private GameObject _coinPartice, _audioSource;
    [SerializeField] private UIOptions _UIOptions;
    [SerializeField] private LayerMask _enemyLayerMask;
    [SerializeField] private Slider _energySlider;
    [SerializeField] private TMP_Text _speedText, _percentageText;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _speed, _rotateSpeed, _maxEnergy, _energyRechargeSpeed, _enemyRange;

    private bool _playerIsMoveing = false;
    private float currentRotationSpeed = 0f, _energy;
    private float currentSpeed = 0f;
    private float acceleration = 1f;

    private void Start()
    {
        _energy = _maxEnergy;
        _energySlider.value = _energy;
        _energySlider.maxValue = _maxEnergy;
    }

    private void Update()
    {
        Movement();

        if (FindEnemiesInRange() == true)
        {
            _audioSource.SetActive(true);
        }
        else if (FindEnemiesInRange() == false && !_playerIsMoveing)
        {
            _audioSource.SetActive(false);
        }

        _speedText.text = _rigidbody.velocity.magnitude.ToString("F1") + "/" + _speed.ToString("F1");
        _percentageText.text = (_energy / (_maxEnergy / 100)).ToString("F0") + "%";
    }

    private void Movement()
    {
        float maxSpeed = _speed * _UIOptions.ReturnSpeedPercentage();
        float rotationSpeed = _rotateSpeed * _UIOptions.ReturnSpeedPercentage();

        _energySlider.value = _energy;

        if (_joystick.Vertical > 0.1f || _joystick.Vertical < -0.1f || _joystick.Horizontal < -0.1f || _joystick.Horizontal > 0.1f)
        {
            _audioSource.SetActive(true);
            _playerIsMoveing = true;
        }
        else
        {
            if (FindEnemiesInRange() == false)
            {
                _audioSource.SetActive(false);
                _playerIsMoveing = false;
            }
        }

        if (_joystick.Vertical > 0.1f)
        {
            if (_energy > 10)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
                _rigidbody.velocity = transform.up * currentSpeed;
                _energy -= currentSpeed * Time.deltaTime;
            }
            else
            {
                currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed / 10, acceleration * Time.deltaTime);
                _rigidbody.velocity = transform.up * currentSpeed;

                if (_energy > 0)
                {
                    _energy -= (_speed * _UIOptions.ReturnSpeedPercentage() * 2) * Time.deltaTime;
                }
            }
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, 2f * Time.deltaTime);
            _rigidbody.velocity = transform.up * currentSpeed;
        }

        if (_joystick.Horizontal < -0.4f)
        {
            currentRotationSpeed = rotationSpeed;
            transform.Rotate(0, 0, currentRotationSpeed * Time.deltaTime);
        }
        else if (_joystick.Horizontal > 0.4f)
        {
            currentRotationSpeed = -rotationSpeed;
            transform.Rotate(0, 0, currentRotationSpeed * Time.deltaTime);
        }
        else
        {
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, 0, 2f * Time.deltaTime);
            transform.Rotate(0, 0, currentRotationSpeed * Time.deltaTime);
        }

        if (_energy <= _maxEnergy)
        {
            _energy += _energyRechargeSpeed * Time.deltaTime;
        }
    }

    private bool FindEnemiesInRange()
    {
        bool haveEnemies = false;

        Collider2D[] Enemies = Physics2D.OverlapCircleAll(transform.position, _enemyRange, _enemyLayerMask);

        for (int i = 0; i < Enemies.Length; i++)
        {
            if (Enemies[i] != null)
            {
                haveEnemies = true;
            }
        }

        return haveEnemies;
    }

    public void SetNewSpeed(float NewSpeed)
    {
        _speed += NewSpeed;
    }

    public float ReturnSpeed()
    {
        return _speed;
    }

    public void LoadSpeed(float Speed)
    {
        _speed = Speed;
    }

    public void SetNewMaxEnergy(float NewEnergy)
    {
        _maxEnergy += NewEnergy;
    }

    public float ReturnMaxEnergy()
    {
        return _maxEnergy;
    }

    public void LoadMaxEnergy(float Energy)
    {
        _maxEnergy = Energy;
    }

    public void SetNewEnergeRechargeSpeed(float NewRechargeSpeed)
    {
        _energyRechargeSpeed += NewRechargeSpeed;
    }

    public float ReturnEnergyRechargeSpeed()
    {
        return _energyRechargeSpeed;
    }

    public void LoadEnergyRechargeSpeed(float RechagerSpeed)
    {
        _energyRechargeSpeed = RechagerSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Instantiate(_coinPartice, collision.transform.position, collision.transform.rotation);
            _UIOptions.AddMoney();
            Destroy(collision.gameObject);
        }
    }
}
