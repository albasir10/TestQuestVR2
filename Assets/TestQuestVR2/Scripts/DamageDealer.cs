using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float _defaultDamage = 10;

    public float DefaultDamage { get { return _defaultDamage; }}
}
