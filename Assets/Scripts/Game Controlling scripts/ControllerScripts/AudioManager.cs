using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour

{
    public static AudioManager instance;
    public AudioClip [] sounds;
    public AudioClip [] femaleCustSounds;
    public AudioClip [] maleCustSounds;
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
       
        source.playOnAwake = false;
        //button.onClick.AddListener(() => PlaySoud());
    }



    // Update is called once per frame

    public void PlaySoud(string Aud)
    {
        switch (Aud)
        {
            case "btn":
                source.clip = sounds[0];
                source.PlayOneShot(sounds[0]);
                break;
            case "maleHappy":
                source.clip = maleCustSounds[0];
                source.PlayOneShot(maleCustSounds[0]);
                break;
            case "maleSad":
                source.clip = maleCustSounds[1];
                source.PlayOneShot(maleCustSounds[1]);
                break;
            case "maleAngry":
                source.clip = maleCustSounds[2];
                source.PlayOneShot(maleCustSounds[2]);
                break;
            case "femaleHappy":
                source.clip = femaleCustSounds[0];
                source.PlayOneShot(femaleCustSounds[0]);
                break;
            case "femaleSad":
                source.clip = femaleCustSounds[1];
                source.PlayOneShot(femaleCustSounds[1]);
                break;
            case "femaleAngry":
                source.clip = femaleCustSounds[2];
                source.PlayOneShot(femaleCustSounds[2]);
                break;
            case "OrderAccept":
                source.clip = sounds[1];
                source.PlayOneShot(sounds[1]);
                break;
            case "SeatReached":
                source.clip = sounds[2];
                source.PlayOneShot(sounds[2]);
                break;
        }
        
    }

}