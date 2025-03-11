using UnityEngine;

public class MeteoriteSpawn : MonoBehaviour
{
    [SerializeField] private float _range, _distanceBehindCamera, _distanseBetweenMeteorites;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _meteorite;
    [SerializeField] private Vector2 _randomRange = new Vector2(5f, 5f);

    private void Update()
    {
        RaycastHit2D[] meteorites = Physics2D.CircleCastAll(_player.position, _range, Vector2.zero, 0, _layerMask);

        if (meteorites.Length < 5)
        {
            SpawnMeteorites(5 - meteorites.Length);
        }
    }

    private void SpawnMeteorites(int numberToSpawn)
    {
        int spawned = 0;

        while (spawned < numberToSpawn)
        {
            Vector3 spawnPosition = GenerateRandomPosition();

            if (IsValidSpawnPosition(spawnPosition))
            {
                Instantiate(_meteorite, spawnPosition, Quaternion.identity);
                spawned++;
            }
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        float randomAngle = Random.Range(0, 2 * Mathf.PI);
        float randomDistance = Random.Range(_distanceBehindCamera, _distanceBehindCamera + _randomRange.magnitude);
        float randomX = Mathf.Cos(randomAngle) * randomDistance;
        float randomY = Mathf.Sin(randomAngle) * randomDistance;

        return new Vector3(_player.position.x + randomX, _player.position.y + randomY, 0f);
    }

    private bool IsValidSpawnPosition(Vector3 spawnPosition)
    {
        Collider2D[] meteoritesNearby = Physics2D.OverlapCircleAll(spawnPosition, _distanseBetweenMeteorites, _layerMask);

        return meteoritesNearby.Length == 0;
    }
}

