using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour, IDressButtonCall
{
    #region public variable

    [Tooltip("Contentainer from the scroll mechanisom")]
    public ContentGrid pant_container;
    public ContentGrid shirt_container;
    public ContentGrid fullSet_container;
    public ContentGrid necklaces_container;
    public ContentGrid shoes_container;

    public Text coin_text;

    public GameObject confirmation_box;
    public GameObject noBalance_box;
    public Text confirmation_box_text;

    public ScrollRect scroller;
    #endregion

    #region private variable
    List<DressAndJwellery> pant_list;
    List<DressAndJwellery> shirt_list;
    List<DressAndJwellery> fullSet_list;
    List<DressAndJwellery> necklaces_list;
    List<DressAndJwellery> shoes_list;

    Dress_Type type_selected;
    GameObject active_panel;
    float coin;
    int dress_selected_id;
    #endregion

    #region Start 
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Get_Shopping_Dress", .1f);

        Check_At_start();
        Hide_No_Balance();
        Hide_Confirmation_Box();
    }

    void Check_At_start()
    {
        if (pant_container == null || shirt_container == null || fullSet_container == null
            || necklaces_container == null || shoes_container == null)
        {
            Debug.LogError("container is not assigned in Home UI");
        }
    }

    void Get_Shopping_Dress()
    {
        shirt_list = DressList.Instance.Get_Shopping_Dress_List(Dress_Type.Shirt);
        Dress_Add_To_UI(Dress_Type.Shirt);

        pant_list = DressList.Instance.Get_Shopping_Dress_List(Dress_Type.Pant);
        Dress_Add_To_UI(Dress_Type.Pant);

        fullSet_list = DressList.Instance.Get_Shopping_Dress_List(Dress_Type.Full_Set);
        Dress_Add_To_UI(Dress_Type.Full_Set);

        necklaces_list = DressList.Instance.Get_Shopping_Dress_List(Dress_Type.Jwellary);
        Dress_Add_To_UI(Dress_Type.Jwellary);

        shoes_list = DressList.Instance.Get_Shopping_Dress_List(Dress_Type.Shoes);
        Dress_Add_To_UI(Dress_Type.Shoes);


        coin = DressList.Instance.Get_Coin();
        coin_text.text = coin.ToString();
    }

    void Dress_Add_To_UI(Dress_Type type)
    {
        List<DressAndJwellery> list = Get_Selected_List(type);

        ContentGrid c = Get_Selected_Container(type);
        c.Extend_Container((int)list.Count / 3); // per row 3 maximum value thats why devide the list by 3

        int i = 0;
        foreach (DressAndJwellery dress in list)
        {
            c.Add_To_Shop_Container(dress.image, i,dress.price);
            i++;
        }

        //deactiv the panel
        c.gameObject.SetActive(false);
    }

    #endregion


    #region Buy Mechanisom
    void Dress_Buy_Mechanisom(int index)
    {
        List<DressAndJwellery> list = Get_Selected_List(type_selected);
        DressAndJwellery selectedDress = list[index];

        
        DressList.Instance.Minus_Coin(list[index].price);
        coin = DressList.Instance.Get_Coin();
        coin_text.text = coin.ToString();

        // for saving purpose
        DressList.Instance.Buy_Current_Dress(type_selected, selectedDress.ID);

        //remove from current list
        //list.Remove(selectedDress);
        //do not remove it

        //remove from UI
        ContentGrid c = Get_Selected_Container(type_selected);
        c.Remove_From_Container(index); //remove from the UI index number will be samne as index when added

        Hide_Confirmation_Box();

    }

    void Show_Confirmation_Box()
    {
        confirmation_box.SetActive(true);
    }

    void Hide_Confirmation_Box()
    {
        confirmation_box.SetActive(false);
    }

    void Show_No_Balance()
    {
        noBalance_box.SetActive(true);
    }

    void Hide_No_Balance()
    {
        noBalance_box.SetActive(false);
    }

    bool Check_Balance(int index)
    {
        dress_selected_id = index;

        Debug.Log("index: "+ index);

        float price = Get_Selected_List(type_selected)[dress_selected_id].price;

        if (price < coin)
        {
            confirmation_box_text.text = "Are you sure you want to buy this" +
                " dress at $" + price + " ?";
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion

    #region Helper for getting selected item

    List<DressAndJwellery> Get_Selected_List(Dress_Type type)
    {
        if (type == Dress_Type.Shirt)
        {
            return shirt_list;
        }
        else if (type == Dress_Type.Pant)
        {
            return pant_list;
        }
        else if (type == Dress_Type.Full_Set)
        {
            return fullSet_list;
        }
        else if (type == Dress_Type.Shoes)
        {
            return shoes_list;
        }
        else if (type == Dress_Type.Jwellary)
        {
            return necklaces_list;
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
        if (Check_Balance(index))
        {
            Show_Confirmation_Box();
        }
        else
        {
            Show_No_Balance();
        }
    }

    public void Confirmation_Yes_Click()
    {
        Dress_Buy_Mechanisom(dress_selected_id);
        dress_selected_id = -1;
    }

    public void Confirmation_No_Click()
    {
        dress_selected_id = -1;
        Hide_Confirmation_Box();
    }

    public void No_Balance_Button_OK()
    {
        Hide_No_Balance();
    }

    /*  0=Shirt
        1=Pant
        2=FUll set
        3=jwellary
        4=Shoes
             */
    public void Dress_Type_Button_Click(int type)
    {
        if (type == 0)
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

    int cheatCount = 0;
    public void CheatCoin()
    {
        cheatCount++;
        if(cheatCount>7)
        {
            DressList.Instance.Add_Coin(500);
            coin = DressList.Instance.Get_Coin();
            coin_text.text = coin.ToString();
            cheatCount = 0;
        }
    }
    #endregion
}
