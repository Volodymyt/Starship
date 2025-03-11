using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private float _lifeTime = 3.5f;

    private void Update()
    {
        if (_lifeTime <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            _lifeTime -= Time.deltaTime;
        }
    }
}
