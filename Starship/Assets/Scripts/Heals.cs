using UnityEngine;
using UnityEngine.UI;

public class Heals : MonoBehaviour
{
    [SerializeField] private Slider _healBar;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private float _maxHeals;
    [SerializeField] private bool _isPlayer = false;

    private float _health;

    private void Start()
    {
        _health = _maxHeals;
    }

    private void Update()
    {
        if (_isPlayer)
        {
            _healBar.maxValue = _maxHeals;
            _healBar.value = _health;
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
            }

            Destroy(gameObject);
        }
    }
}
