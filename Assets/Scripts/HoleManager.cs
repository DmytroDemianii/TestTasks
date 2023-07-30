using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HoleManager : MonoBehaviour
{
    [SerializeField] private Navigator _navigator;
    [SerializeField] private Transform _skin;

    public MeshFilter meshFilter; // The Mesh-filter of the circular plane that has a hole in the center 
    public MeshCollider meshCollider;
    private Mesh _mesh;

    private List<int> VerticesIndx = new List<int>(); //Stores the number of vertices that the circular plane has. for using the list in your project you need to import

    private readonly List<Vector3> offSet = new List<Vector3>(); // Stores the distance of each vertex that has the proper distance to the hole navigator object 

    public float standardDistance;
    public float holeSize = 1f; // determines hole size

    private float targetSkinScaleX;
    private float targetSkinScaleZ;
    private float targetSkinScaleY;
    private float targetScale = 1f;
    private float targetRange;
    private float animDuration = 1.4f;
    
    
    void Start()
    {
        _mesh = meshFilter.mesh;

        for (int i = 0; i < _mesh.vertices.Length; i++)
        {
            var distance = Vector3.Distance(transform.position, _mesh.vertices[i]);


                VerticesIndx.Add(i);
                offSet.Add(_mesh.vertices[i] - transform.position);       
            
        }
    }

    void LateUpdate()
    {
        Vector3[] vertices = _mesh.vertices;

        for (int i = 0; i < VerticesIndx.Count; i++)
        {
            Vector3 currentVertex = vertices[VerticesIndx[i]];
            currentVertex.x = transform.position.x + offSet[i].x * holeSize;
            currentVertex.z = transform.position.z + offSet[i].z * holeSize;
            vertices[VerticesIndx[i]] = currentVertex;
        }

        _mesh.vertices = vertices;

        meshFilter.mesh = _mesh;

        meshCollider.sharedMesh = _mesh;
    }

    public void HoleUpScale(float scaleMultiplier)
    {
        targetSkinScaleX = _skin.localScale.x;
        targetSkinScaleZ = _skin.localScale.z;
        targetSkinScaleY = _skin.localScale.y;

        _navigator._moveSpeed *= 1.2f;

        targetScale *= scaleMultiplier;

        targetRange = _navigator.forceRange;
        targetRange *= (scaleMultiplier = 1.4f);

        _skin.DOScaleX(targetSkinScaleX *= (holeSize * 1.2f), animDuration);
        _skin.DOScaleZ(targetSkinScaleZ *= (holeSize * 1.2f), animDuration);


        DOTween.To(() => _navigator.forceRange, x => _navigator.forceRange = x, targetRange, animDuration);

        DOTween.To(() => holeSize, x => holeSize = x, targetScale, animDuration);
    }
}
