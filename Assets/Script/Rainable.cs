using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Colorable))]
public class Rainable : MonoBehaviour
{
    [SerializeField] private float _secondsLifeAfterCollisionMin;
    [SerializeField] private float _secondsLifeAfterCollisionMax;

    private Colorable _colorable;
    private bool hasChangedColor = false;

    public event Action TimeEnd;

    private void Awake()
    {
        _colorable = GetComponent<Colorable>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform _))
        {
            if (hasChangedColor == false)
            {
                _colorable.SetRandomColor();
                hasChangedColor = true;
            }
        }
    }
}
