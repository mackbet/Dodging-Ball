using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_decore : MonoBehaviour
{
    private Transform _tr;
    public float speed=0.5f;    
    void Start()
    {
         _tr=GetComponent<Transform>();
    }

    void Update()
    {
        if(_tr.position.y<-1 || _tr.position.z<7)
        {
            Destroy(gameObject);
        }
        else
        {
            _tr.position+=new Vector3(0,0,-1)*speed;
        }
    }
}
