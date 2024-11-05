using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseCanvas : UICanvas
{
    // Start is called before the first frame update
    public Sprite OnVolume;
    public Sprite OffVolume;

    [SerializeField] private Image buttonImage;
    void Start()
    {
  
        UpdateButtonImage();
  
    }
    public void Resume()
    {
        Time.timeScale = 1;
        UIManager.Instance.CloseUI<PauseCanvas>(0.2f);
        SoundManager.Instance.PlayVFXSound(2);    
    }

    public void HomeBtn()
    {
        UIManager.Instance.CloseAll();
        Time.timeScale = 1; 
        SceneManager.LoadScene("Home");
        SoundManager.Instance.PlayVFXSound(2);   
        UIManager.Instance.OpenUI<HomeCanvas>();
            
    }
    public void SoundBtn()
    {
        SoundManager.Instance.TurnOn = !SoundManager.Instance.TurnOn;
        UpdateButtonImage();
        SoundManager.Instance.PlayVFXSound(2);   
    }

    private void UpdateButtonImage()
    {
        if (SoundManager.Instance.TurnOn)
        {
            buttonImage.sprite = OnVolume;
        }
        else
        {
            buttonImage.sprite = OffVolume;
        }
    }
}

