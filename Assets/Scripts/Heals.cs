using UnityEngine;
using UnityEngine.UI;

public class Heals : MonoBehaviour
{
    [SerializeField] private Slider _healBar;
    [SerializeField] private SoundsStoper _soundsStoper;

    [SerializeField] private GameObject _losePanel, _explosionParticle, _coin;
    [SerializeField] private float _maxHeals, _healthRechargeValue, _rechargeTime;
    [SerializeField] private bool _isPlayer = false;
    [SerializeField] private InterstitialReclam _interstitialReclam;

    private EnemySpawner _enemySpawner;
    private float _health, _timeToRecharge;

    private void Start()
    {
        _enemySpawner = FindObjectOfType<EnemySpawner>();

        if (!_isPlayer)
        {
            _soundsStoper = GameObject.FindGameObjectWithTag("Explosion Source").GetComponent<SoundsStoper>();
        }
        else
        {
            if (!PlayerPrefs.HasKey("DeadEnemies"))
            {
                PlayerPrefs.SetInt("DeadEnemies", 0);
            }
        }


        _health = _maxHeals;
        _timeToRecharge = _rechargeTime;
    }

    private void Update()
    {
        if (_isPlayer)
        {
            _healBar.maxValue = _maxHeals;
            _healBar.value = _health;

            if (_health < _maxHeals)
            {
                if (_timeToRecharge <= 0)
                {
                    _health += _healthRechargeValue;
                    _timeToRecharge = _rechargeTime;
                }
                else
                {
                    _timeToRecharge -= Time.deltaTime;
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            if (_isPlayer)
            {
                _healBar.value = 0;
                _losePanel.SetActive(true);
                Time.timeScale = 0;

                PlayerPrefs.SetInt("DeadEnemies", PlayerPrefs.GetInt("DeadEnemies") + 1);

                if (PlayerPrefs.GetInt("DeadEnemies") == 2)
                {
                    Debug.Log("ok");
                    _interstitialReclam.Show();
                    PlayerPrefs.SetInt("DeadEnemies", 0);
                }
            }
            else
            {
                _soundsStoper.PlaySound();
                _enemySpawner.AddDeadEnemy();
                Instantiate(_coin, transform.position, transform.rotation);
                Instantiate(_explosionParticle, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }

    public void SetNewMaxHealth(float NewMaxHealth)
    {
        _maxHeals += NewMaxHealth;
    }

    public float ReturnMaxHealth()
    {
        return _maxHeals;
    }

    public void LoadMaxHealth(float MaxHealth)
    {
        _maxHeals = MaxHealth;
    }

    public void SetNewRechargeSpeed(float NewRechargeSpeed)
    {
        _rechargeTime -= NewRechargeSpeed;
    }

    public float ReturnRechargeSpeed()
    {
        return _rechargeTime;
    }

    public void LoadHealthRechargeSpeed(float Value)
    {
        _rechargeTime = Value;
    }
}
