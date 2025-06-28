using UnityEngine;

public class TongController : MonoBehaviour
{
    // Différents états possibles
    private enum TongState { Ready, Shooting, Returning, Cooldown }
    private TongState state = TongState.Ready;


    // Variables publiques viewport
    [Header("Mouvement")]
    [SerializeField] private float speed = 30f;

    [Header("Cooldown")]
    [SerializeField] private float cooldownDuration = 1f;

    [Header("Visuel")]
    [SerializeField] private Color readyColor = Color.white;
    [SerializeField] private Color cooldownColor = Color.grey;


    // Variables internes
    private float targetThreshold = 0.05f;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float cooldownTimer;
    private SpriteRenderer sr;

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
            // ---------- PRÊT : on peut tirer ----------
            case TongState.Ready:
                if (Input.GetMouseButtonDown(0))
                {
                    targetPosition = GetMouseWorldPos();
                    state = TongState.Shooting;
                }
                break;
            // ---------- TIR : vers le clic de la souris ----------
            case TongState.Shooting:
                MoveTowards(targetPosition);

                if (Vector3.Distance(transform.position, targetPosition) < targetThreshold)
                {
                    transform.position = startPosition; // retour instantané
                    state = TongState.Cooldown;
                    cooldownTimer = cooldownDuration;
                    sr.color = cooldownColor;
                }
                break;

            // ---------- COOLDOWN : on attend ----------
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

    private void MoveTowards(Vector3 target)
    {
        target.z = 0f;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        return pos;
    }
}