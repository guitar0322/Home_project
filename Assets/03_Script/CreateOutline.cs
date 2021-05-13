using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOutline : MonoBehaviour
{
    // Start is called before the first frame update
    public bool createFlag;
    public Material oldMat;
    public Material outlineMat;
    MeshRenderer meshRenderer;
    void Start()
    {
        createFlag = false;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(createFlag == true)
        {
            meshRenderer.material = outlineMat;
        }
        else
        {
            meshRenderer.material = oldMat;
        }
    }
}
