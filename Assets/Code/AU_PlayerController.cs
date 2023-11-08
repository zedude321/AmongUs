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
    //Role
    [SerializeField] bool isImpostor;
    [SerializeField] InputAction KILL;

    List<AU_PlayerController> targets;
    [SerializeField] Collider myCollider;

    bool isDead;

    [SerializeField] GameObject bodyPrefab;

    private void Awake() {
        KILL.performed += KillTarget;
    }

    private void OnEnable()
    {
        WASD.Enable();
        KILL.Enable();
    }

    private void OnDisable()
    {
        WASD.Disable();
        KILL.Disable();
    }

    void Start()
    {
        if (hasControl) {
            localPlayer = this;
        }
        targets = new List<AU_PlayerController>();
        myRB = GetComponent<Rigidbody>();
        myAvatar = transform.GetChild(0);
        myAnim = GetComponent<Animator>();
        myAvatarSprite = myAvatar.GetComponent<SpriteRenderer>();

        if (myColor == Color.clear) {
            myColor = Color.white;
        }

        if (!hasControl)
            return;
        myAvatarSprite.color = myColor;
    }

    void Update()
    {
        if (!hasControl)
            return;
        
        movementInput = WASD.ReadValue<Vector2>();
        
        if (movementInput.x != 0)
        {
            myAvatar.localScale = new Vector2(Mathf.Sign(movementInput.x), 1);
        }

        myAnim.SetFloat("Speed", movementInput.magnitude);
    }

    private void FixedUpdate()
    {
        myRB.velocity = movementInput * movementSpeed;
    }

    public void SetColor(Color newColor) {
        myColor = newColor;
        if (myAvatarSprite != null) {
            myAvatarSprite.color=myColor;
        }
    }

    public void setRole (bool newRole) {
        isImpostor = newRole;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            AU_PlayerController tempTarget = other.GetComponent<AU_PlayerController>();
            if (isImpostor) {
                if (tempTarget.isImpostor) {
                    return;
                } else {
                    targets.Add(tempTarget);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            AU_PlayerController tempTarget = other.GetComponent <AU_PlayerController>();
            if (targets.Contains(tempTarget)) {
                targets.Remove(tempTarget);
            }
        }
    }
    
    private void KillTarget(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            if (targets.Count == 0) {
                return;
            } else {
                if (targets[targets.Count - 1].isDead) {
                    return;
                }
                transform.position = targets[targets.Count - 1].transform.position;
                targets[targets.Count - 1].Die();
                targets.RemoveAt(targets.Count - 1);
            }
        }
    }

    public void Die () {
        isDead = true;
        myAnim.SetBool("IsDead", isDead);
        myCollider.enabled = false;

        AU_Body tempBody = Instantiate(bodyPrefab, transform.position, transform.rotation).GetComponent<AU_Body>();
        tempBody.SetColor(myAvatarSprite.color);
    }
}