using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;
    bool  once_click=false;


    private void Awake()
    {
        if (SceneChanger.instance == null)
            SceneChanger.instance = this;
        else
            Destroy(this);
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        //set screen light to never sleep
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Check_Scene();
        }
    }

    void Check_Scene()
    {
        int num=SceneManager.GetActiveScene().buildIndex;
        Double_Chekc_Mech(num);
    }

    void Double_Chekc_Mech(int num)
    {
        if (once_click)
        {
            CancelInvoke("Once_not_click");
            GoTOScene(num-1);
        }
        else
        {
            once_click = true;
            Invoke("Once_not_click", .5f);
        }
    }

    void Once_not_click()
    {
        once_click = false;
    }

    public void GoTOScene(int i)
    {
        Audiomanager.instance.PlayBtnSound();
        if (i > -1)
            SceneManager.LoadScene(i);
        else
            Application.Quit();
            Debug.Log("Quit game");
    }
}
