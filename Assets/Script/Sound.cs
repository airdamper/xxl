using UnityEngine;
using System.Collections;

public class Sound : Only<Sound> {

    public AudioClip click_animl;
    public AudioClip swap_animl;
    public AudioClip eliminate;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Click()
    {
        Play(click_animl);
    }
    public void Swap()
    {
        Play(swap_animl);
    }
    public void Eliminate()
    {
        Play(eliminate);
    }
    void Play(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, Vector3.zero);
    }
}
