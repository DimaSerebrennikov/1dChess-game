using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    //=========================================================== Редактор
    public GameObject inPause;
    public GameObject aboutMeMenu;
    public GameObject tutorialMenu;
    public TextMeshProUGUI bestRecordTMP;
    public TextMeshProUGUI soundText;
    public FailScreenS failScreen;
    //=========================================================== Редактор
    public static bool firstTutrial = true;
    float targetAspect;
    private void Awake()
    {
        targetAspect = 0.462f;
        if (firstTutrial)
        {
            ClickOnTutorial();
            firstTutrial = false;
        }
        CheckSound();
        Adaption();
    }
    public void ClickOnMenuButton()
    {
        inPause.SetActive(true);
        Time.timeScale = 0f;
        UpdateBestRecord();
    }
    public void ClickOutOfMenu()
    {
        inPause.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Restart()
    {
        inPause.SetActive(false);
        Time.timeScale = 1f;
        failScreen.Fail();
    }
    public void Sound()
    {
        if (AudioListener.volume == 1f)
        {
            soundText.text = "II";
            AudioListener.volume = 0f;
            PlayerPrefs.SetInt("sound", 1);
        }
        else
        {
            soundText.text = "}}";
            AudioListener.volume = 1f;
            PlayerPrefs.SetInt("sound", 0);
        }
    }
    void CheckSound()
    {
        if (PlayerPrefs.GetInt("sound") == 1)
        {
            AudioListener.volume = 0f;
            soundText.text = "II";
        }
    }
    public void ClickOnTutorial()
    {
       tutorialMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ClickAboutMe()
    {
        aboutMeMenu.SetActive(true);
    }
    public void ClickOutOfAboutMeOrTutorial()
    {
        aboutMeMenu.SetActive(false);
        tutorialMenu.SetActive(false);
        if (inPause.activeSelf == false)
        {
            Time.timeScale = 1f;
        }
    }
    public void UpdateBestRecord()
    {
        if (PlayerPrefs.HasKey("record"))
        {
            bestRecordTMP.text = PlayerPrefs.GetInt("record").ToString();
        }
    }
    void Adaption()
    {
        float aspectHW = (float)Screen.height/(float)Screen.width;
        
        if (aspectHW < targetAspect)
        {
            gameObject.GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
        }
    }
}
