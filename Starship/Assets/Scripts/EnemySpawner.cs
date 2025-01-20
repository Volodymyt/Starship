using System.Collections;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private float _distanceBehindCamera = 5f, _spawnRate;
    [SerializeField] private Vector2 _randomRange = new Vector2(5f, 5f);

    private int _difficulty = 0;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        GameObject Enemy;

        while (true)
        {
            float RandomAngle = Random.Range(0, 2 * Mathf.PI);

            if (_difficulty < _enemies.Length)
                Enemy = _enemies[Random.Range(0, _difficulty + 1)];
            else
                Enemy = _enemies[Random.Range(0, _enemies.Length)];

            float RandomDistance = Random.Range(_distanceBehindCamera, _distanceBehindCamera + _randomRange.magnitude);

            float RandomX = Mathf.Cos(RandomAngle) * RandomDistance;
            float RandomY = Mathf.Sin(RandomAngle) * RandomDistance;

            Vector3 SpawnPosition = new Vector3(transform.position.x + RandomX, transform.position.y + RandomY);

            Instantiate(Enemy, SpawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(_spawnRate);
        }
    }
}
