using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenCtrl : MonoBehaviour
{
    //the win screen main controller
    [SerializeField] private Texture2D RegularCursor;
    [SerializeField] GameObject blackScreen;
    [SerializeField] MenuMusic winScreenMusic;
    [SerializeField] GameObject backToMenu;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(RegularCursor, Vector2.zero, CursorMode.Auto);
        blackScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (blackScreen.GetComponent<BlackScreen>().blacknow)
            SceneManager.LoadScene("MainMenu");
    }

    public void MainMenuPressed()
    {
       
        //     MenuFxManager.Instance.playSelect(transform,1f);
        blackScreen.SetActive(true);
        blackScreen.GetComponent<BlackScreen>().GoOut();
        winScreenMusic.fadeMusic();

    }
}
