using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed; // The speed the player moves at
    public float jumpHeight; // How high the player jumps
    public float sensitivity; // How sensitive the player's rotation is to mouse movement
    public float interactRange; // How far the interaction ray gets cast
    public float grasp; // How much currentHeld is moving towards the PUP(Pickup Point)
    public float holdDamping; // Damping on the force applied the held object
    public float threshold; // How close the held object needs to be to the PUP before force stops being applied
    public float decelerationSpeed; // Rate at which the force being applied to the held object is depricated
    public float throwForce; // How hard the player throws objects
    public float playerVelDepricationRate; // Rate at which the player's velocity is depricated
    public float maxVel; // Maximum speed the player is allowed to move
    public float walkMaxVel; // Maximum speed player can move when walking
    public float sprintMaxVel; // Maximum speed player can move when sprinting
    public float curVel; // Current velocity of the player
    public float curHoldDist; // Distance at which held objects are held from player
    public float maxHoldDist; // Maximum distance at which playaer can hold held object
    public float minHoldDist; // Minimum distance at which player can hold held object
    public float holdDistMouseMult; // Used to scale scroll wheel input for increasing or decreasing curHoldDist
    public GameObject cam; // Main camera
    public GameObject interactText; // Text that says "interact" when the player is looking at an object he can interact with
    public GameObject crosshair; // Crosshair that appears when the player is looking at an object that he can interact with

    private GameObject currentHeld; // Object currently being held
    private Rigidbody rb; // Player's Rigidbody
    private float x, y, xx; // Variables used for rotating the player and camera, as well as clamping camera rotation
    private bool isGrounded; // Whether or not the player is on solid ground
    private bool canInteract; // Whether or not the player can interact with the object that he's looking at
    private bool canDrop; // Whether or not the player can drop held object


    void Start()
    {
        Cursor.visible = false;// Hiding the cursor
        Cursor.lockState = CursorLockMode.Locked;// Locking the cursor so that it does not float around the screen
        rb = gameObject.GetComponent<Rigidbody>(); // Assigning the player's Rigidbody
        interactText.SetActive(false); // Hide "interact" text
        crosshair.SetActive(false); // Hide crosshair
        xx = 0; // Set to 0 so player doesn't spawn in looking at floor
    }

    void Update()
    {
        // Movement
        //------------------------------------------------------------------------
        curVel = FindVel(rb.velocity); // Calculating the current absoluet velocity of the player

        // Checking for input and adding the velocity as long as the player is below their top speed
        //........................................................................
        if(Input.GetKey(KeyCode.W) && curVel < maxVel)
        {
            rb.velocity += transform.forward * speed * Time.deltaTime; // Forward movement
        }
        if (Input.GetKey(KeyCode.S) && curVel < maxVel)
        {
            rb.velocity += transform.forward * -speed * Time.deltaTime; // Backward Movement
        }
        if (Input.GetKey(KeyCode.A) && curVel < maxVel)
        {
            rb.velocity += transform.right * -speed * Time.deltaTime; // Left movement
        }
        if (Input.GetKey(KeyCode.D) && curVel < maxVel)
        {
            rb.velocity += transform.right * speed * Time.deltaTime; // Right movement
        }
        if(Input.GetKey(KeyCode.LeftShift)) // Check if player is sprinting
        {
            maxVel = sprintMaxVel;
        }
        else
        {
            maxVel = walkMaxVel;
        }
        //........................................................................

        Vector3 curVelTemp = new Vector3(Mathf.Lerp(rb.velocity.x, 0, playerVelDepricationRate * Time.deltaTime), rb.velocity.y, Mathf.Lerp(rb.velocity.z, 0, playerVelDepricationRate * Time.deltaTime)); // Constantly depricating the player's velocity

        rb.velocity = curVelTemp; // Constantly setting the player's velocity to the depricated velocity so they don't exceed their maximum velocity, and so the slow down at the proper rate
        //------------------------------------------------------------------------

        // Rotation
        //------------------------------------------------------------------------
        y = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime; // y = Mouse X because we are rotating the player on the Y axis
        x = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime; // x = Mouse Y because we are rotating the camera on the X axis

        xx -= x; // Keeps track of where the head shoud currently be rotated
        xx = Mathf.Clamp(xx, -90, 90); // Clamping the X rotation of the camera that way the player can't infinitely spin on the X axis

        cam.transform.localRotation = Quaternion.Euler(xx, 0f, 0f); // Setting appropriate camera rotation along the X axis

        transform.Rotate(transform.rotation.x, y, transform.rotation.z); // Rotate player along the Y axis
        //------------------------------------------------------------------------

        // Jumping
        //------------------------------------------------------------------------
        // Check if standing on the ground
        Ray groundCheckRay = new Ray(new Vector3(transform.position.x, transform.position.y - .9f, transform.position.z), Vector3.down); // Creating a ray coming out of the bottom of the player, going down
        // Ray must be cast from inside the player, instead of at the player's feet, or else the raycast doesn't always detect the collider beneath it 
        RaycastHit groundHit; // Ground Raycast Hit

        if (Physics.Raycast(groundCheckRay, out groundHit, 0.2f)) // Checking to see if the raycast hit anything
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // Checking for proper input, and most importantly that the player is standing on solid ground
        {
            isGrounded = false;
            StartCoroutine(Jump());
        }
        //------------------------------------------------------------------------

        // Interaction
        //------------------------------------------------------------------------
        Ray intRay = new Ray(cam.transform.position, cam.transform.forward); // Casting a ray in the direction the player is looking // Interact Ray
        RaycastHit intHit; // Interact Raycast Hit

        if(Physics.Raycast(intRay, out intHit, interactRange)) // Checking to see if the raycast hit anything
            switch(intHit.collider.tag) // Determine what object it hit
            {
                case "Cube":
                    if(Input.GetMouseButtonDown(0) && intHit.collider.gameObject.GetComponent<Rigidbody>() && !canDrop) // Check for left click
                    {
                        currentHeld = intHit.collider.gameObject;
                        StartCoroutine(DelayDrop());
                    }
                    canInteract = false;
                    break;

                case "Toggle Button":
                    if(Input.GetMouseButtonDown(0))
                    {
                        intHit.collider.gameObject.GetComponent<ToggleButton>().PushButton();
                    }
                    canInteract = true;
                    break;

                case "Button":
                    if(Input.GetMouseButtonDown(0))
                    {
                        intHit.collider.gameObject.GetComponent<Button>().PushButton();
                    }
                    canInteract = true;
                    break;

                default:
                    canInteract = false;
                    break;
            }

        if (intHit.collider == null) // Disabling the ability to attempt to interact with nothing
            {
                canInteract = false;
            }

        if (canInteract)
        {
            interactText.SetActive(true); // Enabling "interact" text
            crosshair.SetActive(true); // Enabling crosshair
        }
        else
        {
            interactText.SetActive(false); // Disabling "interact text"
            crosshair.SetActive(false); // Disabling crosshair
        }
        //------------------------------------------------------------------------

        // Throwing and Setting Down
        //------------------------------------------------------------------------
        if(currentHeld != null && Input.GetMouseButtonDown(1)) // Check for right click
        {
            currentHeld.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce); // Throw current held object
            currentHeld.GetComponent<Rigidbody>().useGravity = true; // Enable gravity on held object
            currentHeld = null; // Reset the current held object to null
            canDrop = false;
            canInteract = true;
        }

        if(currentHeld != null && Input.GetMouseButtonDown(0) && canDrop) // Check for left click  
        {
            currentHeld.GetComponent<Rigidbody>().useGravity = true;
            currentHeld = null;
            canDrop = false;
            canInteract = true;
        }
        //------------------------------------------------------------------------
    }

    void FixedUpdate()
    {
        // Holding
        //------------------------------------------------------------------------
        if (currentHeld != null) // Check to see if player is holding an object
        {
            if(canInteract) // Make sure player can't interact with objects whilst holding anything
            {
                canInteract = false;
            }

            curHoldDist += Input.mouseScrollDelta.y * holdDistMouseMult; // Apply mouse wheel rotation to held object, to move it closer to, and further from camera
            curHoldDist = Mathf.Clamp(curHoldDist, minHoldDist, maxHoldDist); // Clamp held object distance from camera

            Rigidbody hrb = currentHeld.GetComponent<Rigidbody>(); // Assign the object's Rigidbody as hrb (Held Rigidbody)

            Vector3 objPos = currentHeld.transform.position; // Position of held object
            Vector3 tPos = cam.transform.position + cam.transform.forward * curHoldDist; // Target position

            Vector3 dir = (tPos - objPos);

            if(hrb.useGravity == true) // Make sure gravity is disabled
            {
                hrb.useGravity = false;
            }

            float f = Vector3.Distance(tPos, objPos) * holdDamping; // Variable to adjust damping based on the held objects position from the PUP

            if(Vector3.Distance(tPos,objPos) > threshold) // Check if the held object is mroe than a certain distance away from the PUP, if so, apply force moving it towards the PUP
            {
                hrb.AddForce(dir * grasp / f);
            }

            hrb.velocity = Vector3.Slerp(hrb.velocity, new Vector3(0, 0, 0), decelerationSpeed); // Constant deprication of the held objects velocity to assist with stability
        }
        //------------------------------------------------------------------------
    }
    IEnumerator Jump()
    {
        rb.AddForce(Vector3.up * jumpHeight * 100f); // Adding desired force upwards

        yield return new WaitForSeconds(0.15f); // Delay to prevent players from spamming jump and getting extra height
    }

    IEnumerator DelayDrop()
    {
        canDrop = false;
        yield return new WaitForSeconds(0.05f); // Delay player being allowed to drop held object (Fix to a bug where player couldn't pick up objects, because it was detecting the click to pick up, and dropping the held object immediately)
        canDrop = true;
    }

    float FindVel(Vector3 vel)
    {
        vel = new Vector3(Mathf.Abs(vel.x), 0, Mathf.Abs(vel.z)); // Getting the absolute value of X and Z

        float f = vel.x * vel.x + vel.z * vel.z; // Squaring and adding the absolute values of X and Z
        f = Mathf.Sqrt(f); // Finding the square root of f
        return f; // Outputting f
    }
}
