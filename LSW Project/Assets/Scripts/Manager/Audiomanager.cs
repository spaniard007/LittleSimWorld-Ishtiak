using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanager : MonoBehaviour
{
    public AudioSource bgSound;
    public AudioSource btnSound;
    public AudioSource resultSound;
    
    #region singletone
    public static Audiomanager instance = null;
    private void Awake()
    {
        if (Audiomanager.instance == null)
            Audiomanager.instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //bgSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SoundOn()
    {
        bgSound.enabled = true;
        btnSound.enabled = true;
        resultSound.enabled = true;
    }
    
    public void SoundOff()
    {
        bgSound.enabled = false;
        btnSound.enabled = false;
        resultSound.enabled = false;
    }

    public void PlayBtnSound()
    {
        if (btnSound != null)
        {
            btnSound.Play();
        }
    }
    public void PlayResultSound()
    {
        if (resultSound != null)
        {
            resultSound.Play();
        }
    }
}
