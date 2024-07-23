using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDeco : MonoBehaviour
{
    [SerializeField] ParticleSystem ParticleSystem;
    [SerializeField] SpriteRenderer Renderer;
    public bool destroyed = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void destroActivation() //make decoration explode when hit by areal damage
    {
        Renderer.enabled = false;
        ParticleSystem.Play();
        destroyed = true;
    }
}
