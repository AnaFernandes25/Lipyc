using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject menuOpcoes, rawImage;
    public AudioSource selectSound;
    public Animator animatorRawImage;
    public Button ButtonPlay;



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
            rawImage.SetActive(true);
            animatorRawImage.SetTrigger("fadein");
            menuOpcoes.SetActive(true);
        }

    }
    public void Play()
    {
        SceneManager.LoadScene("intro");
    }
}
