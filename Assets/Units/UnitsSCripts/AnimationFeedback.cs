
using UnityEngine;

public class AnimationFeedback : MonoBehaviour
{
    public bool hit = false;
    public bool shoot = false;
    public bool recovered = false;
    public bool teleport = false;
    public bool stoppedAttacking = false;
    public bool specialHit = false;
    public bool reanimated = false;
    public bool goNextLevel = false;
    [SerializeField] Stats stats;
    // Start is called before the first frame update


    public void Hit(string boom)
    {
        hit = true;
    }

    public void Shoot(string boom)
    {
        shoot = true;
    }
    public void Recovered(string boom)
    {
        recovered = true;
    }
    public void Teleport(string boom)
    {
        teleport = true;

    }
    public void StoppedAttacking()
    {
        stoppedAttacking = true;
    }

    public void SpecialHit()
    {
        specialHit = true;
    }

    public void Reanimated()
    {
        reanimated = true;
    }

    public void playSpecialAttackSound()
    {
        SoundFXManager.Instance.playSFXClip(stats.specialAttackSound, transform, 0.5f);
    }

    public void playRangedAttackSound()
    {
        SoundFXManager.Instance.playSFXClip(stats.rangedAttackSound, transform, 0.5f);
    }

    public void playMeeleAttackSound()
    {
        SoundFXManager.Instance.playSFXClip(stats.normalAttackSound, transform, 0.5f);
    }

    public void goNext()
    {
        goNextLevel = true;
    }

}