using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public Animator animator;

    [SerializeField] PlayerController playerController;


    // Update is called once per frame
    void Update()
    {
        UpdateInput();
    }

    void UpdateInput()
    {
        float xValue = Input.GetAxis("Horizontal");

        animator.SetFloat("speed", Mathf.Abs(xValue));

        if (Mathf.Abs(xValue) > 0.1f)
        {
            xValue = Mathf.Sign(xValue);
            playerController.MoveX(xValue);
        }


        if (Input.GetButtonDown("Jump")) {
            playerController.Jump();
        }

        playerController.RotatePivotPoint(UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition));

        
        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetMouseButton(0))
        {
            playerController.Shoot();
        }


    }

}
