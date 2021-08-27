using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoteUi : MonoBehaviour
{
    #region ui element
    public Text pointText;
    public Text reciveCoinInfo;

    public GameObject uiCoinCanvas;
    #endregion

    #region private variable
    [SerializeField] private SpriteRenderer shirt;
    [SerializeField] private SpriteRenderer pant;
    [SerializeField] private SpriteRenderer fullSet;
    [SerializeField] private SpriteRenderer necklaces;
    [SerializeField] private SpriteRenderer shoes;
    #endregion

    #region singletone
    public static VoteUi instance = null;
    private void Awake()
    {
        if (VoteUi.instance == null)
            VoteUi.instance = this;
        else
            Destroy(this.gameObject);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Set_Curent_Dress();
    }

    
    void Set_Curent_Dress()
    {
        Sprite shirtSprite, pantSprite, full_setSprite, jwellerySprite, shoesSprite;        
        DressList.Instance.Get_Current_Dress_Data(out shirtSprite, out pantSprite, out full_setSprite, out jwellerySprite, out shoesSprite);
        shirt.sprite = shirtSprite;
        pant.sprite = pantSprite;
        fullSet.sprite = full_setSprite;
        necklaces.sprite = jwellerySprite;
        shoes.sprite = shoesSprite;
    }
}
