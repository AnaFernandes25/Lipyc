using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SceneController : MonoBehaviour
{
    private AudioSource[] audioSources;
    private PostProcessVolume[] postProcessVolumes;

    // Start is called before the first frame update
    void Start()
    {
        audioSources = GameObject.FindObjectsOfType<AudioSource>();

        foreach (AudioSource audio in audioSources)
        {
            if (audio.gameObject.tag == "music")
                audio.volume = SceneConfigs.musicVolume;

            if (audio.gameObject.tag == "effect")
                audio.volume = SceneConfigs.effectsVolume;

            audio.volume = Mathf.Clamp(audio.volume, 0, SceneConfigs.globalVolume);
        }

        // Post Process
        postProcessVolumes = GameObject.FindObjectsOfType<PostProcessVolume>();

        foreach (PostProcessVolume volume in postProcessVolumes)
        {
            if (volume.profile.TryGetSettings(out Bloom bloom))
                bloom.active = SceneConfigs.bloom;

            if (volume.profile.TryGetSettings(out ScreenSpaceReflections reflections))
                reflections.active = SceneConfigs.reflections;
        }
    }
}
