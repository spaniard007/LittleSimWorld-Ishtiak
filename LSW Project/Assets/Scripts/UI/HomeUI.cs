using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HomeUI : MonoBehaviour, IDressButtonCall
{
    #region public variable
    public SpriteRenderer playerCharacter;
    public SpriteRenderer playerHair;
    public SpriteRenderer shirt;
    public SpriteRenderer pant;
    public SpriteRenderer fullSet;
    public SpriteRenderer necklaces;
    public SpriteRenderer shoes;

    [Tooltip("Contentainer from the scroll mechanisom")]
    public ContentGrid pant_container;
    public ContentGrid shirt_container;
    public ContentGrid fullSet_container;
    public ContentGrid necklaces_container;
    public ContentGrid shoes_container;

    public Text coin_text;
    public ScrollRect scroller;
    public static bool isReady;

    #endregion

    #region private variable
    List<DressAndJwellery> pant_bought_list;
    List<DressAndJwellery> shirt_bought_list;
    List<DressAndJwellery> fullSet_bought_list;
    List<DressAndJwellery> necklaces_bought_list;
    List<DressAndJwellery> shoes_bought_list;

    Dress_Type type_selected;
    GameObject active_panel;
    #endregion

    #region Start 
    // Start is called before the first frame update
    void Start()
    {
        playerCharacter.sprite = DressList.Instance.player_Charater[PlayerPrefs.GetInt("PlayerCharacter")];
        playerHair.sprite = DressList.Instance.player_Hair[PlayerPrefs.GetInt("PlayerHair")];
        
        Check_At_start();
        type_selected = Dress_Type.Shirt;
        Invoke("Get_Bought_Dress", 1f);
        //isReady = true;
    }

    void Check_At_start()
    {
        if (pant_container == null || shirt_container == null || fullSet_container == null
            || necklaces_container==null || shoes_container==null)
        {
            Debug.LogError("container is not assigned in Home UI");
        }
    }
    #endregion

    void Get_Bought_Dress()
    {
        Debug.Log("1");
        shirt_bought_list = DressList.Instance.Get_Bought_Dress_List(Dress_Type.Shirt);
        Debug.Log("2");
        Dress_Add_To_UI(Dress_Type.Shirt);

        pant_bought_list = DressList.Instance.Get_Bought_Dress_List(Dress_Type.Pant);
        Dress_Add_To_UI(Dress_Type.Pant);

        fullSet_bought_list = DressList.Instance.Get_Bought_Dress_List(Dress_Type.Full_Set);
        Dress_Add_To_UI(Dress_Type.Full_Set);

        necklaces_bought_list = DressList.Instance.Get_Bought_Dress_List(Dress_Type.Jwellary);
        Dress_Add_To_UI(Dress_Type.Jwellary);

        shoes_bought_list = DressList.Instance.Get_Bought_Dress_List(Dress_Type.Shoes);
        Dress_Add_To_UI(Dress_Type.Shoes);

        Set_Curent_Dress();
        Dress_Type_Choose();
    }

    void Dress_Add_To_UI(Dress_Type type)
    {
        List<DressAndJwellery> list = Get_Selected_List(type);

        ContentGrid c = Get_Selected_Container(type);
        c.Extend_Container((int)list.Count/3); // per row 3 maximum value thats why devide the list by 3

        int i = 0;
        foreach (DressAndJwellery dress in list)
        {
            c.Add_To_Container(dress.image, i);
            i++;
        }

        //deactiv the panel
        c.gameObject.SetActive(false);
    }

    #region Curent Dress Mechanisom
    //get the saved dress last time player used and ref it to the game sprites
    void Set_Curent_Dress()
    {
        Sprite shirtSprite, pantSprite, full_setSprite, jwellerySprite, shoesSprite;
        DressList.Instance.Get_Current_Dress( out shirtSprite, out pantSprite, out full_setSprite, out jwellerySprite, out shoesSprite);

        shirt.sprite = shirtSprite;
        pant.sprite = pantSprite;
        fullSet.sprite = full_setSprite;
        necklaces.sprite = jwellerySprite;
        shoes.sprite = shoesSprite;

        coin_text.text = DressList.Instance.Get_Coin().ToString();
    }

    void Save_Current_Dress(Dress_Type type,int id)
    {
        DressList.Instance.Save_Current_Dress(type,id);
    }

    #endregion


    #region Helper for getting selected item
    SpriteRenderer Get_Selected_Sprite()
    {
        if (type_selected == Dress_Type.Shirt)
        {
            //using shirt requires full set null
            fullSet.sprite = null;
            return shirt;
        }
        else if (type_selected == Dress_Type.Pant)
        {
            //using pant requires full set null
            fullSet.sprite = null;
            return pant;
        }
        else if (type_selected == Dress_Type.Full_Set)
        {
            //using full set require shirt and pant off
            shirt.sprite = null;
            pant.sprite = null;
            return fullSet;
        }
        else if (type_selected == Dress_Type.Shoes)
        {
            return shoes;
        }
        else if (type_selected == Dress_Type.Jwellary)
        {
            return necklaces;
        }
        else
        {
            return null;
        }
    }

    List<DressAndJwellery> Get_Selected_List(Dress_Type type)
    {
        if (type == Dress_Type.Shirt)
        {
            return shirt_bought_list;
        }
        else if (type == Dress_Type.Pant)
        {
            return pant_bought_list;
        }
        else if (type == Dress_Type.Full_Set)
        {
            return fullSet_bought_list;
        }
        else if (type == Dress_Type.Shoes)
        {
            return shoes_bought_list;
        }
        else if (type == Dress_Type.Jwellary)
        {
            return necklaces_bought_list;
        }
        else
        {
            return null;
        }
    }
    ContentGrid Get_Selected_Container(Dress_Type type)
    {
        if (type == Dress_Type.Shirt)
        {
            return shirt_container;
        }
        else if (type == Dress_Type.Pant)
        {
            return pant_container;
        }
        else if (type == Dress_Type.Full_Set)
        {
            return fullSet_container;
        }
        else if (type == Dress_Type.Shoes)
        {
            return shoes_container;
        }
        else if (type == Dress_Type.Jwellary)
        {
            return necklaces_container;
        }
        else
        {
            return null;
        }
    }
    #endregion

    #region Button Click Mechanisom
    public void Dress_Button_Click(int index)
    {
        DressAndJwellery selectedDress = Get_Selected_List(type_selected)[index];
        Get_Selected_Sprite().sprite = selectedDress.image;
        Save_Current_Dress(type_selected,selectedDress.ID);
    }
/*  0=Shirt
    1=Pant
    2=FUll set
    3=jwellary
    4=Shoes
         */
    public void Dress_Type_Button_Click(int type)
    {
        if(type==0)
        {
            type_selected = Dress_Type.Shirt;
        }
        else if (type == 1)
        {
            type_selected = Dress_Type.Pant;
        }
        else if (type == 2)
        {
            type_selected = Dress_Type.Full_Set;
        }
        else if (type == 3)
        {
            type_selected = Dress_Type.Jwellary;
        }
        else
        {
            type_selected = Dress_Type.Shoes;
        }

        Dress_Type_Choose();
    }

    void Dress_Type_Choose()
    {

        if (active_panel != null)
        {
            active_panel.SetActive(false);
        }

        active_panel = Get_Selected_Container(type_selected).gameObject;
        active_panel.SetActive(true);
        scroller.content = active_panel.GetComponent<RectTransform>();
    }


    #endregion

}
