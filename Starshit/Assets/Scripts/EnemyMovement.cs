using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _followSpeed = 5f;
    [SerializeField] private float _stopDistance = 1f;
    [SerializeField] private float _decelerationRate = 2f;
    [SerializeField] private float _rotationSpeed = 200f;
    [SerializeField] private Rigidbody2D _rigidbody;

    private void Start()
    {
        _player = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, _player.position);

        Vector2 direction = (_player.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        if (distanceToPlayer > _stopDistance)
        {
            _rigidbody.velocity = direction * _followSpeed;
        }
        else
        {
            _rigidbody.velocity = Vector2.Lerp(_rigidbody.velocity, Vector2.zero, _decelerationRate * Time.deltaTime);
        }
    }
}

