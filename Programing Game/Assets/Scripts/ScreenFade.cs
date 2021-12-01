using System.Collections;
using UnityEngine;

public class ScreenFade : MonoBehaviour {
    Animator anim;
    int counter = 0;
    bool isFading = false;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
    }
	public IEnumerator FadeToClear()
    {
        isFading = true;
        anim.SetTrigger("Fade In");

        counter = counter + 1;
        if (counter % 2 == 0)
            SoundManagerScript.playSound("ambient music");
        else if (counter % 2 != 0)
            SoundManagerScript.playSound("battle music");

        while (isFading)
            yield return null;

    }
    public IEnumerator FadeToBlack()
    {
        isFading = true;
        anim.SetTrigger("Fade Out");

        while (isFading)
            yield return null;

    }
    void AnimationComplete()
    {
        isFading = false;
    }
}
