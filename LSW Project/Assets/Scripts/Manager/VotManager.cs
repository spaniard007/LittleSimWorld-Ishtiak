using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VotManager : MonoBehaviour
{
    #region private variable
    [SerializeField] private int point;
    [SerializeField] private bool _IsResultShowed;
    #endregion

    #region public variable for store current dress info

    [Space(10)]
    public bool hasShirt;
    public Event_Catagory shirtCat;
    public int shirtpoint;

    [Space(10)]
    public bool hasPant;
    public Event_Catagory pantCat;
    public int pantpoint;

    [Space(10)]
    public bool hasFullSet;
    public Event_Catagory fullSetCat;
    public int fullSetpoint;

    [Space(10)]
    public bool hasJwellary;
    public Event_Catagory jwellaryCat;
    public int jwellarypoint;

    [Space(10)]
    public bool hasShoes;
    public Event_Catagory shoesCat;
    public int shoespoint;

    #endregion

    #region singletone
    public static VotManager instance = null;
    private void Awake()
    {
        if (VotManager.instance == null)
            VotManager.instance = this;
        else
            Destroy(this.gameObject);
    }
    #endregion

    private void Start()
    {
        point = 0;
       _IsResultShowed = false;
    }



    public void GetResult()
    {
        if (_IsResultShowed)
            return;

        VoteUi.instance.reciveCoinInfo.text = "<color=black>please wait....</color>" + "\n <color=#3873d1> analyzing your result </color>";
        VoteUi.instance.uiCoinCanvas.SetActive(true);

        StartCoroutine(ShowResult());
    }

    IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(2.5f);

        _IsResultShowed = true;

        //check shirt info
        if (hasShirt)
        {
            if (shirtCat == EventManager.instance.RunningEvent)
            {
                point += shirtpoint;
                Debug.Log("event & shirt category matched");
            }
            else
            {
                point += shirtpoint / 4;
            }
        }

        //check pant info
        if (hasPant)
        {
            if (pantCat == EventManager.instance.RunningEvent)
            {
                point += pantpoint;
                Debug.Log("event & pant category matched");
            }
            else
            {
                point += pantpoint / 4;
            }

        }

        //check FullSet info 
        if (hasFullSet)
        {
            if (fullSetCat == EventManager.instance.RunningEvent)
            {
                point += fullSetpoint;
                Debug.Log("event & fullSet category matched");
            }
            else
            {
                point += fullSetpoint / 4;
            }
        }

        //check Jwellary info 
        if (hasJwellary)
        {
            if (jwellaryCat == EventManager.instance.RunningEvent)
            {
                point += jwellarypoint;
                Debug.Log("event & jwellary category matched");
            }
            else
            {
                point += jwellarypoint / 4;
            }
        }

        //check Shoes info 
        if (hasShoes)
        {
            if (shirtCat == EventManager.instance.RunningEvent)
            {
                point += shoespoint;
                Debug.Log("event & jwellary category matched");
            }
            else
            {
                point += shoespoint / 4;
            }
        }


        //give player some coin based on score
        if ((hasShirt == true && hasPant == false && hasFullSet == false) || (hasShirt == false && hasPant == true && hasFullSet == false))
        {
            point = 0;
            if (EventManager.instance.IsPaidVoting == true)
            {
                VoteUi.instance.reciveCoinInfo.text = "your Current Event is\"<color=#f00202>" + EventManager.instance.RunningEvent +
                                                        "\" </color>" + "\n and your voting result is not satisfied. \n" +
                                                        "<color=#f70015> because you are not ready completely </color> \n" +
                                                        "you will loss<color=red> 10 </color>coin";
                DressList.Instance.Minus_Coin(10);
            }
            else
            {
                VoteUi.instance.reciveCoinInfo.text = "your Current Event is\"<color=#f00202>" + EventManager.instance.RunningEvent +
                                                       "\" </color>" + "\n and your voting result is not satisfied. \n" +
                                                       "<color=#f70015> because you are not ready completely </color>";
            }

            Debug.Log("player not ready completely");
            VoteUi.instance.uiCoinCanvas.SetActive(true);
        }
        else
        {
            if (point >= 15)
            {
                float _getCoin;
                if (EventManager.instance.IsPaidVoting == true)
                {
                    _getCoin = point * 4;
                }
                else
                    _getCoin = point * 1.5f;

                DressList.Instance.Add_Coin(_getCoin);
                VoteUi.instance.reciveCoinInfo.text = "your Current Event is\"<color=#f00202>" + EventManager.instance.RunningEvent + "\" </color>" + "\n and your voting result is satisfied. \n you will get<color=green> " + _getCoin + " </color>coin reward";
                VoteUi.instance.uiCoinCanvas.SetActive(true);
            }
            else if (point <= 14)
            {
                float _Coin;
                if (EventManager.instance.IsPaidVoting)
                {
                    _Coin = 5;

                    DressList.Instance.Minus_Coin(_Coin);
                    VoteUi.instance.reciveCoinInfo.text = "your Current Event is\"<color=#f00202>" + EventManager.instance.RunningEvent + "\" </color>" + "\n and your voting result is not satisfied. \n you will loss<color=red> " + _Coin + " </color>coin";
                }
                else
                {
                    _Coin = point * 2;

                    DressList.Instance.Add_Coin(_Coin);
                    VoteUi.instance.reciveCoinInfo.text = "your Current Event is\"<color=#f00202>" + EventManager.instance.RunningEvent + "\" </color>" + "\n and your voting result is not satisfied. \n you will get<color=green> " + _Coin + " </color>coin reward";
                }
                VoteUi.instance.uiCoinCanvas.SetActive(true);
            }
        }
        //set point velue to UI text
        VoteUi.instance.pointText.text = "your point: " + point.ToString();

        //finish current event
        EventManager.instance.EndEvent();
    }
}
