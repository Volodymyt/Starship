using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private float _distanceBehindCamera = 5f, _spawnRate;
    [SerializeField] private Vector2 _randomRange = new Vector2(5f, 5f);

    [SerializeField] private int _difficulty = 0, _deadEnemisCount = 0;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        GameObject Enemy = null;

        while (true)
        {
            float RandomAngle = Random.Range(0, 2 * Mathf.PI);

            if (_difficulty < 3)
            {
                Enemy = _enemies[Random.Range(0, _difficulty + 1)];
            }
            else if (_difficulty > 3 && _difficulty < 10)
            {
                Enemy = _enemies[Random.Range(0, _enemies.Length)];

                _spawnRate = 3.5f - (((float)_difficulty / 10) - 0.3f);
            }
            else
            {
                Enemy = _enemies[Random.Range(0, _enemies.Length)];

                _spawnRate = 2.7f;
            }

            float RandomDistance = Random.Range(_distanceBehindCamera, _distanceBehindCamera + _randomRange.magnitude);

            float RandomX = Mathf.Cos(RandomAngle) * RandomDistance;
            float RandomY = Mathf.Sin(RandomAngle) * RandomDistance;

            Vector3 SpawnPosition = new Vector3(transform.position.x + RandomX, transform.position.y + RandomY);

            Instantiate(Enemy, SpawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(_spawnRate);
        }
    }

    public void AddDeadEnemy()
    {
        _deadEnemisCount++;

        _difficulty = System.Math.Min(_deadEnemisCount / 30, 10);
    }

    public int ReturnDifficulty()
    {
        return _difficulty;
    }

    public void LoadDifficulty(int Value)
    {
        _difficulty = Value;
    }

    public int ReturnDeadsEnemies()
    {
        return _deadEnemisCount;
    }

    public void LoadDeadsEnemies(int value)
    {
        _deadEnemisCount = value;
    }
}
