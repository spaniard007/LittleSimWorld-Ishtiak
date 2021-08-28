using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    [Header("Player Movement Settings")] [SerializeField] [Range(0f, 10f)]
    private float
        speed = 0f;

    private bool isRunning = false;


    public Animator anim;

    private Vector3 target = Vector3.zero;
    private Vector3 tmpPos = Vector3.zero;


    private Vector2 rightAxis = Vector2.right;
    private Vector2 leftAxis = -Vector2.right;
    private Vector2 topAxis = Vector2.up;
    private Vector2 bottomAxis = -Vector2.up;
    
    
    private SpriteRenderer sprite;


    void Awake()
    {


    }

    void Start()
    {
        isRunning = true;

        target = transform.position;
        anim = GetComponent<Animator>();
        //transform.position = new Vector3(PlayerPrefs.GetFloat(("posX")), PlayerPrefs.GetFloat(("posY")),
       //     transform.position.z);
    }

    void Update()
    {
        PlayerPrefs.SetFloat("posX",transform.position.x);
        PlayerPrefs.SetFloat("posY",transform.position.y);

        if (ConversationManager.conversationInstance.isDialogueActive)
        {
            FixAnimParam(false,false,false,false);
        }

        ProcessInput();
    }

    private void ProcessInput()
    {
        //Debug.Log (isRunning);

        if (isRunning && !ConversationManager.conversationInstance.isDialogueActive)
        {

            if (transform.position.x != tmpPos.x && transform.position.y != tmpPos.y)
            {

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(tmpPos.x, tmpPos.y, 0),
                    speed * Time.deltaTime);


                //anim.SetBool("IsRunning", true);
                ProcessDirection();


                if (speed == 0)
                {
                    FixAnimParam(false,false,false,false);
                    //	AudioController.Instance.StopWalkSound ();
                }


            }
            else if (transform.position.x == tmpPos.x && transform.position.y == tmpPos.y)
            {

                //speed = 0f;
                isRunning = false;
                //	AudioController.Instance.StopWalkSound ();
            }

        }
        else
        {
            //AudioController.Instance.StopWalkSound ();
        }


    }



    public void MoveToTarget()
    {
        //speed = 1.44f;
        tmpPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);

        isRunning = true;
    }

    private void FixAnimParam(bool upDir, bool downDir,bool leftDir,bool rightDir)
    {

        anim.SetBool("UpDir", upDir);
        anim.SetBool("DownDir", downDir);
        anim.SetBool("LeftDir", leftDir);
        anim.SetBool("RightDir", rightDir);
    }

    private void ApplyAnimation(float correctDir, float comp1)
    {

    }

    private void ProcessDirection()
    {

        FixAnimParam(false,false,false,false);

        Vector2 dir = (tmpPos - transform.position).normalized;
        //	Debug.Log("Dir"+dir);
        float comp1 = Vector2.Angle(dir, rightAxis);
        float comp2 = Vector2.Angle(dir, topAxis);
        float comp3 = Vector2.Angle(dir, leftAxis);
        float comp4 = Vector2.Angle(dir, bottomAxis);

        float correctDir = Mathf.Min(new float[] {comp1, comp2, comp3, comp4});
        if (correctDir == comp1 && comp3 != 90)
        {
            Debug.Log ("Right");

            if (isRunning && comp3 != 0)
            {

                anim.SetBool("RightDir", true);
            }

        }
        else if (correctDir == comp2 && comp1 != 90)
        {

            Debug.Log ("Top");

            if (isRunning)
            {
                anim.SetBool("UpDir", true);
            }
        }

        else if (correctDir == comp3 && comp3 != 90)
        {
            Debug.Log ("Left");

            if (isRunning)
            { anim.SetBool("LeftDir", true);
            }
        }

        else if (correctDir == comp4 && comp1 != 90)
        {
            Debug.Log ("Bottom");

            if (isRunning)
            { anim.SetBool("DownDir", true);
            }
        }

    }
}
    