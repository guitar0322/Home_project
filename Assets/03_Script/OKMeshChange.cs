using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OKMeshChange : MonoBehaviour
{
    public GameObject[] needChangeObject;
    public Texture[] myTextrue;
    public Texture[] horror_Texture;
    
    public void ChangeRenderer()
    {
        for (int i = 0; i < needChangeObject.Length; i++)
        {
            needChangeObject[i].GetComponent<Renderer>().material.mainTexture = horror_Texture[i];
        }
    }

    public void ReturnRenderer()
    {
        for (int i = 0; i < needChangeObject.Length; i++)
        {
            needChangeObject[i].GetComponent<Renderer>().material.mainTexture = myTextrue[i];
        }
    }
}