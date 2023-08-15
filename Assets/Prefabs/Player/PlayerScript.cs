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
    bool placeTileInputCheck = false;
    bool rotateTileInputCheck = false;

    Vector2 mouseInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wallManager = GameObject.FindWithTag("WallManager").GetComponent<WallManager>();  
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        placeTileInputCheck = Input.GetKeyDown(KeyCode.B);
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
        if (tempWall != null)
        {
            Vector2 pos = new Vector2(Mathf.RoundToInt(mouseInput.x - 0.5f), Mathf.RoundToInt(mouseInput.y - 0.5f));
            tempWall.transform.position = new Vector2(pos.x, pos.y);
        }
    }

    private void PlaceWall()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (tempWall != null)
            {
                if (!Physics2D.OverlapCircle(mouseInput, 0.2f))
                {
                    GameObject placedWall = Instantiate(tempWall);
                    placedWall.transform.SetParent(wallManager.transform);
                    Vector4 color = placedWall.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                    placedWall.GetComponent<BoxCollider2D>().enabled = true;
                    placedWall.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(color.x, color.y, color.z, 1f);
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

    GameObject tempWall;
    private void PlaceWallTemp()
    {
        if (placeTileInputCheck)
        {
            if (tempWall == null)
            {
                // BIG ASS TO DO (PLACE BY MOUSE)
                Vector2 pos = new Vector2(Mathf.Round(mouseInput.x), Mathf.Round(mouseInput.y));
                tempWall = Instantiate(wallManager.WallPrefab, new Vector2(pos.x, pos.y), Quaternion.identity);
                tempWall.GetComponent<BoxCollider2D>().enabled = false;
                Vector4 color = tempWall.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                tempWall.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(color.x, color.y, color.z, 0.3f);
            }
            else
                Destroy(tempWall);
        }
        // 1 tile = 0.5, 0.5
        // transform.position * tilePos
        // press r to rotate face
        // have a global wall manager to know the current upgrade
    }

    private void RotateTile()
    {
        if (rotateTileInputCheck && tempWall != null)
        {
            tempWall.GetComponent<Walls>().ChangeFace();
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
