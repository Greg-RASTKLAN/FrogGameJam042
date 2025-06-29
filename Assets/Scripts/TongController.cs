using UnityEngine;

public class TongController : MonoBehaviour
{
    // ---------------- États ----------------
    private enum TongState { Ready, Shooting, Returning, Cooldown }
    private TongState state = TongState.Ready;

    // ---------------- Réglages exposés ----------------
    [Header("Mouvements")]
    [SerializeField] private float shootSpeed = 35f;   // vitesse aller
    [SerializeField] private float returnSpeed = 25f;   // vitesse retour

    [Header("Cooldown")]
    [SerializeField] private float cooldownDuration = 1f;

    [Header("Visuel")]
    [SerializeField] private Color readyColor = Color.white;
    [SerializeField] private Color cooldownColor = Color.grey;

    [Header("Sound")]
    [SerializeField] private AudioSource tongueSounds;
    [SerializeField] private AudioClip[] clips;

    // ---------------- Variables internes ----------------
    private readonly float threshold = 0.05f;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float cooldownTimer;
    private SpriteRenderer sr;


    private void Start()
    {
        startPosition = transform.position;
        sr = GetComponent<SpriteRenderer>();
        sr.color = readyColor;

        GameManager.Instance.OnTongueCooldownDecreased -= UpdateCooldownValue;
        GameManager.Instance.OnTongueCooldownDecreased += UpdateCooldownValue;
        UpdateCooldownValue(GameManager.Instance.tongueCooldown);
    }

    private void Update()
    {
        switch (state)
        {
            // ---------- PRÊT ----------
            case TongState.Ready:
                if (Input.GetMouseButton(0))
                {
                    targetPosition = GetMouseWorldPos();
                    state = TongState.Shooting;
                }
                break;

            // ---------- ALLER ----------
            case TongState.Shooting:

                MoveTowards(targetPosition, shootSpeed);
                
                if (Vector3.Distance(transform.position, targetPosition) < threshold)
                {
                    state = TongState.Returning;
                    int index = Random.Range(0, clips.Length);
                    tongueSounds.PlayOneShot(clips[index]);
                }
                break;

            // ---------- RETOUR ----------
            case TongState.Returning:
                MoveTowards(startPosition, returnSpeed);

                if (Vector3.Distance(transform.position, startPosition) < threshold)
                {
                    state = TongState.Cooldown;
                    cooldownTimer = cooldownDuration;
                    sr.color = cooldownColor;
                }
                break;

            // ---------- COOLDOWN ----------
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

    // ---------------- Fonctions nécessaires aux états ----------------
    private void MoveTowards(Vector3 target, float speed)
    {
        target.z = 0f;
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        return pos;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Fly fly = other.GetComponent<Fly>();
        fly?.OnCaught();
    }

    private void UpdateCooldownValue(float newCooldown) 
    {
        cooldownDuration = newCooldown;
    }
}