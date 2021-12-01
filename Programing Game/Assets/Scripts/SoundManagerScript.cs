using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip jumpSound, walkSound, ambientMusic, battleMusic, selectSound;
    static AudioSource audioSrcJump, audioSrcWalk, audioSrcSelect, audioSrcAmbient, audioSrcBattle;
    // Start is called before the first frame update
    void Start()
    {
        audioSrcJump = GameObject.FindGameObjectWithTag("jump sfx").GetComponent<AudioSource>();
        audioSrcWalk = GetComponent<AudioSource>();
        audioSrcSelect = GameObject.FindGameObjectWithTag("select sfx").GetComponent<AudioSource>();
        audioSrcAmbient = GameObject.FindGameObjectWithTag("ambient music").GetComponent<AudioSource>();
        audioSrcBattle = GameObject.FindGameObjectWithTag("battle music").GetComponent<AudioSource>();
        jumpSound = Resources.Load<AudioClip>("Sounds/jumping sfx");
        walkSound = Resources.Load<AudioClip>("Sounds/walking sfx1");
        selectSound = Resources.Load<AudioClip>("Sounds/select sfx1");
        ambientMusic = Resources.Load<AudioClip>("Sounds/ambient music");
        battleMusic = Resources.Load<AudioClip>("Sounds/battle music");
        SoundManagerScript.playSound("ambient music");
    }

    public static void playSound(string clip)
    {
        switch (clip)
        {
            case "jumping sfx":
                audioSrcJump.PlayOneShot(jumpSound);
                break;
            case "select sfx":
                audioSrcSelect.PlayOneShot(selectSound);
                break;
            case "ambient music":
                audioSrcBattle.Stop();
                audioSrcAmbient.clip = ambientMusic;
                audioSrcAmbient.Play();
                audioSrcAmbient.loop = true;
                break;
            case "battle music":
                audioSrcAmbient.Stop();
                audioSrcBattle.clip = battleMusic;
                audioSrcBattle.Play();
                audioSrcBattle.loop = true;
                break;
        }
    }
}
