using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float MovementSpeed = 6f;

    private Rigidbody2D rb;
    private WallManager wallManager;
    public Animator animator;
    public Vector2 moveDir;
    public Vector2 Offset = Vector2.zero;

    Vector2 facing = Vector2.right;
    bool openShopInput = false;
    bool rotateTileInputCheck = false;
    public GameObject placingPrefab;
    Transform gameManager;

    Vector2 mouseInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wallManager = GameObject.FindWithTag("WallManager").GetComponent<WallManager>();
        gameManager = GameObject.FindWithTag("UpgradeManager").transform;
        placingPrefab = wallManager.WallPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        openShopInput = Input.GetKeyDown(KeyCode.B);
        rotateTileInputCheck = Input.GetKeyDown(KeyCode.R);
        mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        PlaceWallTemp();
        RotateTile();
        PlaceWall();
        RemoveWall();
    }

    void FixedUpdate()
    {
        Move();
        if (tempPrefab != null)
        {
            Vector2 pos = new Vector2(Mathf.RoundToInt(mouseInput.x - 0.5f), Mathf.RoundToInt(mouseInput.y - 0.5f));
            tempPrefab.transform.position = new Vector2(pos.x, pos.y);
        }
    }

    private void PlaceWall()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (tempPrefab != null)
            {
                if (!Physics2D.OverlapCircle(mouseInput, 0.2f))
                {
                    GameObject placedPrefab = Instantiate(tempPrefab);
                    placedPrefab.transform.SetParent(gameManager);
                    Vector4 color = placedPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                    placedPrefab.GetComponent<BoxCollider2D>().enabled = true;
                    if (tempPrefab.tag != "Wall")
                        tempPrefab.GetComponent<Attackable>().enabled = true;
                    placedPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(color.x, color.y, color.z, 1f);
                }
             }
        }
    }

    private void RemoveWall()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Collider2D[] walls = Physics2D.OverlapCircleAll(mouseInput, 0.1f);
            if (walls.Length != 0)
            {
                Destroy(walls[0].gameObject);
            }
        }
    }

    GameObject tempPrefab;
    public void PlacePrefabTemp(GameObject gameObject)
    {
        if (tempPrefab == null)
        {
            placingPrefab = gameObject;
            Vector2 pos = new Vector2(Mathf.Round(mouseInput.x), Mathf.Round(mouseInput.y));
            tempPrefab = Instantiate(placingPrefab, new Vector2(pos.x, pos.y), Quaternion.identity);
            if (tempPrefab.tag != "Wall")
                tempPrefab.GetComponent<Attackable>().enabled = false;
            tempPrefab.GetComponent<BoxCollider2D>().enabled = false;
            Vector4 color = tempPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
            tempPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(color.x, color.y, color.z, 0.3f);
        }
    }

    private void PlaceWallTemp()
    {
        if (openShopInput)
        {
            if (tempPrefab == null)
            {
                GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
            }
            else
                Destroy(tempPrefab);
        }
    }

    private void RotateTile()
    {
        if (rotateTileInputCheck && tempPrefab != null && tempPrefab.tag == "Walls")
        {
            tempPrefab.GetComponent<Walls>().ChangeFace();
        }
    }

    private void ProcessInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(moveX, moveY).normalized;
        if (moveDir != Vector2.zero && moveDir != facing && Mathf.Abs(moveDir.x) != Mathf.Abs(moveDir.y))
            facing = moveDir;
        // Changing State Between moving and idle
        animator.SetBool("isMoving", moveDir != Vector2.zero);
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDir.x * MovementSpeed, moveDir.y * MovementSpeed);
    }
}
