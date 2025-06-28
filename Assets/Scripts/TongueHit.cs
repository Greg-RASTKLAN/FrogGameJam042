using UnityEngine;

public class TongHit : MonoBehaviour
{
    public void DetectObjectsBetween(Vector3 from, Vector3 to)
    {
        Vector3 direction = (to - from).normalized;
        float distance = Vector3.Distance(from, to);

        RaycastHit2D[] hits = Physics2D.RaycastAll(from, direction, distance);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                Fly fly = hit.collider.GetComponent<Fly>();

                if (fly != null)
                {
                    Destroy(fly.gameObject); // ou tout autre effet
                }
            }
        }
    }
}