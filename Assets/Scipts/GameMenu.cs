using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {

    public void OnButtonStartDown()
    {
        SceneManager.LoadScene(1);
    }
    public void OnButtonExitDown()
    {
        //在unity编辑器下退出游戏
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else        
        Application.Quit();
#endif

    }
}
