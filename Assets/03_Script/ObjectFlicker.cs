using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFlicker : MonoBehaviour
{
    public GameObject checkerLight;

    ObjectCheck objectCheck;

    private void Start()
    {
        objectCheck = GameObject.FindWithTag("Player").GetComponent<ObjectCheck>();
    }

    private void Update()
    {
        if(Time.time > 5 && objectCheck.isInteract_Scarf == false)
        {
            StartCoroutine("CheckerLighting");
        }
        else if (objectCheck.isInteract_Scarf)
        {
            StopCoroutine("CheckerLighting");
        }
    }

    IEnumerator CheckerLighting()
    {
        checkerLight.SetActive(false);

        yield return new WaitForSeconds(2);

        checkerLight.SetActive(true);
    }
}
