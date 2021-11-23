using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Enemy_Collision : MonoBehaviour
{
    [SerializeField] private Player_Movement _player;
    [SerializeField] private CharacterController _characterController;
    private float slideSpeed = 10f;
    private bool collisionFlag = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        print("object entered the trigger");
        collisionFlag = true;
    }

    void OnTriggerStay(Collider other)
    {

        Vector3 moveDirection = transform.forward;

        //print("collision detected");

        if (collisionFlag)
        {
            //if (other.gameObject.tag == "Enemy")
            //{
            //    print("collision object is enemy");
            //    _characterController.Move(moveDirection * slideSpeed * Time.deltaTime);
            //    collisionFlag = false;
            //}
            if(other.gameObject.layer == 8) //layer == "Enemy"
            {
                print("collision object is enemy");
                _characterController.Move(moveDirection * slideSpeed * Time.deltaTime);
                collisionFlag = false;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        print("collision end");
        collisionFlag = false;
    }
}
