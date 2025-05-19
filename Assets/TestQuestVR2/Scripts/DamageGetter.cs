using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class DamageGetter : MonoBehaviour
{
    private Rigidbody _rb;
    public event Action<int> OnGetDamage;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out DamageDealer damageDealer))
        {
            int damage = Mathf.RoundToInt((_rb.velocity.magnitude + collision.rigidbody.velocity.magnitude) * damageDealer.DefaultDamage) ;

            OnGetDamage?.Invoke(damage);
        }
    }
}
