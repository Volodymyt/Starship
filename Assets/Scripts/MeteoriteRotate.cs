using UnityEngine;

public class MeteoriteRotate : MonoBehaviour
{
    private float rotationSpeed = 0;

    private void Start()
    {
        rotationSpeed = Random.Range(1, 12);

        float scale = Random.Range(3.5f, 6);
        transform.localScale = new Vector3(scale, scale, 0);
    }

    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
