using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasButtons : MonoBehaviour
{
	public Sprite soundON, soundOFF;
	
	void Start()
	{
		if(PlayerPrefs.GetString("sounds")=="No" && gameObject.name=="SoundControl")
		{
			GetComponent<Image>().sprite=soundOFF;
		}
	}
    public void RestartGame()
	{
		if(PlayerPrefs.GetString("sounds")!="No")
		{
			GetComponent<AudioSource>().Play();
		}
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	public void SoundsControll()
	{
		if(PlayerPrefs.GetString("sounds")=="No")
		{
			GetComponent<AudioSource>().Play();
			PlayerPrefs.SetString("sounds","Yes");
			GetComponent<Image>().sprite=soundON;
		}
		else
		{
			PlayerPrefs.SetString("sounds","No");
			GetComponent<Image>().sprite=soundOFF;
		}
	}
	public void Insta()
	{
		if(PlayerPrefs.GetString("sounds")!="No")
		{
			GetComponent<AudioSource>().Play();
		}
		Application.OpenURL("https://www.instagram.com/shanaev_667");
	}
}
