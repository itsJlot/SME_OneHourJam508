using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRandomizer : MonoBehaviour
{
    public GameObject[] bottom;
    public GameObject[] top;
    public GameObject[] hair;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var VARIABLE in bottom)
        {
            VARIABLE.SetActive(false);
        }

        foreach (var VARIABLE in this.top)
        {
            VARIABLE.SetActive(false);
        }
        foreach (var VARIABLE in hair)
        {
            VARIABLE.SetActive(false);
        }

        GameObject activeHairPart = hair[Random.Range(0,hair.Length)];
        activeHairPart.SetActive(true);
        activeHairPart.GetComponent<SkinnedMeshRenderer>().material.color = Random.ColorHSV(0, 1, 0.5f, 0.8f);
        GameObject activeBottomPart = bottom[Random.Range(0,bottom.Length)];
        activeBottomPart.SetActive(true);
        activeBottomPart.GetComponent<SkinnedMeshRenderer>().material.color = Random.ColorHSV(0, 1, 0.5f, 0.8f);        
        GameObject activeTopPart = top[Random.Range(0,top.Length)];
        activeTopPart.SetActive(true);
        activeTopPart.GetComponent<SkinnedMeshRenderer>().material.color = Random.ColorHSV(0, 1, 0.5f, 0.8f);

    }
}