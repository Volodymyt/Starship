using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private GameObject _particleSystem, _audioSource;
    [SerializeField] private Transform _firePoint, _targetPosition;
    [SerializeField] private AudioSource _audioSourceCom;
    [SerializeField] private LayerMask _hitLayerMask;
    [SerializeField] private float _damage, _damageTime;

    private List<ParticleSystem> particles = new List<ParticleSystem>();
    private float _disableDamageTime;

    private void Start()
    {
        _audioSourceCom.volume = PlayerPrefs.GetFloat("SS");

        _disableDamageTime = _damageTime;
        FillLists();
        DisableLaser();
    }

    public void EnableLaser()
    {
        _lineRenderer.enabled = true;
        _audioSource.SetActive(true);

        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].gameObject.SetActive(true);
        }
    }

    public void UpdateLaser()
    {
        Vector2 targetPosition = (Vector2)_targetPosition.position;

        _lineRenderer.SetPosition(0, _firePoint.position);

        Vector2 direction = targetPosition - (Vector2)_firePoint.position;

        RaycastHit2D hit = Physics2D.Raycast(_firePoint.position, direction.normalized, direction.magnitude, _hitLayerMask);

        if (hit.collider != null)
        {
            _lineRenderer.SetPosition(1, hit.point);

            if (!hit.collider.gameObject.CompareTag("Meteorite"))
            {
                if (_disableDamageTime <= 0)
                {
                    hit.collider.gameObject.GetComponent<Heals>().TakeDamage(_damage);
                    _disableDamageTime = _damageTime;
                }
                else
                {
                    _disableDamageTime -= Time.deltaTime;
                }
            }
        }
        else
        {
            _lineRenderer.SetPosition(1, targetPosition);
        }

        _particleSystem.transform.position = _lineRenderer.GetPosition(1);
    }

    public void DisableLaser()
    {
        _lineRenderer.enabled = false;
        _audioSource.SetActive(false);

        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].gameObject.SetActive(false);
        }
    }

    public void FillLists()
    {
        for (int i = 0; i < _particleSystem.transform.childCount; i++)
        {
            var ps = _particleSystem.transform.GetChild(i).GetComponent<ParticleSystem>();

            if (ps != null)
            {
                particles.Add(ps);
            }
        }
    }

    public void SetNewDamage(float NewDamage)
    {
        _damage += NewDamage;
    }

    public float ReturnDamage()
    {
        return _damage;
    }

    public void LoadDamage(float Value)
    {
        _damage = Value;
    }    

    public void SetNewRechargeTime(float NewRechargeTime)
    {
        _damageTime -= NewRechargeTime;
    }

    public float ReturnDamageTime()
    {
        return _damageTime;
    }

    public void LoadDamageTime(float Value)
    {
        _damageTime = Value;
    }
}
