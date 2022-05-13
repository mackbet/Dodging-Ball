using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ballController : MonoBehaviour
{
    
    public float speed;
    private bool GameOver, start;
    private Transform _BallTransform;
    public GameObject YellowAnim, _canvas;
    private RectTransform CanvasRectTransform;
    void Start()
    {
        _BallTransform=GetComponent<Transform>();
        CanvasRectTransform = _canvas.GetComponent<RectTransform>();
    }
    void Update()
    {
        if(Input.touchCount>0 && !EventSystem.current.currentSelectedGameObject && !GameOver && start)
        {
            Touch touch=Input.GetTouch(0);
            if(touch.position.x>CanvasRectTransform.rect.width/2 && _BallTransform.position.x<2.5f)
            {
                _BallTransform.position+=new Vector3(1,0,0)*speed;
            }
            else if(touch.position.x<CanvasRectTransform.rect.width/2 && _BallTransform.position.x>-2.5f)
            {
                _BallTransform.position+=new Vector3(1,0,0)*-speed;    
            }
        }
        else if(!GameOver)
        {
            if(_BallTransform.position.x>0)
            {
                _BallTransform.position+=new Vector3(1,0,0)*-speed;
            }
            else if(_BallTransform.position.x<0)
            {
                _BallTransform.position+=new Vector3(1,0,0)*speed;
            }
        }
        if(_BallTransform.position.z==13f)
        {
            start=true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Blocks"){
            Destroy(collision.gameObject.GetComponent<Rigidbody>());
            Destroy(collision.gameObject.GetComponent<DefaultMotion>());
            for(int j=0;j<collision.gameObject.transform.childCount;j++)
            {
                collision.gameObject.transform.GetChild(j).GetComponent<MeshRenderer>().material.color=Color.red;
            }
            Instantiate(YellowAnim, new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, collision.contacts[0].point.z), Quaternion.identity);
            GameOver=true;
            if(PlayerPrefs.GetString("sounds")!="No")
            {
                GetComponent<AudioSource>().Play();
            }
        }
        else if(collision.gameObject.tag=="ColorBlocks")
        {
            Destroy(collision.gameObject.GetComponent<Rigidbody>());
            Destroy(collision.gameObject.transform.parent.GetChild(0).GetComponent<Rigidbody>());
            Destroy(collision.gameObject.transform.parent.GetChild(1).GetComponent<Rigidbody>());
            Destroy(collision.gameObject.transform.parent.GetChild(2).GetComponent<Rigidbody>());
            if(collision.gameObject.GetComponent<MeshRenderer>().material.color!=GetComponent<MeshRenderer>().material.color)
            {
                Destroy(collision.gameObject.transform.parent.GetComponent<DefaultMotion>());
                collision.gameObject.GetComponent<MeshRenderer>().material.color=Color.red;
                collision.gameObject.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
                Instantiate(YellowAnim, new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, collision.contacts[0].point.z), Quaternion.identity);
                GameOver=true;
                GetComponent<AudioSource>().Play();
            }
        }
        else if(collision.gameObject.tag=="FastBlocks")
        {
            Destroy(collision.gameObject.GetComponent<Rigidbody>());
            Destroy(collision.gameObject.GetComponent<DefaultMotion>());
            for(int j=0;j<collision.gameObject.transform.childCount;j++)
            {
                collision.gameObject.transform.GetChild(j).GetComponent<MeshRenderer>().material.color=Color.white;
                collision.gameObject.transform.GetChild(j).GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            }
            Instantiate(YellowAnim, new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, collision.contacts[0].point.z), Quaternion.identity);
            GameOver=true;
            GetComponent<AudioSource>().Play();
        }
	}
}