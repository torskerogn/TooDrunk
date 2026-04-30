using UnityEngine;

public class HeadTarget : MonoBehaviour
{
    public PlayerSide owner = PlayerSide.Bottom;

    private HelmetMover mountedHelmet = null;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (mountedHelmet != null) return;
        if (!other.CompareTag("Helmet")) return;

        HelmetMover mover = other.GetComponent<HelmetMover>();
        if (mover == null) return;
        if (!mover.isLaunched) return;
        if (mover.owner != owner) return;

        mountedHelmet = mover;
        mover.MountHelmet(transform);
    }

    public void UnmountHelmet()
    {
        mountedHelmet = null;
    }
}
