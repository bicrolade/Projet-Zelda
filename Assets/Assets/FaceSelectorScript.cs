using System.Collections.Generic;
using UnityEngine;
//[ExecuteAlways]
public class FaceSelectorScript : MonoBehaviour
{

    public List<GameObject> assets = new List<GameObject>();
    public List<MeshFilter> murMesh = new List<MeshFilter>(); 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int pick = Random.Range(0, assets.Count);
        int pickMesh = Random.Range(0, murMesh.Count);
        
        Instantiate(assets[pick], this.transform.position, Quaternion.identity);
        MeshFilter oldMesh = this.GetComponent<MeshFilter>();
        oldMesh = murMesh[pickMesh];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
