using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* the reaper has weapons that need to be enabled and disabled at specific sequences*/
public class ReaperWeaponManager : MonoBehaviour
{
    public GameObject[] weaponObjs;
    
    public void EnableWeapons()
    {
        foreach (var weapon in weaponObjs)
        {
            weapon.SetActive(true);
        }
    }
    
    public void DisableWeapons()
    {
        foreach (var weapon in weaponObjs)
        {
            weapon.SetActive(false);
        }
    }
}
