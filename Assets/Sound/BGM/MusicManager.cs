using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    //public static MusicManager Instance;

    [SerializeField] AudioClip planningMusic;
    [SerializeField] AudioClip[] battleMusic;

    [SerializeField] AudioClip victoryMusic;
    [SerializeField] AudioClip loseMusic;

    AudioSource BGM;
    AudioClip newMusic;
    [SerializeField] float volumeChange = 0.01f;
    [SerializeField] float silenceTime = 1f;
    [SerializeField] float CurrentSilence;
    
    public bool levelEnd = false;
    

    // Start is called before the first frame update
    void Start()
    {
        BGM = GetComponent<AudioSource>();
        BGM.clip = planningMusic;
        newMusic = planningMusic;
    }

    // Update is called once per frame
    void Update()
    {
        if (!levelEnd)
        {


            if ((newMusic != BGM.clip) && (BGM.volume > 0))  // if the music is switched sllowly make the current track sillent
                BGM.volume -= volumeChange*Time.deltaTime;

            else if (BGM.volume == 0)  // if the music is switched and the previous track is silensed
            {
                if (CurrentSilence > silenceTime) //play the new track after a small dellay
                {
                    BGM.Stop();
                    BGM.clip = newMusic;
                    BGM.volume = 1;
                    BGM.Play();
                }
                else
                    CurrentSilence += Time.deltaTime;


            }

            if ((newMusic == BGM.clip) && (BGM.volume < 1))
                BGM.volume += volumeChange;
        }
        else
            BGM.volume -= volumeChange;  //desend in vlolume if the lvel ends
    }

    public void switchMusic() // checks what game status is currently and switches the BGM to the appropriate one
    {
        CurrentSilence = 0;
        if (winOrLose.victory == true)
        {
            newMusic = victoryMusic;
            return;
        }

        if (winOrLose.defeat == true)
            newMusic = loseMusic;
      
        else if (RoundStatus.currentgameStatus == RoundStatus.CurrrentGameStatus.Battle)
        {
            int trackNum;
            do { trackNum = Random.Range(0, battleMusic.Length); } //makes sure not to repeat the battle music from previous round
            while (trackNum == GameStatus.lastBattleTrackUsed);

            GameStatus.lastBattleTrackUsed = trackNum;
            newMusic = battleMusic[trackNum];
        }
        
    }

    public void fadeMusic()
    {
        levelEnd = true;
    }


}
