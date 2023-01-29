using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float positionY;

    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;

    private Vector2 movementInput;
    private Vector2 mousePosition;

    private bool isLookingLeft;

    private bool isWalking;

    public float movementSpeed;

    private bool isActioning;

    private bool isActionButtonDown;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && CoreGame._instance.gameManager.gameState != GameState.CRAFT)
        {
            if(CoreGame._instance.gameManager.gameState == GameState.MENU)
            {
                CoreGame._instance.menuManager.CloseScreenMenu();
            } else
            {
                CoreGame._instance.menuManager.OpenScreenMenu(1);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            CoreGame._instance.menuManager.OpenScreenMenuLeft();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            CoreGame._instance.menuManager.OpenScreenMenuRight();
        }

        if (CoreGame._instance.gameManager.gameState != GameState.GAMEPLAY)
        {
            return;
        }
     
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(mousePosition.x < transform.position.x && isLookingLeft == false)
        {
            Flip();
        }
        if(mousePosition.x > transform.position.x && isLookingLeft == true)
        {
            Flip();
        }

        if(Input.GetButtonDown("Fire1") && isActioning == false)
        {
            isActionButtonDown = true;
        }

        if(Input.GetButtonUp("Fire1"))
        {
            isActionButtonDown = false;
        }

        if(isActionButtonDown == true && isActioning == false)
        {
            isActioning = true;
            playerAnimator.SetTrigger("isGathering");
        }

        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        isWalking = movementInput.magnitude != 0;

        if(isActioning == false)
        {
            playerRigidBody.velocity = movementInput * movementSpeed;
        } else if(isActioning == true)
        {
            playerRigidBody.velocity = Vector2.zero;
            isWalking = false;
        }


        playerAnimator.SetBool("isWalking", isWalking);
    }

    private void LateUpdate()
    {
        positionY = transform.position.y;
    }
    private void Flip()
    {
        if(isActioning == true)
        {
            return;
        }
        isLookingLeft = !isLookingLeft;
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, 1, 1);
    }

    public void AxeHit()
    {
        CoreGame._instance.gameManager.ObjectHit();
    }

    private void ActionDone()
    {
        isActioning = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Loot":
                Item item = collision.gameObject.GetComponent<Loot>().item;
                CoreGame._instance.inventory.GetItem(item, 1);
                Destroy(collision.gameObject);
                break;
        }
    }
}
