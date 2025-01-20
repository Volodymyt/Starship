using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private UIOptions _UIOptions;
    [SerializeField] private Slider _energySlider;
    [SerializeField] private TMP_Text _speedText, _percentageText;
    [SerializeField] private float _speed, _rotateSpeed, _maxEnergy, _energyRechargeSpeed;

    private float currentRotationSpeed = 0f, _energy;

    private void Start()
    {
        _energy = _maxEnergy;
        _energySlider.value = _energy;
        _energySlider.maxValue = _maxEnergy;
    }

    private void Update()
    {
        Movement();
        _speedText.text = _rigidbody.velocity.magnitude.ToString("F1") + "/" + _speed.ToString();
        _percentageText.text = (_energy / (_maxEnergy / 100)).ToString("F0") + "%";
    }

    private void Movement()
    {
        float speed = _speed * _UIOptions.ReturnSpeedPercentage();
        float rotationSpeed = _rotateSpeed * _UIOptions.ReturnSpeedPercentage();

        _energySlider.value = _energy;

        if (Input.GetKey(KeyCode.W))
        {
            if (_energy > 10)
            {
                _rigidbody.velocity = transform.up * speed;
                _energy -= (_speed * _UIOptions.ReturnSpeedPercentage()) * Time.deltaTime;
            }
            else
            {
                _rigidbody.velocity = transform.up * (speed / 10);

                if (_energy > 0)
                {
                    _energy -= (_speed * _UIOptions.ReturnSpeedPercentage() * 2) * Time.deltaTime;
                }
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (_energy > 10)
            {
                _rigidbody.velocity = transform.up * (-speed / 2);
                _energy -= (_speed * _UIOptions.ReturnSpeedPercentage()) * Time.deltaTime;
            }
            else
            {
                _rigidbody.velocity = transform.up * ((speed / 2) / 10);

                if (_energy > 0)
                {
                    _energy -= (_speed * _UIOptions.ReturnSpeedPercentage() * 2) * Time.deltaTime;
                }
            }
        }
        else
        {
            _rigidbody.velocity = Vector2.Lerp(_rigidbody.velocity, Vector2.zero, 2f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            currentRotationSpeed = rotationSpeed;
            transform.Rotate(0, 0, currentRotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
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
}
