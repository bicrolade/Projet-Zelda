using NUnit.Framework.Internal;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class FaceSelectorScript : MonoBehaviour
{
    [Header("Faces")]
    public List<GameObject> faces = new List<GameObject>();
    public GameObject top;
    //public List<GameObject> assets = new List<GameObject>();

    [Header("Meshes")]
    public List<Mesh> murMesh = new List<Mesh>(); 
    void Start()
    {
        
        

        //Instantiate(assets[pick], this.transform.position, Quaternion.identity);
       
        for (int i = 0; i < faces.Count; i++)
        { 
            int pickMesh = Random.Range(1, murMesh.Count);
            //Debug.Log(pickMesh);
            faces[i].GetComponent<MeshFilter>().mesh = murMesh[pickMesh];
            int choose = Random.Range(0, 2);
            if(choose == 1)
            {
                choose = 180;
            }
            faces[i].transform.eulerAngles += new Vector3(choose, 0, 0);
        }
        int chooseTopOrientation = Random.Range(0, 5);
        chooseTopOrientation *= 90;
        Debug.Log(chooseTopOrientation);
        top.transform.eulerAngles += new Vector3(0, chooseTopOrientation, 0);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
