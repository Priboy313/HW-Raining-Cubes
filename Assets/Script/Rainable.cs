using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Colorable))]
public class Rainable : MonoBehaviour
{
    private Colorable _colorable;
    private Rigidbody _rigidbody;
    private bool hasCollided = false;

    public event Action<Rainable> ActionPlatformCollided;

    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _colorable = GetComponent<Colorable>();
    }

    public void Init(Color color)
    {
        _colorable.SetColor(color);
        hasCollided = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasCollided == false && collision.gameObject.TryGetComponent<Platform>(out _))
        {
            hasCollided = true;
            _colorable.SetRandomColor();
            ActionPlatformCollided?.Invoke(this);
        }
    }
}
