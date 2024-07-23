using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFxManager : MonoBehaviour
{
   

    [SerializeField] AudioSource soundFXobject;

    [SerializeField] AudioClip MouseOver;
    [SerializeField] AudioClip Select;
    [SerializeField] AudioClip Change;
    [SerializeField] float theVolume = 1;

    


    public void playSFXClip(AudioClip audioClip, Transform spawnTransform, float volume)  //called to make the sound effect prefab
    {
        AudioSource source = Instantiate(soundFXobject, spawnTransform.position, Quaternion.identity);
        source.clip = audioClip;
        source.volume = volume;
        source.Play();
        float audioClipLength = source.clip.length;
        Destroy(source.gameObject, audioClipLength);


    }



    public void playMouseOver()
    {
        playSFXClip(MouseOver, transform, theVolume);
    }

    public void playSelect()
    {
        playSFXClip(Select, transform, theVolume);
    }

    public void playChange()
    {
        playSFXClip(Change, transform, theVolume);
    }
}
