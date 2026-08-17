//Manges players movement input and rotates the player to face in the direction of movement
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
        GameSettings.playerCharacter = ES3.Load("SelectedCharacter",GameSettings.playerCharacter);
        animator = SelectCharacter(GameSettings.playerCharacter);
        lookDirection = transform.forward;
        characterController = GetComponent<CharacterController>();
    
        // GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        print(GetComponent<PlayerInput>().currentActionMap);
        print(GetComponent<PlayerInput>().currentControlScheme);
        

        animator.SetBool("Grounded", true);

    }

    public void TeleportToLocation(Vector3 position)
    {
        characterController.enabled = false;
        gameObject.transform.position = position;
        characterController.enabled = true;
    }

    public void EnableMovement(bool enable = true)
    {
        enableMovement = enable;
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

    public void PlayAnimation(string name)
    {
        animator.SetTrigger(name);
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
