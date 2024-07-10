using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject menuInicial, menuOptions, rawImage;
    public AudioSource selectSound;
    public string newGameScene;
    private Animator animatorRawImage;

    public Dropdown resolution, quality;
    public InputField textFPS;
    public Toggle limitFPS, windowMode, bloom, reflection, autoSave;
    public Slider globalVol, musicsVol, effectVol;

    // Start is called before the first frame update
    void Start()
    {
        if (rawImage == null || menuOptions == null || menuInicial == null)
        {
            Debug.LogError("One or more GameObjects are not assigned in the inspector");
            return;
        }

        ApplyConfigs();

        rawImage.SetActive(false);
        menuOptions.SetActive(false);
        menuInicial.SetActive(false);
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
            animatorRawImage.SetTrigger("fadeIn");
            menuInicial.SetActive(true);
        }
    }

    public void Options()
    {
        menuInicial.SetActive(false);
        menuOptions.SetActive(true);
    }

    public void Salvar()
    {
        SaveConfigs();
        ReturnMenuInicial();
    }

    public void ReturnMenuInicial()
    {
        menuInicial.SetActive(true);
        menuOptions.SetActive(false);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void ApplyConfigs()
    {
        var configs = LoadConfigs();

        if (configs == null)
        {
            Debug.LogWarning("Configs are null in ApplyConfigs");
            return;
        }

        // Aplicar a resolução e modo de janela
        if (configs.Resolution != null)
        {
            Screen.SetResolution(configs.Resolution.Width, configs.Resolution.Height, !configs.WindowMode);
        }

        // Aplicar o preset da qualidade
        QualitySettings.SetQualityLevel((int)configs.Quality);

        // Aplicar Limite de FPS
        if (configs.LimitFPS != null)
        {
            Application.targetFrameRate = configs.LimitFPS.Limit ? configs.LimitFPS.FPS : -1;
        }

        // Ativar o volume
        SceneConfigs.effectsVolume = configs.EffectsVolume;
        SceneConfigs.globalVolume = configs.GlobalVolume;
        SceneConfigs.musicVolume = configs.MusicVolume;
        SceneConfigs.autoSave = configs.AutoSave;
        SceneConfigs.bloom = configs.Bloom;
        SceneConfigs.reflections = configs.Reflection;

        // Atualizar UI
        UpdateUI(configs);
    }

    private ConfigModel LoadConfigs()
    {
        try
        {
            var fileDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Lypic", "ConfigData.Save");
            if (!File.Exists(fileDirectory))
                return null;

            using (var file = File.OpenRead(fileDirectory))
            {
                var binaryFormatter = new BinaryFormatter();
                return (ConfigModel)binaryFormatter.Deserialize(file);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to load config: " + ex.Message);
            return null;
        }
    }

    private void SaveConfigs()
    {
        var resolutionModel = new Resolution();

        switch (resolution.value)
        {
            case 0:
                resolutionModel.Width = 800;
                resolutionModel.Height = 600;
                break;
            case 1:
                resolutionModel.Width = 1280;
                resolutionModel.Height = 720;
                break;
            case 2:
                resolutionModel.Width = 1920;
                resolutionModel.Height = 1080;
                break;
            case 3:
                resolutionModel.Width = 2560;
                resolutionModel.Height = 1440;
                break;
            case 4:
                resolutionModel.Width = 3840;
                resolutionModel.Height = 2160;
                break;
        }

        var configs = new ConfigModel()
        {
            AutoSave = autoSave.isOn,
            Bloom = bloom.isOn,
            WindowMode = windowMode.isOn,
            GlobalVolume = globalVol.value,
            EffectsVolume = effectVol.value,
            MusicVolume = musicsVol.value,
            LimitFPS = new LimitFPS()
            {
                FPS = int.Parse(textFPS.text),
                Limit = limitFPS.isOn
            },
            Quality = (Quality)quality.value,
            Resolution = resolutionModel
        };

        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Lypic");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var filePath = Path.Combine(path, "ConfigData.Save");

        var binaryFormatter = new BinaryFormatter();
        using (var file = File.Create(filePath))
        {
            binaryFormatter.Serialize(file, configs);
        }

        ApplyConfigs();
    }

    private void UpdateUI(ConfigModel configs)
    {
        var option = resolution.options.Where(x => x.text == $"{configs.Resolution.Width}x{configs.Resolution.Height}").FirstOrDefault();
        resolution.value = resolution.options.IndexOf(option);
        quality.value = (int)configs.Quality;
        textFPS.text = configs.LimitFPS.FPS.ToString();
        windowMode.isOn = configs.WindowMode;
        bloom.isOn = configs.Bloom;
        reflection.isOn = configs.Reflection;
        autoSave.isOn = configs.AutoSave;
        globalVol.value = configs.GlobalVolume;
        effectVol.value = configs.EffectsVolume;
        musicsVol.value = configs.MusicVolume;
    }

}
