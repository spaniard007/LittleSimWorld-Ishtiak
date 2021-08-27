using UnityEngine;


public enum Dress_Type
{
    Shirt,Pant,Full_Set,Jwellary,Shoes
}
public enum Event_Catagory
{
    Weeding, Party, Office, Casual
}
[CreateAssetMenu(fileName = "Fashion Game", menuName = "Fashion Game/Dress And Jwellary", order = 1)]
public class DressAndJwellery : ScriptableObject
{
    [Tooltip ("dress image reference")]
    public Sprite image;
    [Tooltip("ID will be automatically generated left it as it is")]
    public int ID;
    [Tooltip("Dress type (ex: Shirt,Pant...)")]
    public Dress_Type dress_type;
    [Tooltip("Event type (ex: Wedding,Office Meeting...)")]
    public Event_Catagory event_catagory;
    [Tooltip("dress price")]
    public float price;
    [Tooltip("left it as it is")]
    public bool bought;
    [Tooltip("how much this dress should get")]
    public int dress_point;
}
