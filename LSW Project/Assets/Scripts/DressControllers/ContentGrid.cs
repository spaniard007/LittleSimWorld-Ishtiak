using UnityEngine;
using UnityEngine.UI;

public class ContentGrid : MonoBehaviour
{
    #region varibales
    RectTransform rectTransform;

    [Tooltip("which button will be added to the grid ")]
    public GameObject content_prefab;

    [Tooltip("UI Manager implementing IDressButtonCall interface")]
    public GameObject uiManager;

    IDressButtonCall manager;

    float xValue;

    [Tooltip("maximum row number of the grid")]
    public int maximum_row;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        xValue = rectTransform.sizeDelta.x; // getting the defailt width
        manager = uiManager.GetComponent<IDressButtonCall>();
        if (manager == null)
        {
            Debug.LogError("In ContentGrid class uiManager filed do not have the reference of an object implimenting" +
                " IDressButtonCall interface");
        }
    }

    //for every 4th button it will be called
    public void Extend_Container(int multiplayer)
    {
        //adding the default width with current width
        xValue = xValue * multiplayer;

        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x + xValue, rectTransform.sizeDelta.y);
    }

    #region Add To The Container
    public void Add_To_Container(Sprite sprite, int index)
    {
        Add(sprite, index);
    }

    public void Add_To_Shop_Container(Sprite sprite, int index,float price)
    {
        Add_To_Shop(sprite, index,price);
    }
    //instantiate and assign image and add On Click event
    void Add(Sprite sprite,int index)
    {
        GameObject contentObj= Instantiate(content_prefab,this.transform);
        Image dress_img=contentObj.transform.GetChild(0).GetComponent<Image> ();

        Button b = contentObj.GetComponent<Button>();

        //asigning the button click method
        b.onClick.AddListener(() => manager.Dress_Button_Click(index));

        if (dress_img!=null)
        {
            dress_img.sprite = sprite;
        }
    }

    void Add_To_Shop(Sprite sprite, int index, float price)
    {
        GameObject contentObj = Instantiate(content_prefab, this.transform);
        Image dress_img = contentObj.transform.GetChild(0).GetComponent<Image>();
        Text proce_text= contentObj.transform.GetChild(3).GetComponent<Text>();

        Button b = contentObj.GetComponent<Button>();

        //asigning the button click method
        b.onClick.AddListener(() => manager.Dress_Button_Click(index));

        dress_img.sprite = sprite;
        proce_text.text = "$"+price;
    }

    #endregion

    public void Remove_From_Container(int index_in_container)
    {
        GameObject dress= this.transform.GetChild(index_in_container).gameObject;
        if (dress != null)
            dress.SetActive(false);
    }

    //for the Shop Scene

}
