using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressList : MonoBehaviour
{
    #region List variables
    [Tooltip("First 4 will be free dress by default")]
    [SerializeField]
    List<DressAndJwellery> shirtsList;

    [Tooltip("First 4 will be free dress by default")]
    [SerializeField]
    List<DressAndJwellery> pantsList;

    [Tooltip("First 4 will be free dress by default")]
    [SerializeField]
    List<DressAndJwellery> full_setsList;

    [Tooltip("First 4 will be free dress by default")]
    [SerializeField]
    List<DressAndJwellery> jwellarysList;

    [Tooltip("First 4 will be free dress by default")]
    [SerializeField]
    List<DressAndJwellery> shoesList;

    /*    hold the bool formated data in a dictionary
          for tacking a dress is bought or not and save data 
          using SavingMechanism class
    */
    Dress_Bought_List bought_check;
    #endregion

    #region Singletone
    static DressList instance;
    private void Awake()
    {
        if (DressList.instance == null)
            DressList.instance = this;
        else
            Destroy(this.gameObject);

        //object can be shared between scene
        DontDestroyOnLoad(this.gameObject);
    }

    public static DressList Instance
    {
        get { return instance; }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Load_Dress_Bought_Data();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
            bought_check.Print();
    }

    #region Coin
    void Load_Dress_Bought_Data()
    {
        Ready_ID();
        bought_check = SavingMechanism.LoadPlayer();
        if (bought_check == null)
        {
            Assign_Bought_Player_Prefs();
        }
        else
        {
            Debug.Log("Have Data");
        }
        Load_Bought_Info();
    }

    public void Add_Coin(float add)
    {
        bought_check.Add_Coin(add);
    }

    public void Minus_Coin(float minus)
    {
        bought_check.Minus_Coin(minus);
    }

    public float Get_Coin()
    {
        return bought_check.Get_Coin();
    }

    #endregion

    #region Get Methods
    public List<DressAndJwellery> GetDressList(Dress_Type type)
    {
        return List_According_To_Type(type);
    }

    //return dresses which were bought
    public List<DressAndJwellery> Get_Bought_Dress_List(Dress_Type type)
    {
        List<DressAndJwellery> list = List_According_To_Type(type);
        List<DressAndJwellery> only_bought_list=new List<DressAndJwellery>();
        foreach(DressAndJwellery dress in list)
        {
            if(dress.bought)
            {
                only_bought_list.Add(dress);
            }
        }
        return only_bought_list;
    }


    public List<DressAndJwellery> Get_Shopping_Dress_List(Dress_Type type)
    {
        List<DressAndJwellery> list = List_According_To_Type(type);
        List<DressAndJwellery> not_bought_list = new List<DressAndJwellery>();
        foreach (DressAndJwellery dress in list)
        {
            if (!dress.bought)
            {
                not_bought_list.Add(dress);
            }
        }
        return not_bought_list;
    }

    #endregion

    #region Current Dress
    public void Get_Current_Dress( out Sprite shirt, out Sprite pant, out Sprite full_set,
    out Sprite jwellery, out Sprite shoes)
    {
        int shirtID = 0, pantID = 0, full_set_ID = 0, shoesID = 0, jwellaryID = 0;

        bought_check.GetCurrentDress(out shirtID, out pantID, out full_set_ID, out jwellaryID, out shoesID);

        Debug.Log("Dress List " + shoesID.ToString());

        SetSprite(shirtsList, shirtID, out shirt);
        SetSprite(pantsList, pantID, out pant);
        SetSprite(full_setsList, full_set_ID, out full_set);
        SetSprite(jwellarysList, jwellaryID, out jwellery);
        SetSprite(shoesList, shoesID, out shoes);
    }


    public void Get_Current_Dress_Data(out Sprite shirt, out Sprite pant, out Sprite full_set, out Sprite jwellery, out Sprite shoes)
    {
        int shirtID = 0, pantID = 0, full_set_ID = 0, shoesID = 0, jwellaryID = 0;

        bought_check.GetCurrentDress(out shirtID, out pantID, out full_set_ID, out jwellaryID, out shoesID);

        SetSprite(shirtsList, shirtID, out shirt);
        SetSprite(pantsList, pantID, out pant);
        SetSprite(full_setsList, full_set_ID, out full_set);
        SetSprite(jwellarysList, jwellaryID, out jwellery);
        SetSprite(shoesList, shoesID, out shoes);

        try
        {
            //get current shirt data
            VotManager.instance.shirtCat = shirtsList[shirtID].event_catagory;
            VotManager.instance.shirtpoint = shirtsList[shirtID].dress_point; 
            VotManager.instance.hasShirt = true; 
        }
        catch 
        {
            VotManager.instance.hasShirt = false;
        }


        try
        {
            //get current pant data
            VotManager.instance.pantCat = shirtsList[pantID].event_catagory;
            VotManager.instance.pantpoint = shirtsList[pantID].dress_point;
            VotManager.instance.hasPant = true;
        }
        catch 
        { 
            VotManager.instance.hasPant = false;
        }

        try
        {
            //get current fullSet data
            VotManager.instance.fullSetCat = shirtsList[full_set_ID].event_catagory;
            VotManager.instance.fullSetpoint = shirtsList[full_set_ID].dress_point;
            VotManager.instance.hasFullSet = true;
        }
        catch 
        {
            VotManager.instance.hasFullSet = false;
        }

        try
        {
            //get current shoes data
            VotManager.instance.shoesCat = shirtsList[shoesID].event_catagory;
            VotManager.instance.shoespoint = shirtsList[shoesID].dress_point;
            VotManager.instance.hasShoes = true;
        }
        catch
        {
            VotManager.instance.hasShoes = false;
        }

        try
        {
            //get current jwellary data
            VotManager.instance.jwellaryCat = shirtsList[jwellaryID].event_catagory;
            VotManager.instance.jwellarypoint = shirtsList[jwellaryID].dress_point;
            VotManager.instance.hasJwellary = true;
        }
        catch 
        {
            VotManager.instance.hasJwellary = false;
        }
    }


    //if -1 means no dress 
    void SetSprite(List<DressAndJwellery> list, int ID, out Sprite sprite)
    {
        if (ID < 0)
        {
            sprite = null;
        }
        else if (list[ID].bought)
        {
            sprite = list[ID].image;
        }
        else
        {
            sprite = null;
        }
    }

    public void Save_Current_Dress(Dress_Type dress_type,int dress_ID)
    {
        List<DressAndJwellery> list= List_According_To_Type(dress_type);

        if(list[dress_ID].bought)
        {
            bought_check.SaveCurrentDress(dress_type,dress_ID);
        }
    }

    #endregion


    #region ID Generate
    void Ready_ID()
    {
        ID_Generator(shirtsList);
        ID_Generator(pantsList);
        ID_Generator(full_setsList);
        ID_Generator(jwellarysList);
        ID_Generator(shoesList);
    }

    void ID_Generator(List<DressAndJwellery> list)
    {
        int i = 0;
        foreach (DressAndJwellery dress in list)
        {
            dress.ID = i;
            i++;
        }
    }

    #endregion

    #region Called Once In App Installed
    void Assign_Bought_Player_Prefs()
    {
        //creating object for holding date 
        bought_check = new Dress_Bought_List();
        bought_check.CreateObject();

        //set false for dress
        bought_check.SetAllPrefsFalse(Dress_Type.Shirt, shirtsList.Count);
        bought_check.SetAllPrefsFalse(Dress_Type.Pant, pantsList.Count);
        bought_check.SetAllPrefsFalse(Dress_Type.Full_Set, full_setsList.Count);
        bought_check.SetAllPrefsFalse(Dress_Type.Jwellary, jwellarysList.Count);
        bought_check.SetAllPrefsFalse(Dress_Type.Shoes, shoesList.Count);

        bought_check.Add_Coin(30);

        //saving as custome PlayerPrefs
        SavingMechanism.SaveData(bought_check);

    }

    #endregion

    #region Load Player bought data
    void Load_Bought_Info()
    {
        Bought_Info_Set_Mechanisom(Dress_Type.Shirt);
        Bought_Info_Set_Mechanisom(Dress_Type.Pant);
        Bought_Info_Set_Mechanisom(Dress_Type.Full_Set);
        Bought_Info_Set_Mechanisom(Dress_Type.Jwellary);
        Bought_Info_Set_Mechanisom(Dress_Type.Shoes);
    }

    void Bought_Info_Set_Mechanisom(Dress_Type type)
    {
        List<DressAndJwellery> dress_List = List_According_To_Type(type);
        List<bool> bought = bought_check.List_According_To_Type(type);
        int i = 0;
        foreach (bool b in bought)
        {
            dress_List[i].bought = bought[i];
            i++;
        }

    }

    #endregion

    public void Buy_Current_Dress(Dress_Type type, int ID)
    {
        List<DressAndJwellery> list = List_According_To_Type(type);
        list[ID].bought=true;

        bought_check.Buy_Current_Dress(type,ID);
    }

    //return the associated list 
    public List<DressAndJwellery> List_According_To_Type(Dress_Type type)
    {
        if (type == Dress_Type.Shirt)
        {
            return shirtsList;
        }
        else if (type == Dress_Type.Pant)
        {
            return pantsList;
        }
        else if (type == Dress_Type.Full_Set)
        {
            return full_setsList;
        }
        else if (type == Dress_Type.Jwellary)
        {
            return jwellarysList;
        }
        else if (type == Dress_Type.Shoes)
        {
            return shoesList;
        }
        else
        {
            return null;
        }
    }

    //----------------Save while quit app
    private void OnApplicationQuit()
    {
        SavingMechanism.SaveData(bought_check);
    }

    //------------testing purpose---------------
}
