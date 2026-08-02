//Manges players movement input and rotates the player to face in the direction of movement
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    public bool enableMovement;
    private Vector3 movementDirection;
    private Vector3 lookDirection;
    [SerializeField] private Animator femaleRig;
    [SerializeField] private Animator maleRig;
    private Animator animator;

    private CharacterController characterController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = SelectCharacter(GameSettings.playerCharacter);
        lookDirection = transform.forward;
        characterController = GetComponent<CharacterController>();
    
        // GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        print(GetComponent<PlayerInput>().currentActionMap);
        print(GetComponent<PlayerInput>().currentControlScheme);
        

        enableMovement = true;
        animator.SetBool("Grounded", true);

    }

    private Animator SelectCharacter(GameSettings.CharacterSelection selection)
    {
        if(selection == GameSettings.CharacterSelection.MALE)
        {
            Destroy(femaleRig.gameObject);
            maleRig.gameObject.SetActive(true);

            return maleRig;
        }
        else
        {
            Destroy(maleRig.gameObject);
            femaleRig.gameObject.SetActive(true);
            return femaleRig;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Player will move based on the input and face in the same direction
        if (enableMovement)
        {
            characterController.SimpleMove(movementSpeed * movementDirection);
        }
            transform.forward = lookDirection;
            animator.SetFloat("MoveSpeed", movementDirection.magnitude);


        

    }

    public void OnMove(InputAction.CallbackContext value)
    {
        Vector2 input = value.ReadValue<Vector2>();
        {
            movementDirection = new Vector3(input.x, 0, input.y);
            movementDirection = Vector3.ClampMagnitude(movementDirection, 1f);  
            if (value.performed) //Sets the look direction only when movement has started.
            {
                lookDirection = movementDirection;
            }
        }
    }



}
