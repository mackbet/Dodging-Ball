using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class create_decore : MonoBehaviour
{
    private Coroutine spawnBlocks;
    private int last_count=-100, x1, x2;
    List<GameObject> ListOfDecore_Desert,ListOfDecore_Forest;
    public GameObject desert_l,desert_r,rock1,rock2,saksaul_l,saksaul_r,forest_cost,forest_r1,forest_r2,forest_r3,forest_l1,forest_l2,forest_l3;
    public GameObject Decorations, kirpich;
    void Start()
    {
        ListOfDecore_Desert = new List<GameObject>();
        ListOfDecore_Desert.Add(desert_l);
        ListOfDecore_Desert.Add(desert_r);
        ListOfDecore_Desert.Add(rock1);
        ListOfDecore_Desert.Add(rock2);
        ListOfDecore_Desert.Add(saksaul_l);
        ListOfDecore_Desert.Add(saksaul_r);
        ListOfDecore_Desert.Add(forest_cost);
        ListOfDecore_Desert.Add(forest_r1);
        ListOfDecore_Desert.Add(forest_r2);
        ListOfDecore_Desert.Add(forest_r3);
        ListOfDecore_Desert.Add(forest_l1);
        ListOfDecore_Desert.Add(forest_l2);
        ListOfDecore_Desert.Add(forest_l3);
    }

    void Update()
    {
        Color_kirpich();
        if(kirpich.GetComponent<MeshRenderer>().material.color != Color.black && Decorations.transform.childCount==0)
            spawnBlocks=StartCoroutine(SpawnBlocks());
        else if(kirpich.GetComponent<MeshRenderer>().material.color == Color.black && Decorations.transform.childCount!=0)
            StopCoroutine(spawnBlocks);
        
    }
    private IEnumerator SpawnBlocks()
    {
        while(true)
        {
            Instantiate(ListOfDecore_Desert[Randomaizer_for_decor(x1,x2)]).transform.SetParent(Decorations.transform);
            Vector3 pos=Decorations.transform.GetChild(Decorations.transform.childCount-1).transform.position;
            if(kirpich.GetComponent<MeshRenderer>().material.color == Color.yellow)
            {
                Decorations.transform.GetChild(Decorations.transform.childCount-1).transform.position=new Vector3(pos.x+Randomaizer_for_other(0,25),pos.y,pos.z+5);
            }
            else if(kirpich.GetComponent<MeshRenderer>().material.color == Color.green)
            {
                Decorations.transform.GetChild(Decorations.transform.childCount-1).transform.position=new Vector3(pos.x+Randomaizer_for_other(0,22),pos.y,pos.z+5);
                Decorations.transform.GetChild(Decorations.transform.childCount-1).transform.Rotate(0,Randomaizer_for_other(0,360),0);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    int Randomaizer_for_decor(int a, int b)
    {
        int rand=UnityEngine.Random.Range(a,b);
        while(last_count==rand)
        {
            rand=UnityEngine.Random.Range(a,b);
        }
        last_count=rand;
        return rand;
    }
    int Randomaizer_for_other(int a, int b)
    {
        int rand=UnityEngine.Random.Range(a,b);
        return rand;
    }
    private void Color_kirpich()
    {
        if(kirpich.GetComponent<MeshRenderer>().material.color == Color.yellow)
        {
            x1=0;
            x2=6;
        }
        else if(kirpich.GetComponent<MeshRenderer>().material.color == Color.green)
        {
            x1=7;
            x2=13;
        }
    }
}
