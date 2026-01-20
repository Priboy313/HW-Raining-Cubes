using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Colorable))]
public class Rainable : PoolableObject
{
    private float _lifetime = 2;
    private bool hasCollided = false;
    
    private Colorable _colorable;
    private Rigidbody _rigidbody;

    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _colorable = GetComponent<Colorable>();
    }

    public void Init(Color color, float lifetimeMin, float lifetimeMax)
    {
        _colorable.SetColor(color);
        _lifetime = DevUtils.GetRandomNumber(lifetimeMin, lifetimeMax);
        hasCollided = false;
    }

	private void OnCollisionEnter(Collision collision)
    {
        if (hasCollided == false && collision.gameObject.TryGetComponent<Platform>(out _))
        {
            hasCollided = true;
            _colorable.SetRandomColor();

            StartCoroutine(StartTimerToDestroy());
        }
    }

    private IEnumerator StartTimerToDestroy()
    {
        yield return new WaitForSeconds(_lifetime);

        ReturnToPool();
    }
}
