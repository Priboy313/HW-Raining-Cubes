using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    private static readonly Collider[] _hitsBuffer = new Collider[30];

    public void ApplyExplosionForceAround(float force, float radius)
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, radius, _hitsBuffer);

        var uniueRigidbodies = new HashSet<Rigidbody>();

        for (int i = 0; i < count; i++)
        {
            Collider hit = _hitsBuffer[i];

            if (hit.attachedRigidbody != null)
            {
                if (hit.attachedRigidbody.gameObject != gameObject)
                {
                    uniueRigidbodies.Add(hit.attachedRigidbody);
                }
            }
        }

        foreach (Rigidbody rigidbody in uniueRigidbodies)
        {
            rigidbody.AddExplosionForce(force, transform.position, radius);
        }
    }
}
