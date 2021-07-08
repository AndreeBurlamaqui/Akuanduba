using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FAllower_Control : MonoBehaviour
{
    public bool waitFallow=true;
    public float speed;//velocidade do minion
    public Transform target;// transform do player
    bool podeAndar = true;

    //controle do raycast
    public float ViewDistance;
    public Transform castPoint;
    public LayerMask layerToSee;

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

    void Start()
    {
        
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();

    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawLine(transform.position, target.position);
    }

    bool orientacao=true; //direita = true, esquerda = false
    void ChangeQuaternion()
    {
        if (target.transform.localScale.x == -1)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            orientacao = false;
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            orientacao = true;
        }
    }
    void Update()
    {

        if (target.position.x<transform.position.x && orientacao == true)
        {
            ChangeQuaternion();
        }
        else if(target.position.x > transform.position.x && orientacao == false)
        {
            ChangeQuaternion();
        }

        if (waitFallow)
        {
            //Vector2 endpos = castPoint.position + Vector3.right * ViewDistance;
            RaycastHit2D hit = Physics2D.Raycast(castPoint.position, transform.right,ViewDistance,layerToSee);
            if (hit.collider != null)
            {

                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    Debug.DrawLine(transform.position, hit.point, Color.green);
                    podeAndar = false;


                }
                



            }
            else
            {
               
                podeAndar = true;
            }


            if (podeAndar == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);//comando para seguir
                
            }

        }
    }
   
}
