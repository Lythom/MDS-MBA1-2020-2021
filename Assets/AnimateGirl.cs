using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class AnimateGirl : MonoBehaviour
{
    [Tooltip("Vitesse max en unités par secondes")]
    public int MaxSpeed = 2;

    // Autres scripts
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // variables de mon instance
    private Vector3 speed;

    // statics
    private static readonly int Speed = Animator.StringToHash("Speed");

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        var maxDisancePerFrame = MaxSpeed * Time.deltaTime;
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            move += Vector3.right * maxDisancePerFrame;
            spriteRenderer.flipX = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            move += Vector3.left * maxDisancePerFrame;
            spriteRenderer.flipX = true;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            move += Vector3.up * maxDisancePerFrame;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            move += Vector3.down * maxDisancePerFrame;
        }

        animator.SetFloat(Speed, move.magnitude * 10f);
        this.transform.position = this.transform.position + move;
    }
}