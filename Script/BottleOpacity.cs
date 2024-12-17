using TMPro;
using UnityEngine;

public class BottleOpacity : MonoBehaviour
{
    public static BottleOpacity instant;
    private SpriteRenderer spriteRenderer;
    public float fadeSpeed = 1f;

    public Sprite bottleSprite;
    public GameObject bottlePrefabSprite;
    private bool panelWasClosed = true;


    private void Awake()
    {
        instant = this;
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (spriteRenderer != null)
        {

            float alpha = Mathf.PingPong(Time.time * fadeSpeed, 1f);

            Color color = spriteRenderer.color;
            color.a = alpha;

            spriteRenderer.color = color;
        }


        if (UIManager.Instance.PlayPanle.activeSelf && panelWasClosed)
        {
            SpawnBottleSprites();
            panelWasClosed = false;
        }
        else if (!UIManager.Instance.PlayPanle.activeSelf)
        {
            panelWasClosed = true;
        }
    }
    public void SpawnBottleSprites()
    {
        if (transform.position != null && bottleSprite != null)
        {
            /*bottlePrefabSprite = new GameObject("BottleSprite");

            spriteRenderer = bottlePrefabSprite.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = bottleSprite;
            spriteRenderer.sortingOrder = 1;

            bottlePrefabSprite.transform.position = transform.position;*/

            /*Rigidbody2D rb = bottlePrefabSprite.AddComponent<Rigidbody2D>();
            rb.gravityScale = 1;
            rb.mass = 1;

            BoxCollider2D collider = bottlePrefabSprite.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;*/
        }
        else
        {
            Debug.LogWarning("BottleSprite or Player is not assigned!");
        }
    }
}
