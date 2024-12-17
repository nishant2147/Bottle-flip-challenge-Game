using UnityEngine;

public class BottleFlip : MonoBehaviour
{
    public static BottleFlip Instance;

    [Header("Bottle Settings")]
    public Transform bottle;
    public Rigidbody2D bottleRigidbody;
    public float flipForce;
    public float rotationSpeed;

    [Header("Landing Settings")]
    public Transform landingArea;
    float landingTolerance = 25f;

    [Header("Game Control")]
    int maxBounceCount = 3;
    public int currentBounceCount = 0;
    private int bottleFallCount = 0;
    private const int maxFalls = 3;

    private Vector2 startPosition;
    private Vector2 dragStartPosition;
    private Vector2 dragEndPosition;

    private bool isDragging = false;
    public bool canFlip = true;
    private bool isGrounded = false;
    public bool hasLandedSuccessfully = false;

    public EdgeCollider2D edgeCollider;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        landingArea = BottleOpacity.instant.bottlePrefabSprite.gameObject.transform;
        //Debug.Log("landingarea" + landingArea);  

        if (bottle != null)
        {
            startPosition = bottle.position;
        }

        edgeCollider = GetComponent<EdgeCollider2D>();
    }

    private void Update()
    {
        HandleBottleFall();
        HandleBottleFlipInput();
    }

    private void HandleBottleFall()
    {
        if (bottle != null && bottle.position.y < -7.5f)
        {
            bottleFallCount++;

            if (bottleFallCount >= maxFalls)
            {
                UIManager.Instance.panelmanage(UIManager.Instance.OVERPANEL);
            }
            else
            {
                Restart();
            }
        }
    }
    private void HandleBottleFlipInput()
    {
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            dragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
            canFlip = true;
        }
        else if (isDragging && Input.GetMouseButtonUp(0))
        {
            dragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (canFlip)
            {
                Vector2 swipeDirection = (dragEndPosition - dragStartPosition).normalized;
                float swipeForce = (dragEndPosition - dragStartPosition).magnitude * flipForce;

                if (swipeForce >= 0.5f)
                {
                    Vector2 swipeDirections = swipeDirection.normalized;
                    float swipeForces = swipeForce * flipForce;

                    AdjustFlipForce(ref swipeDirections, ref swipeForces);
                    FlipBottle(swipeDirections, swipeForces);
                }

                canFlip = false;

            }
            isDragging = false;
        }
    }
    private void AdjustFlipForce(ref Vector2 direction, ref float force)
    {
        if (landingArea != null)
        {
            float maxDistance = Mathf.Abs(landingArea.position.x - startPosition.x);
            float allowedForce = Mathf.Clamp(force, 0, maxDistance * flipForce);
            force = allowedForce;
            direction.Normalize();
        }
    }
    private void FlipBottle(Vector2 dragDirection, float force)
    {
        if (!isGrounded) return;

        bottleRigidbody.velocity = Vector2.zero;
        bottleRigidbody.AddForce(dragDirection * force, ForceMode2D.Impulse);

        bottleRigidbody.angularVelocity = 0;
        bottleRigidbody.AddTorque(-rotationSpeed, ForceMode2D.Impulse);

        isGrounded = false;
        canFlip = false;

        //ObstacleSpawner.instance.HandleObstacle();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Table"))
        {
            currentBounceCount++;
            //Debug.Log($"Bounce Count: {currentBounceCount}");

            if (currentBounceCount >= maxBounceCount)
            {
                edgeCollider.enabled = false;

                CheckLanding();
            }

            isGrounded = true;
            canFlip = true;
        }
    }

    private void CheckLanding()
    {
        if (landingArea != null)
        {
            float angle = Mathf.Abs(bottle.eulerAngles.z);

            if ((angle < landingTolerance || angle > (360 - landingTolerance)))
            {
                Vector3 bottleSpritePosition = BottleOpacity.instant.bottlePrefabSprite.transform.position;

                float positionalTolerance = 0.2f;

                if (Mathf.Abs(bottle.position.x - bottleSpritePosition.x) <= positionalTolerance)
                {   
                    Debug.Log("Bottle landed successfully!==>" + bottle.position.x);

                    Vector3 targetLandingPosition = new Vector3(bottleSpritePosition.x,
                    bottleSpritePosition.y, 0);
                    Debug.Log("posX:==" + bottleSpritePosition.x);


                    bottle.position = targetLandingPosition;

                    bottle.rotation = Quaternion.Euler(0, 0, 0f);
                    bottleRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

                    hasLandedSuccessfully = true;

                    UIManager.Instance.AddScore(1);

                    edgeCollider.enabled = true;
                    Debug.Log("Edge collider enabled and spawning obstacles.");

                    ObstacleSpawner.instance.SpawnObstacle();

                    DelayedAction();

                } 
                else
                {
                    //Debug.Log("Bottle did not land upright.");
                }
            }
            else
            {
                //canFlip = true;
            }
        }
    }
    public void DelayedAction()
    {
        if (hasLandedSuccessfully)
        {
            hasLandedSuccessfully = false;
            /*edgeCollider.enabled = true;
             canFlip = true;*/
            bottleRigidbody.constraints = RigidbodyConstraints2D.None;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EndLine"))
        {
            UIManager.Instance.panelmanage(UIManager.Instance.OVERPANEL);
            Debug.Log("Game Over");
        }
    }
    public void Restart()
    {
        if (bottle != null)
        {
            bottle.position = startPosition;
            bottle.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            bottle.GetComponent<Rigidbody2D>().angularVelocity = 0f;

            bottle.rotation = Quaternion.identity;

            edgeCollider.enabled = true;

            currentBounceCount = 0;

            //Debug.Log("Game restarted!");
        }
    }
}
