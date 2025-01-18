using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkGlass : MonoBehaviour
{
    void Fill()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
        GetComponent<ParticleSystem>().Play();
    }
}
