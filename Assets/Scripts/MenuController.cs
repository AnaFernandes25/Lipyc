using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MenuController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject menuOpcoes, rawImage;
    public AudioSource selectSound;
    private Animator animatorRawImage;

    // Start is called before the first frame update
    void Start()
    {
        rawImage.SetActive(false);
        animatorRawImage = rawImage.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!videoPlayer.isPlaying && Input.anyKeyDown)
        {
            selectSound.Play();
            videoPlayer.Play();
            rawImage.SetActive(true );
            animatorRawImage.SetTrigger("fadeIn");
            menuOpcoes.SetActive(true);
        }
    }
}
