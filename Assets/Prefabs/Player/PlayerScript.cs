using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float MovementSpeed = 6f;

    private Rigidbody2D rb;
    private WallManager wallManager;
    public Animator animator;
    public Vector2 moveDir;
    public Vector2 Offset = Vector2.zero;
    public float AttackDelay = 0.2f;

    Vector2 facing = Vector2.right;
    bool openShopInput = false;
    bool rotateTileInputCheck = false;
    public GameObject placingPrefab;
    Transform upgradeManager;

    Vector2 mouseInput;

    float attackTime;
    Attackable attackable;
    PickaxeScript pickaxe;
    TorchScript torch;
    bool canBuy = false;
    EnemySpawner enemy;
    ShopButton shop;

    public int WoodsCount = 0;
    public int RocksCount = 0;

    SoundManager sound;
    bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shop = GameObject.FindWithTag("Shop").GetComponent<ShopButton>();
        sound = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
        enemy = GameObject.FindWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        wallManager = GameObject.FindWithTag("WallManager").GetComponent<WallManager>();
        upgradeManager = GameObject.FindWithTag("UpgradeManager").transform;
        pickaxe = transform.GetChild(2).GetComponent<PickaxeScript>();
        torch = transform.GetChild(3).GetComponent<TorchScript>();
        placingPrefab = wallManager.WallPrefab;
        attackable = GetComponent<Attackable>();
        attackTime = Time.time + AttackDelay;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        openShopInput = Input.GetKeyDown(KeyCode.B);
        rotateTileInputCheck = Input.GetKeyDown(KeyCode.R);
        mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // PlaceTiles
        PlaceWallTemp();
        RotateTile();
        PlaceWall();
        RemoveWall();

        WeaponHandle();

    }

    void WeaponHandle()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!pickaxe.gameObject.activeSelf)
                pickaxe.gameObject.SetActive(true);
            else
                pickaxe.PutAway();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!torch.gameObject.activeSelf)
                torch.gameObject.SetActive(true);
            else
                torch.PutAway();
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (pickaxe.gameObject.activeSelf)
            {
                if (attackTime < Time.time)
                {
                    Vector2 pos = transform.position;
                    attackable.Attack(mouseInput - pos);
                    pickaxe.Attack();
                    canMove = false;
                    attackTime = Time.time + AttackDelay;
                    StartCoroutine(BlockMovement(0.2F));
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (canMove)
            Move();
        else
            rb.velocity = Vector2.zero;
        if (transform.position.magnitude <= enemy.PlayableZone + 5f)
        {
            canBuy = true;
            shop.Show(true);
        }
        else
        {
            canBuy = false;
            shop.Show(false);
        }
        if (tempPrefab != null)
        {
            Vector2 pos = new Vector2(Mathf.RoundToInt(mouseInput.x - 0.5f), Mathf.RoundToInt(mouseInput.y - 0.5f));
            tempPrefab.transform.position = new Vector2(pos.x, pos.y);
        }
    }

    private IEnumerator BlockMovement(float time)
    {
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    private void ProcessInput()
    {
        if (!canMove)
            return;
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(moveX, moveY).normalized;
        if (moveDir != Vector2.zero && moveDir != facing && Mathf.Abs(moveDir.x) != Mathf.Abs(moveDir.y))
            facing = moveDir;
        // Changing State Between moving and idle
        bool moving = moveDir != Vector2.zero;
        animator.SetBool("isMoving", moveDir != Vector2.zero);
        if (pickaxe.gameObject.activeSelf)
            pickaxe.Move(moving);
        if (torch.gameObject.activeSelf)
            torch.Move(moving);
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDir.x * MovementSpeed, moveDir.y * MovementSpeed);
    }

    #region Placing Tiles
    private void PlaceWall()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (tempPrefab != null)
            {
                if (!Physics2D.OverlapCircle(mouseInput, 0.2f) && upgradeManager.GetComponent<UpgradeManager>().Sold(tempPrefab.tag))
                {
                    GameObject placedPrefab = Instantiate(tempPrefab);
                    placedPrefab.transform.SetParent(upgradeManager);
                    Vector4 color = placedPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                    placedPrefab.GetComponent<BoxCollider2D>().enabled = true;
                    placedPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(color.x, color.y, color.z, 1f);
                    if (tempPrefab.tag != "Wall" && tempPrefab.tag != "Torch")
                        tempPrefab.GetComponent<FriendlyAI>().canAttack = true;
                    sound.PlayClip("Grass1");
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
                upgradeManager.GetComponent<UpgradeManager>().GetPrice(walls[0].gameObject.tag);
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
            if (tempPrefab.tag != "Wall" && tempPrefab.tag != "Torch")
                tempPrefab.GetComponent<FriendlyAI>().canAttack = false ;
            if (tempPrefab.tag == "Wall")
                GameObject.FindWithTag("RotateButton").GetComponent<CanvasGroup>().alpha = 1f;
            tempPrefab.GetComponent<BoxCollider2D>().enabled = false;
            Vector4 color = tempPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
            tempPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(color.x, color.y, color.z, 0.3f);
        }
    }

    private void PlaceWallTemp()
    {
        if (openShopInput && canBuy)
        {
            if (GameObject.Find("Canvas").transform.GetChild(2).gameObject.activeSelf)
            {
                GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                if (tempPrefab == null)
                {
                    GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true);
                }
                else
                {
                    if (tempPrefab.tag == "Wall")
                        GameObject.FindWithTag("RotateButton").GetComponent<CanvasGroup>().alpha = 0f;
                    Destroy(tempPrefab);
                }
            }
        }
        if (openShopInput && tempPrefab != null)
        {
            if (tempPrefab.tag == "Wall")
                GameObject.FindWithTag("RotateButton").GetComponent<CanvasGroup>().alpha = 0f;
            Destroy(tempPrefab);
        }
    }

    private void RotateTile()
    {
        if (rotateTileInputCheck && tempPrefab != null && tempPrefab.tag == "Wall")
        {
            tempPrefab.GetComponent<Walls>().ChangeFace();
        }
    }
    #endregion
}
