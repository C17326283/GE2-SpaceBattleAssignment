using System;
using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine;
using Random = UnityEngine.Random;

/* the basis of all the behaviours */
[RequireComponent(typeof(ShipBoid))]
public class BaseShipBehaviour : MonoBehaviour
{
    [HideInInspector]
    public ShipBoid shipBoid;

    private void Awake()
    {
        shipBoid = GetComponent<ShipBoid>();
    }
}
