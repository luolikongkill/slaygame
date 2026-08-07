using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
   public static List<GameObject> vfxs = new();

   public static void ClearUPVFX()
    {
        foreach (GameObject obj in vfxs)
        {
            Destroy(obj);
        }
        vfxs.Clear();
    }
}
