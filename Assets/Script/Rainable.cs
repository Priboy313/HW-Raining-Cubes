using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Colorable))]
public class Rainable : MonoBehaviour
{
    private float _lifetimeMin = 2;
    private float _lifetimeMax = 5;

    private Colorable _colorable;
    private Rigidbody _rigidbody;
    private bool hasCollided = false;

    public event Action<Rainable> ActionLifetimeOut;

    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _colorable = GetComponent<Colorable>();
    }

    public void Init(Color color, float lifetimeMin, float lifetimeMax)
    {
        _colorable.SetColor(color);
        _lifetimeMin = lifetimeMin;
        _lifetimeMax = lifetimeMax;

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
        yield return new WaitForSeconds(DevUtils.GetRandomNumber(_lifetimeMin, _lifetimeMax + 1f));

        ActionLifetimeOut?.Invoke(this);
    }
}
