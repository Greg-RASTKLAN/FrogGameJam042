using UnityEngine;


public class TongController : MonoBehaviour
{
    // --- États possibles -----------------------------------------------------
    private enum TongState { Ready, Shooting, Returning, Cooldown }
    private TongState state = TongState.Ready;

    // --- Variables publique viewport ----------------------------------
    [Header("Mouvement")]
    [SerializeField] private float speed = 5f; // Vitesse du Lerp

    [Header("Cooldown")]
    [SerializeField] private float cooldownDuration = 1f;

    [Header("Visuel")]
    [SerializeField] private Color readyColor = Color.white;
    [SerializeField] private Color cooldownColor = Color.grey;

    // --- Variables internes --------------------------------------------------
    private float returnThreshold = 0.05f;
    private Vector3 startPosition;
    private float cooldownTimer;
    private SpriteRenderer sr;

    // -------------------------------------------------------------------------
    void Start()
    {
        startPosition = transform.position;
        sr = GetComponent<SpriteRenderer>();
        sr.color = readyColor;
    }

    void Update()
    {
        switch (state)
        {
            // ---------- PRÊT : on peut tirer ---------------------------------
            case TongState.Ready:
                if (Input.GetMouseButtonDown(0))
                    state = TongState.Shooting;
                break;

            // ---------- TIR : vers la souris ---------------------------------
            case TongState.Shooting:
                if (Input.GetMouseButtonUp(0))
                    state = TongState.Returning;

                MoveTowards(GetMouseWorldPos());
                break;

            // ---------- RETOUR : vers la base --------------------------------
            case TongState.Returning:
                MoveTowards(startPosition);

                if (IsAtStart())
                {
                    state = TongState.Cooldown;
                    cooldownTimer = cooldownDuration;
                    sr.color = cooldownColor;   // indicateur visuel
                }
                break;

            // ---------- COOLDOWN : on attend ---------------------------------
            case TongState.Cooldown:
                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer <= 0f)
                {
                    state = TongState.Ready;
                    sr.color = readyColor;
                }
                break;
        }
    }

    // -------------------------------------------------------------------------
    private void MoveTowards(Vector3 target)
    {
        target.z = 0f; // reste en 2D
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        return pos;
    }

    private bool IsAtStart() =>
        Vector3.Distance(transform.position, startPosition) < returnThreshold;
}