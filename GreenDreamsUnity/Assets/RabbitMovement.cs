using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RabbitMovement : MonoBehaviour
{
    Animator animator;

    public float moveSpeed = 1.2f;
    Vector3 stopPosition;

    float walkTime;
    public float walkCounter;
    float waitTime;
    public float waitCounter;

    int walkDirection;

    public bool isWalking;




    void Start()
    {
        animator = GetComponent<Animator>();

        walkTime = Random.Range(3, 6);
        waitTime = Random.Range(3, 5);

        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();

    }


    void Update()
    {
        if (isWalking)
        {
            animator.SetBool("isRunning", true);
            walkCounter -= Time.deltaTime;

            switch (walkDirection)
            {
                case 0:
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    transform.localPosition += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case 1:
                    transform.localRotation = Quaternion.Euler(0f, 90, 0f);
                    transform.localPosition += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case 2:
                    transform.localRotation = Quaternion.Euler(0f, -90, 0f);
                    transform.localPosition += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case 3:
                    transform.localRotation = Quaternion.Euler(0f, 180, 0f);
                    transform.localPosition += transform.forward * moveSpeed * Time.deltaTime;
                    break;

            }


            if (walkCounter <= 0)
            {
                stopPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                isWalking = false;
                transform.position = stopPosition;
                animator.SetBool("isRunning", false);
                waitCounter = waitTime;
            }
            
        }
        else
        {
            waitCounter -= Time.deltaTime;


            if (waitCounter <= 0)
            {
                ChooseDirection();
            }
        }


    }

    public void ChooseDirection()
    {
        walkDirection = Random.Range(0, 4);

        isWalking = true;
        walkCounter = walkTime;
    }
}
