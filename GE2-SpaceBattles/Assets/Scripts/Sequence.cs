using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : MonoBehaviour
{
    public float waitBeforeNext;
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Think() { }
}
