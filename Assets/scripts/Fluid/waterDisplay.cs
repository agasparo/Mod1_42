using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterDisplay : MonoBehaviour
{

    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public void DrawMesh(MeshData meshdata)
    {

        meshFilter.sharedMesh = meshdata.CreateMesh();
    }

}

