using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRenderChanger : MonoBehaviour
{
    public bool isChanging;

    Light flashing_Light;
    OKMeshChange oKMesh;

    void Start()
    {
        flashing_Light = GetComponent<Light>();
        oKMesh = GameObject.Find("RenderChangeManager").GetComponent<OKMeshChange>();
    }

    private void Update()
    {
        MNQFlashing();
    }

    void MNQFlashing()
    {
        int i = oKMesh.needChangeObject.Length;

        if (isChanging)
        {
            oKMesh.ChangeRenderer();
        }
        else
        {
            oKMesh.ReturnRenderer();
        }
    }
}