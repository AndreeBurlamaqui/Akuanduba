using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TrupeMember
{
    public Transform TargetPosition;
    public GameObject Member;
    public float MoveSpeed, FrequencySine, MagnitudeSine;
    public Vector3 sinePos;

}

public class Trupe : MonoBehaviour
{

    public bool waitFallow = true;
    public float dist;

    public TrupeMember member1 = new TrupeMember();
    public TrupeMember member2 = new TrupeMember();
    public TrupeMember member3 = new TrupeMember();

    private Player_Input playerInput;


    private void Awake()
    {
        playerInput = new Player_Input();
        playerInput.Player.WaitFollow.performed += _ => waitFallow = !waitFallow;
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void FixedUpdate()
    {
        //Movement
        if (waitFallow)
        {

            //MEMBER 1
            if (Vector2.Distance(member1.Member.transform.position, member1.TargetPosition.position) > dist)
            {
                //trail.Play();
                Vector2 member1Dir = member1.Member.transform.position - member1.TargetPosition.position;
                float member1Angle = Mathf.Atan2(member1Dir.y, member1Dir.x) * Mathf.Rad2Deg;
                member1.Member.transform.rotation = Quaternion.AngleAxis(member1Angle + 90f, Vector3.forward);
            }
            else
            {
                //trail.Stop();
            }

            member1.sinePos = Vector3.Lerp(member1.Member.transform.position, member1.TargetPosition.position, member1.MoveSpeed * Time.deltaTime);

            member1.Member.transform.position = member1.sinePos + member1.TargetPosition.up * Mathf.Sin(Time.time * member1.FrequencySine) * member1.MagnitudeSine;

            


            //MEMBER 2
            if (Vector2.Distance(member2.Member.transform.position, member2.TargetPosition.position) > dist)
            {
                //trail.Play();
                Vector2 member2Dir = member2.Member.transform.position - member2.TargetPosition.position;
                float member2Angle = Mathf.Atan2(member2Dir.y, member2Dir.x) * Mathf.Rad2Deg;
                member2.Member.transform.rotation = Quaternion.AngleAxis(member2Angle + 90f, Vector3.forward);
            }
            else
            {
                //trail.Stop();
            }

            member2.sinePos = Vector3.Lerp(member2.Member.transform.position, member2.TargetPosition.position, member2.MoveSpeed * Time.deltaTime);

            member2.Member.transform.position = member2.sinePos + member2.TargetPosition.up * Mathf.Sin(Time.time * member2.FrequencySine) * member2.MagnitudeSine;

            

            //MEMBER 3
            if (Vector2.Distance(member3.Member.transform.position, member3.TargetPosition.position) > dist)
            {
                //trail.Play();

                Vector2 member3Dir = member3.Member.transform.position - member3.TargetPosition.position;
                float member3Angle = Mathf.Atan2(member3Dir.y, member3Dir.x) * Mathf.Rad2Deg;
                member3.Member.transform.rotation = Quaternion.AngleAxis(member3Angle + 90f, Vector3.forward);
            }
            else
            {
                //trail.Stop();
            }

            member3.sinePos = Vector3.Lerp(member3.Member.transform.position, member3.TargetPosition.position, member3.MoveSpeed * Time.deltaTime);

            member3.Member.transform.position = member3.sinePos + member3.TargetPosition.up * Mathf.Sin(Time.time * member3.FrequencySine) * member3.MagnitudeSine;
        }
    }


}
