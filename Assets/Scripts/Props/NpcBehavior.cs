using System.Collections;
using System.Collections.Generic;
using Props;
using UnityEngine;

public class NpcBehavior : MonoBehaviour
{
    public void Start()
    {
        if (Random.value > 0.5)
        {
            var spawnedHat = Instantiate(hats[Random.Range(0, hats.Length - 1)], hatPos);
            var prop = spawnedHat.GetComponent<PropBehavior>();
            prop.rig.isKinematic = true;
            prop.canBeInteractedWith = false;
        }
        if (Random.value > 0.5)
        {
            var spawnedGlasses = Instantiate(glasses[Random.Range(0, hats.Length - 1)], glassesPos);
            var prop = spawnedGlasses.GetComponent<PropBehavior>();
            prop.rig.isKinematic = true;
            prop.canBeInteractedWith = false;
        }
        if (Random.value > 0.5)
        {
            var spawnedCup = Instantiate(cups[Random.Range(0, hats.Length - 1)], cupPos);
            var prop = spawnedCup.GetComponent<PropBehavior>();
            prop.rig.isKinematic = true;
            prop.canBeInteractedWith = false;
        }
    }

    public Transform hatPos;
    public Transform glassesPos;
    public Transform cupPos;

    public GameObject[] hats;
    public GameObject[] glasses;
    public GameObject[] cups;
}
