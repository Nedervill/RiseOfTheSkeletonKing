
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MenuCtrl : MonoBehaviour
{
    /*
     * Menu Ctrl, everything happens in the main menu, goes throught this script
     */

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] private Texture2D RegularCursor;
    [SerializeField] GameObject blackScreen;
    [SerializeField] CameraAnimatorReport cameraAnimReport;
    [SerializeField] GameObject theMenuTexts;
    [SerializeField] MenuMusic menuMusic;
    [SerializeField] GameObject pressAnyKey;
    [SerializeField] GameObject Credits;
    [SerializeField] GameObject MenuOptions;
    [SerializeField] GameObject HighScores;
    [SerializeField] GameObject DemoLevel;
    [SerializeField] GameObject Settings;
    bool titlesAreShowing = false;
   public int qcounter;

    private string SceneName;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("saved") == 0)
        {
            PlayerPrefs.SetInt("saved",50);
            PlayerPrefs.SetInt("MasterSoundVolume", 100);
            PlayerPrefs.SetInt("Musicolume", 100);
            PlayerPrefs.SetInt("SFXVolume", 100);
            PlayerPrefs.SetInt("killCamOn", 1);
        }
        else
        {
            startingSoundVolume("MasterSoundVolume");
            startingSoundVolume("Musicolume");
            startingSoundVolume("SFXVolume");
        }

        DemoLevel.SetActive(false);
        Cursor.SetCursor(RegularCursor, Vector2.zero, CursorMode.Auto);
        blackScreen.SetActive(true);
        theMenuTexts.SetActive(false);
       // Settings.SetActive(true);
        Settings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.anyKey) && (cameraAnimReport.panAlmostOver == false)) //speedup of the camerapan-intro
            Time.timeScale = 50f;

        if (Input.anyKeyDown) //check for revealling of the demo level option combination
        {
            //Now check if your key is being pressed
       
           if (Input.GetKeyDown(KeyCode.Q))
                qcounter++;

          else
            qcounter = 0;
         }
            
        if (qcounter == 3)
              DemoLevel.SetActive(true);

        if (cameraAnimReport.panAlmostOver) // stop near the the end of camera pan and reveal the menu options
        {
            Time.timeScale = 1f;
            theMenuTexts.SetActive(true);
            if (!titlesAreShowing)
             pressAnyKey.SetActive(false);
        }

        if (blackScreen.GetComponent<BlackScreen>().blacknow) //load the requested scene
            SceneManager.LoadScene(SceneName);
        if (titlesAreShowing && (Input.anyKey))
            stopCredits();
    }

    public void NewGameSelected() //called to load the normal game scene
    {
        SceneName = "NormalGame";
        //     MenuFxManager.Instance.playSelect(transform,1f);
        blackScreen.SetActive(true);
        blackScreen.GetComponent<BlackScreen>().GoOut();
        menuMusic.fadeMusic();

    }

    public void DemoLevelSelected() //called to load the DemoLevel scene
    {
        SceneName = "DemoLevel";
        //     MenuFxManager.Instance.playSelect(transform,1f);
        blackScreen.SetActive(true);
        blackScreen.GetComponent<BlackScreen>().GoOut();
        menuMusic.fadeMusic();

    }
    public void TutorialSelected() //called to load the NewTutorial1 scene
    {
        SceneName = "NewTutorial1";
        //     MenuFxManager.Instance.playSelect(transform,1f);
        blackScreen.SetActive(true);
        blackScreen.GetComponent<BlackScreen>().GoOut();
        menuMusic.fadeMusic();

    }

    public void runCredits() //called to show the credits
    {
        MenuOptions.GetComponent<MakeMenuAppear>().appearing = false;
        Credits.SetActive(true);
        Credits.GetComponent<MakeMenuAppear>().appearing = true;
        titlesAreShowing = true;
        pressAnyKey.SetActive(true);

    }

    public void stopCredits() //called to show the menu options and hide the credits
    {
        Credits.GetComponent<MakeMenuAppear>().appearing = false;
        MenuOptions.SetActive(true);
        MenuOptions.GetComponent<MakeMenuAppear>().appearing = true;
        titlesAreShowing = false;
        pressAnyKey.SetActive(false);
    }

    public void showHighSCores() //called to show the high scores
    {
        MenuOptions.GetComponent<MakeMenuAppear>().appearing = false;
        HighScores.SetActive(true);
        HighScores.GetComponent<MakeMenuAppear>().appearing = true;
     

    }

    public void hideHighSCores()  //called to show the menu options and hide the high scores
    {
        HighScores.GetComponent<MakeMenuAppear>().appearing = false;
        MenuOptions.SetActive(true);
        MenuOptions.GetComponent<MakeMenuAppear>().appearing = true;
       // titlesAreShowing = false;
      //  pressAnyKey.SetActive(false);
    }


    public void showSettings()   //called to show the high Settings
    {
        MenuOptions.GetComponent<MakeMenuAppear>().appearing = false;
        Settings.SetActive(true);
        //  Credits.GetComponent<RectTransform>().localPosition = Credits.GetComponent<MoveUp>().startingPos;
        Settings.GetComponent<MakeMenuAppear>().appearing = true;
        // scoresaAreShowing = true;
        //   pressAnyKey.SetActive(true);

    }
    public void hideSettings()   //called to show the menu options and hide the Settings
    {
        Settings.GetComponent<MakeMenuAppear>().appearing = false;
        MenuOptions.SetActive(true);
        MenuOptions.GetComponent<MakeMenuAppear>().appearing = true;
        // titlesAreShowing = false;
        //  pressAnyKey.SetActive(false);
    }

    void startingSoundVolume(string volumeType) //set the sound based on player prefs when the game loads
    {
        float volume = PlayerPrefs.GetInt(volumeType);
        float volumeToLog;
        if (volume != 0)
            volumeToLog = volume / 100f;
        else volumeToLog = 0.0001f; // log10 can never me 0
        audioMixer.SetFloat(volumeType, Mathf.Log10(volumeToLog) * 20f);
      //  updatevolumeBars();
    }
}
