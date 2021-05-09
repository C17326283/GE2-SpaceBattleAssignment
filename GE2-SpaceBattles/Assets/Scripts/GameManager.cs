using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Control core things like closing game*/
public class GameManager : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Quit();
        }
    }
    
    public void Quit()
    {
        print("escape");
        Application.Quit();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; 
        #endif
    }
}
