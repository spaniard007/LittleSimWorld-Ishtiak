using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Dress_Bought_List {

    public List<bool> shirt_bought;
    public List<bool> pant_bought;
    public List<bool> full_set_bought;
    public List<bool> jwellery_bought;
    public List<bool> shoes_bought;

    public float coin;

    //current dress key will be dress type and value will be dress ID
    Dictionary<string, int> current_dress;

    public void CreateObject()
    {
        shirt_bought = new List<bool>();
        pant_bought = new List<bool>();
        full_set_bought = new List<bool>();
        jwellery_bought = new List<bool>();
        shoes_bought = new List<bool>();

        //set current dress as the 1st dress of the index
        //full set is set to -1 for not having value
        //-1 means have no value
        current_dress = new Dictionary<string, int>();
        current_dress.Add(Dress_Type.Shirt.ToString(),0);
        current_dress.Add(Dress_Type.Pant.ToString(),0);
        current_dress.Add(Dress_Type.Full_Set.ToString(),-1);
        current_dress.Add(Dress_Type.Shoes.ToString(),0);
        current_dress.Add(Dress_Type.Jwellary.ToString(),0);
    }

    //save the index of the current wearing dress
    public void SaveCurrentDress(Dress_Type key,int ID)
    {
        if(key==Dress_Type.Full_Set)
        {
            current_dress[Dress_Type.Shirt.ToString()] = -1;
            current_dress[Dress_Type.Pant.ToString()] = -1;
        }
        else if(key == Dress_Type.Shirt || key == Dress_Type.Pant)
        {
            current_dress[Dress_Type.Full_Set.ToString()] = -1;
        }

        current_dress[key.ToString()] = ID;
        Debug.Log(key.ToString()+" "+ID);
    }

    public void Add_Coin(float add)
    {
        coin += add;
    }

    public void Minus_Coin(float minus)
    {
        coin -= minus;
        Debug.Log("Minus_Coin " + coin);
    }

    public float Get_Coin()
    {
        Debug.Log("Get_Coin " + coin);
        return coin;
    }

    public void GetCurrentDress(out int shirtID, out int pantID, out int fullSetID, out int jwelleryID, out int shoesID)
    {
        shirtID = current_dress[Dress_Type.Shirt.ToString()];
        pantID = current_dress[Dress_Type.Pant.ToString()];
        fullSetID = current_dress[Dress_Type.Full_Set.ToString()];
        jwelleryID = current_dress[Dress_Type.Jwellary.ToString()];
        shoesID = current_dress[Dress_Type.Shoes.ToString()];
        Debug.Log("Dress Bought List " + shoesID);
    }

    #region Called Once In App Installed
    //first dress is set to baught for free access
    void List_Creator(List<bool> list,int length)
    {
        for (int i = 0; i < length; i++)
        {
            if (i < 1)
            {
                list.Add(true);
            }
            else
            {
                list.Add(false);
            }
        }
    }

    //check the dress type and set default valuea acording to its type and size 
    public void SetAllPrefsFalse(Dress_Type dressType,int length)
    {

        if (dressType == Dress_Type.Shirt)
        {
            List_Creator(shirt_bought, length);
        }
        else if (dressType == Dress_Type.Pant)
        {
            List_Creator(pant_bought, length);
        }
        else if (dressType == Dress_Type.Full_Set)
        {
            List_Creator(full_set_bought, length);
        }
        else if (dressType == Dress_Type.Jwellary)
        {
            List_Creator(jwellery_bought, length);
        }
        else if (dressType == Dress_Type.Shoes)
        {
            List_Creator(shoes_bought, length);
        }
    }
    #endregion

    public void Buy_Current_Dress(Dress_Type type, int ID)
    {
        List<bool> list = List_According_To_Type(type);
        list[ID] = true;
    }


    public void CurrentDressIndexed(out int shirtID, out int  pantID, out int  full_set_ID, 
        out int  shoesID, out int jwellaryID)
    {
        shirtID = current_dress[Dress_Type.Shirt.ToString()];
        pantID = current_dress[Dress_Type.Pant.ToString()];
        full_set_ID = current_dress[Dress_Type.Full_Set.ToString()];
        shoesID = current_dress[Dress_Type.Shoes.ToString()];
        jwellaryID = current_dress[Dress_Type.Jwellary.ToString()];
    }

    //return the associated list 
    public List<bool> List_According_To_Type(Dress_Type type)
    {
        if(type == Dress_Type.Shirt)
        {
            return shirt_bought;
        }
        else if(type == Dress_Type.Pant)
        {
            return pant_bought;
        }
        else if (type == Dress_Type.Full_Set)
        {
            return full_set_bought;
        }
        else if (type == Dress_Type.Jwellary)
        {
            return jwellery_bought;
        }
        else if (type == Dress_Type.Shoes)
        {
            return shoes_bought;
        }
        else
        {
            return null;
        }
    }

    public void Print()
    {
        for(int i=0;i<full_set_bought.Count;i++)
        {
            Debug.LogWarning(full_set_bought[i]);
        }
    }

}
