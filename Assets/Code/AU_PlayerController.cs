using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AU_PlayerController : MonoBehaviour
{
    [SerializeField] bool hasControl;
    public static AU_PlayerController localPlayer;
    //Compnents
    Rigidbody myRB;
    Transform myAvatar;
    Animator myAnim;
    //Player movement 
    [SerializeField] InputAction WASD;
    Vector2 movementInput;
    [SerializeField] float movementSpeed;
    static Color myColor;
    SpriteRenderer myAvatarSprite;
    private void OnEnable()
    {
        WASD.Enable();
    }

    private void OnDisable()
    {
        WASD.Disable();
    }

    void Start()
    {
        if (hasControl) {
            localPlayer = this;
        }
        myRB = GetComponent<Rigidbody>();
        myAvatar = transform.GetChild(0);
        myAnim = GetComponent<Animator>();
        
        myAvatarSprite = myAvatar.GetComponent<SpriteRenderer>();
        if (myColor == Color.clear) {
            myColor = Color.white;
        }

        myAvatarSprite.color = myColor;
    }

    void Update()
    {
        movementInput = WASD.ReadValue<Vector2>();
        
        if (movementInput.x != 0)
        {
            myAvatar.localScale = new Vector2(Mathf.Sign(movementInput.x)*128, 128);
        }

        myAnim.SetFloat("Speed", movementInput.magnitude);
    }
    private void FixedUpdate()
    {
        myRB.velocity = movementInput * movementSpeed;
        Debug.Log(myRB.position);
    }

    public void SetColor(Color newColor) {
        myColor = newColor;
        if (myAvatarSprite != null) {
            myAvatarSprite.color=myColor;
        }
    }
}