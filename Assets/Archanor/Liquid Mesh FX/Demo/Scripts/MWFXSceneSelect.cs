using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace MeshWaterFX
{

public class MWFXSceneSelect : MonoBehaviour
{
	
	public bool GUIHide = false;
	public bool GUIHide2 = false;
	
    public void LoadSceneMWFXFlowing()
    {
        SceneManager.LoadScene("MWFXFlowing");
    }
    public void LoadSceneMWFXMissiles()
    {
        SceneManager.LoadScene("MWFXMissiles");
    }
    public void LoadSceneMWFXExplosions()
    {
        SceneManager.LoadScene("MWFXExplosions");
    }
    public void LoadSceneMWFXSprays()
    {
        SceneManager.LoadScene("MWFXSprays");
    }
    public void LoadSceneMWFXDirectional()
    {
        SceneManager.LoadScene("MWFXDirectional");
    }

	void Update ()
	 {
 
     if(Input.GetKeyDown(KeyCode.L))
	 {
         GUIHide = !GUIHide;
     
         if (GUIHide)
		 {
             GameObject.Find("CanvasSceneSelect").GetComponent<Canvas> ().enabled = false;
         }
		 else
		 {
             GameObject.Find("CanvasSceneSelect").GetComponent<Canvas> ().enabled = true;
         }
     }
	      if(Input.GetKeyDown(KeyCode.J))
	 {
         GUIHide2 = !GUIHide2;
     
         if (GUIHide2)
		 {
             GameObject.Find("CanvasMissiles").GetComponent<Canvas> ().enabled = false;
         }
		 else
		 {
             GameObject.Find("CanvasMissiles").GetComponent<Canvas> ().enabled = true;
         }
     }
	}
}
}