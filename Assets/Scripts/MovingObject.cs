using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    public float speed;

    private Vector3 vector;

    public float runSpeed;
    private float applyrunSpeed;
    private bool applyRunFlag = false;

    public int walkCount;
    private int CurrentWalkCount;

    private bool canMove = true;

    private Animator animator;

    // speed = 2.4, walkCount = 20
    // 2.4 * 20 = 48
    // While
    // currentWalkCount += 1, 20


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyrunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                applyrunSpeed = 0;
                applyRunFlag = false;
            }


            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)
                vector.y = 0;

            // vector.x = 1 x쪽으로 이동할때 y쪽을 0으로 만듦
            // vector.y = 0;
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);
            animator.SetBool("Walking", true);

            while (CurrentWalkCount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyrunSpeed), 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyrunSpeed), 0);
                }
                if (applyRunFlag)
                    CurrentWalkCount++;
                CurrentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }
            CurrentWalkCount = 0;


        }
        animator.SetBool("Walking", false);
        canMove = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());

            }

        }

    }
}
