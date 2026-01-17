using UnityEngine;

namespace NappyCat.Pool
{
    [AddComponentMenu("Nappy Cat/Pool/NcPooled Behaviour")]
    public class NcPooledBehaviour : MonoBehaviour
    {
        // Called when behaviour is fetched from pool (after activation).
        public virtual void OnPoolAcquire()
        {
        }

        // Called when behaviour is recycled (before deactivation).
        public virtual void OnPoolRelease()
        {
        }
    }
}
