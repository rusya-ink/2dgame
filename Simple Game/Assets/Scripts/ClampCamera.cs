using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampCamera : MonoBehaviour
{
    //Camera variables
    [SerializeField]Transform player;
    [SerializeField]float minX=-13.8f;
    [SerializeField]float maxX=12.8f;
    [SerializeField]float minY=0.7f;
    [SerializeField]float maxY=12.2f;
    Camera cam;
    Transform tr;

    // Start is called before the first frame update
    void Awake(){
    	tr = GetComponent<Transform>();
    	cam = GetComponent<Camera>();
    }

    // Update is called once per frame (moving camera without going out of borders)
    void Update()
    {
    	float x = Mathf.Clamp(player.position.x, minX, maxX);
    	float y = Mathf.Clamp(player.position.y, minY, maxY);
        tr.position = new Vector3(x, y, tr.position.z);
    }
}
