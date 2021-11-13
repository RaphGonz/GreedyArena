using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
    }

    void UpdateInput()
    {
        float xValue = Input.GetAxis("Horizontal");

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
