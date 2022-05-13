using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Advertisements;

public class GameController : MonoBehaviour
{
    public bool prinudStart;
    private static bool adsActive;
    List<GameObject> ListOfBlocks;
    public GameObject center, left, right, side ,centerB, leftB, rightB, sideB, movingBlock1, movingBlock2, movingBlock3, ColorBlock, FastCenter, FastLeft, FastRight, FastSide, AllBlocks, Ball, platform, restartButton, logo, justScore, lights, mainLight, FastAnim, point1, point2, insta, hand;
    public GameObject UnderPlatform, kirpich, Decorations;
    public Color[] roadColor;
    public Color[] UnderPlatformClolor;
    public Color[] environmentColor;
    public Color[] UnderP_emission;
    public Material RedPlatform,DefaultPlatform;
    public int score=0,colorNumb;
    private int a=0, b=4, LastColorChanginInt=999, nightTIMER, RandBlockInt=999, LastBlockInt=999,fastTIMER;
    public float time=1.5f;
    private float rotate=15f,globalTIME=1.5f,hardTimeFast=1.2f,hardTimeMove=1.5f;
    private bool NOagain, colorChanged=false,returnPos;   
    private Coroutine spawnBlocks;
    public Text BestScoreTxt;
    public Text NowScoreTxt;
    private string gameId = "3806455", type="video";
    void Start()
    {   
        Advertisement.Initialize(gameId);
        if(Advertisement.IsReady(type) && adsActive){
                adsActive=false;
                Advertisement.Show(type);
        }
        ListOfBlocks = new List<GameObject>();
        ListOfBlocks.Add(center);
        ListOfBlocks.Add(left);
        ListOfBlocks.Add(right);
        ListOfBlocks.Add(side);
        ListOfBlocks.Add(centerB);
        ListOfBlocks.Add(leftB);
        ListOfBlocks.Add(rightB);
        ListOfBlocks.Add(sideB);
        ListOfBlocks.Add(movingBlock1);
        ListOfBlocks.Add(movingBlock2);
        ListOfBlocks.Add(movingBlock3);
        ListOfBlocks.Add(ColorBlock);
        ListOfBlocks.Add(FastCenter);
        ListOfBlocks.Add(FastLeft);
        ListOfBlocks.Add(FastRight);
        ListOfBlocks.Add(FastSide);
    }   

    void Update()
    {
        ScoreController();
        ColorChanging();
        FirstTouch();
        PlayingProcess();
    }
    private IEnumerator SpawnBlocks()
    {
        while(true)
        {
            Instantiate(ListOfBlocks[Randomaizer()]).transform.SetParent(AllBlocks.transform);
			yield return new WaitForSeconds(time);
		}
    }
    private void Rolling()
    {
        Ball.GetComponent<Transform>().Rotate(rotate,0,0);
    }
    private void FirstTouch()
    {
        if((Input.touchCount>0 && !EventSystem.current.currentSelectedGameObject && !NOagain) || prinudStart)//надо убрать
        {
            prinudStart=false;

            NOagain=true;
            spawnBlocks=StartCoroutine(SpawnBlocks());
            logo.SetActive(false);
            insta.SetActive(false);
            hand.SetActive(false);
            justScore.SetActive(true);
        }
    }
    private void PlayingProcess()
    {
        if(AllBlocks.transform.childCount>0 && CheckForEnd())
        {
            StopCoroutine(spawnBlocks);
            kirpich.GetComponent<MeshRenderer>().material.color = Color.black;
            for(int i=0;i<AllBlocks.transform.childCount;i++)
            {
                Destroy(AllBlocks.transform.GetChild(i).GetComponent<DefaultMotion>());
            }
            for(int j=0;j<Decorations.transform.childCount;j++)
            {
                Destroy(Decorations.transform.GetChild(j).GetComponent<Moving_decore>());
            }
            adsActive=true;
            restartButton.SetActive(true);
        }
        else if(AllBlocks.transform.childCount>0 && AllBlocks.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material.color!=Color.red)
        {
            Rolling();
        }
        ScoreAdder();
    }
    private void ScoreController()
    {
        if(score%50==0 && score/50!=LastColorChanginInt)
        {
            if(score/50%2==0)
            {
                Clearning(colorNumb);
                LastColorChanginInt=score/50;
                colorNumb=UnityEngine.Random.Range(4,4);
                if(time>0.6f && score!=0)
                {
                    time+=-0.1f;
                    globalTIME=time;
                }
            }
            else
            {
                LastColorChanginInt=score/50;
                colorNumb=UnityEngine.Random.Range(6,9);
                HardMode(colorNumb);
            }
        }
        ModeSupport(colorNumb);
    }
    private void ColorChanging()
    {
        if(Camera.main.backgroundColor!=environmentColor[colorNumb] || platform.GetComponent<MeshRenderer>().material.color!=roadColor[colorNumb] || UnderPlatform.GetComponent<MeshRenderer>().material.color!=UnderPlatformClolor[colorNumb])
        {
            Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, environmentColor[colorNumb], Time.deltaTime/0.5f);
            UnderPlatform.GetComponent<MeshRenderer>().material.color = Color.Lerp(UnderPlatform.GetComponent<MeshRenderer>().material.color, UnderPlatformClolor[colorNumb], Time.deltaTime/0.5f);
            platform.GetComponent<MeshRenderer>().material.color = Color.Lerp(platform.GetComponent<MeshRenderer>().material.color, roadColor[colorNumb], Time.deltaTime/0.5f);
            UnderPlatform.GetComponent<Renderer>().material.SetColor("_EmissionColor", UnderP_emission[colorNumb]);
        }
        if(AllBlocks.transform.childCount>0)
        {
            if(colorNumb==4)
            {
                kirpich.GetComponent<MeshRenderer>().material.color = Color.yellow;
            }
            else if(colorNumb==2)
            {
                kirpich.GetComponent<MeshRenderer>().material.color = Color.green;
            }
        }
    }
    private void HardMode(int m)
    {
        if(m==5)
        {
            ClearAllBlocks();
            Instantiate(lights).transform.SetParent(platform.transform);
            mainLight.SetActive(false);
            a=4;
            b=8;
            nightTIMER=0;
        }
        else if(m==6)
        {
            ClearAllBlocks();
            time=1.5f;
            a=8;
            b=11;
        }
        else if(m==7)
        {
            ClearAllBlocks();
            Ball.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            platform.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            a=11;
            b=12;
        }
        else if(m==8)
        {
            ClearAllBlocks();
            platform.GetComponent<MeshRenderer>().material=RedPlatform;
            platform.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            FastAnim.SetActive(true);
            time=hardTimeFast;
            a=12;
            b=16;
            fastTIMER=0;
        }
    }
    private void ModeSupport(int s)
    {
        if(s==5 && ((nightTIMER==20 && platform.transform.GetChild(0).childCount==4) || (nightTIMER==40 && platform.transform.GetChild(0).childCount==3)))
        {
            Destroy(platform.transform.GetChild(0).transform.GetChild(0).gameObject);
        }
        else if(s==7 && AllBlocks.transform.childCount>=1 && !colorChanged)
        {
            Ball.GetComponent<MeshRenderer>().material.color=AllBlocks.transform.GetChild(0).transform.GetChild(UnityEngine.Random.Range(0,AllBlocks.transform.GetChild(0).childCount)).GetComponent<MeshRenderer>().material.color;
            if(Ball.GetComponent<MeshRenderer>().material.color!=Color.white)
            {
                colorChanged=true;
            }
        }
        else if(s==8)
        {
            if((fastTIMER==15 && time==hardTimeFast) || (fastTIMER==30 && time==hardTimeFast-0.25))
            {
                time+=-0.25f;
            }
            Vector3 pos=Ball.transform.position;
            point1.transform.position=new Vector3(pos.x, pos.y, point1.transform.position.z);
            pos.y=0f;
            FastAnim.transform.position=pos;
            Ball.transform.position= Vector3.MoveTowards(Ball.transform.position, point1.transform.position, 0.1f);
        }
        else if(Ball.transform.position.z!=14 && NOagain)
        {
            Vector3 pos=Ball.transform.position;
            point2.transform.position=new Vector3(pos.x, pos.y, point2.transform.position.z);
            Ball.transform.position= Vector3.MoveTowards(Ball.transform.position, point2.transform.position, 0.5f);
        }
    }
    private void Clearning(int c)
    {
        if(c==5)
        {
            ClearAllBlocks();
            mainLight.SetActive(true);
            Destroy(platform.transform.GetChild(0).gameObject);
            paintWhite();
        }
        else if(c==6)
        {
            ClearAllBlocks();
            hardTimeMove+=-0.1f;
        }
        else if(c==7)
        {
            ClearAllBlocks();
            platform.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            Ball.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            Ball.GetComponent<MeshRenderer>().material.color=Color.white;
        }
        else if(c==8)
        {
            FastAnim.SetActive(false);
            ClearAllBlocks();
            platform.GetComponent<MeshRenderer>().material=DefaultPlatform;
            hardTimeFast+=-0.1f;
        }
        time=globalTIME;
        a=0;
        b=4;
    }
    private void NiggA()
    {
        for(int i=0;i<AllBlocks.transform.childCount;i++)
        {
            for(int j=0;j<AllBlocks.transform.GetChild(i).childCount;j++)
            {
                AllBlocks.transform.GetChild(i).GetChild(j).GetComponent<MeshRenderer>().material.color=Color.black;
                AllBlocks.transform.GetChild(i).GetChild(j).GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            }
        }
    }
    private void paintWhite()
    {
        for(int i=0;i<AllBlocks.transform.childCount;i++)
        {
            for(int j=0;j<AllBlocks.transform.GetChild(i).childCount;j++)
            {
                AllBlocks.transform.GetChild(i).GetChild(j).GetComponent<MeshRenderer>().material.color=Color.white;
                AllBlocks.transform.GetChild(i).GetChild(j).GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            }
        }
    }
    int Randomaizer()
    {
        if(b-a==1)
        {
            return a;
        }
        else
        {
            while(RandBlockInt==LastBlockInt)
            {
                RandBlockInt=UnityEngine.Random.Range(a,b);
            }
            LastBlockInt=RandBlockInt;
            return RandBlockInt;
        }
    }
    private void ClearAllBlocks()
    {
        for(int i=0;i<AllBlocks.transform.childCount;i++)
        {
            Destroy(AllBlocks.transform.GetChild(i).gameObject);
        }
    }
    bool CheckForEnd()
    {
        if(AllBlocks.transform.GetChild(0).tag=="FastBlocks" && AllBlocks.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material.color==Color.white)
        {
            return true;
        }
        else
        {
            for(int k=0;k<AllBlocks.transform.GetChild(0).childCount;k++)
            {
                if(AllBlocks.transform.GetChild(0).transform.GetChild(k).GetComponent<MeshRenderer>().material.color==Color.red)
                {
                    return true;
                }
            }
            return false;
        }
    }
    private void ScoreAdder()
    {
        if(AllBlocks.transform.childCount>0 && AllBlocks.transform.GetChild(0).transform.position.z+1f<Ball.transform.position.z){
            score++;
            nightTIMER++;
            fastTIMER++;
            AllBlocks.transform.GetChild(0).SetParent(null);
            colorChanged=false;
        }
        if(PlayerPrefs.GetInt("score")<score)
            PlayerPrefs.SetInt("score",score);
        NowScoreTxt.text="<size=30>Now:</size>" + score;
        BestScoreTxt.text="<size=40>Best:</size>" + PlayerPrefs.GetInt("score");
    }
}