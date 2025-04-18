using System.Collections.Generic;
using UnityEngine;
//[ExecuteAlways]
public class FaceSelectorScript : MonoBehaviour
{

    //public List<GameObject> assets = new List<GameObject>();
    public List<Mesh> murMesh = new List<Mesh>(); 
    void Start()
    {
        //int pick = Random.Range(0, assets.Count);
        int pickMesh = Random.Range(0, murMesh.Count+1);
        

        //Instantiate(assets[pick], this.transform.position, Quaternion.identity);
         this.GetComponent<MeshFilter>().mesh = murMesh[pickMesh];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
