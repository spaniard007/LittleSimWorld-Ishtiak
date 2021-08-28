using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateTimer : MonoBehaviour
{
    public static float timerForDate = 100f;

    private Image timerImage;
    // Start is called before the first frame update
    void Start()
    {
        //StartTimer();
        gameObject.SetActive(false);
    }
    
    public void StartTimer()
    {
        timerImage = GetComponent<Image>();
        timerImage.fillAmount = 1;
        CountDown();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CountDown()
    {
        if (timerForDate > 0f)
        {
            timerForDate--;
            timerImage.fillAmount = timerForDate / 100f;
            Invoke("CountDown",1f);
        }
        else
        {
            SceneChanger.instance.GoTOScene(4);
        }
    }
}
