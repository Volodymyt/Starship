using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(_player.position.x, _player.position.y, -10);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed);
    }
}
