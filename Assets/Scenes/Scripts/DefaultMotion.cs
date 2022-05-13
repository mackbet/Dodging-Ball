using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultMotion : MonoBehaviour
{
    List<Material> Colors;
    public Material blue,green,yellow,violet;
    private Transform _tr;
    public float speed=0.5f;
    private int side,RandInt=999, LastInt=999;
    void Start()
    {
        Colors = new List<Material>();
        Colors.Add(blue);
        Colors.Add(green);
        Colors.Add(yellow);
        Colors.Add(violet);
        RandomValues();
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
        Moving();
        Painting();
    }
    private void Moving()
    {
        if(gameObject.tag=="FastBlocks")
        {
            speed=1f;
        }
        else if(_tr.GetChild(0).GetComponent<MeshRenderer>().material.color!=Color.white && _tr.GetChild(0).GetComponent<MeshRenderer>().material.color!=Color.black && _tr.childCount==1)
        {
            speed=0.4f;
            if(_tr.GetChild(0).transform.position.x<=-1.5f)
            {
                side=1;
            }
            else if(_tr.GetChild(0).transform.position.x>=1.5f)
            {
                side=-1;
            }
            _tr.GetChild(0).transform.position+=new Vector3(side,0,0)*0.05f;
        }
        else
        {
            speed=0.5f;
        }
    }
    
    private void RandomValues()
    {
        int rand=UnityEngine.Random.Range(0,2);
        if(rand%2==0)
        {
            side=-1;
        }
        else
        {
            side=1;
        }
    }
    private void Painting()
    {
        if(_tr.childCount==3)
        {
            for(int t=0;t<3;t++)
            {
                if(_tr.GetChild(t).GetComponent<MeshRenderer>().material.color==Color.white)
                {
                    _tr.GetChild(t).GetComponent<MeshRenderer>().material=Colors[Randomaizer()];
                }
            }
        }
    }
    int Randomaizer()
    {
        while(RandInt==LastInt)
        {
            RandInt=UnityEngine.Random.Range(0,4);
        }
        LastInt=RandInt;
        return RandInt;
    }
}
