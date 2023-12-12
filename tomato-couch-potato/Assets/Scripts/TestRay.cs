using trrne.Box;
using UnityEngine;

namespace trrne.test
{
    public class TestRay : MonoBehaviour
    {
        (Ray ray, RaycastHit2D hit) left;

        const int IGNORE_LAYER = ~(1 << 0);
        float length, half, offset, originOffset;

        void Start()
        {
            BoxCollider2D c = GetComponent<BoxCollider2D>();
            offset = c.size.x * 1.2f;
            length = c.size.x * 0.8f;
            originOffset = (c.size.x - length) * 2;
            half = c.size.x * 0.5f;
        }

        void Update()
        {
            Left();
        }

        void Left()
        {
            left.ray = new(
                origin: transform.position + new Vector3(-offset / 2, -originOffset),
                direction: transform.up.ToV2());
            left.ray.DrawRay(length, Surface.Gaming);
            left.hit = Gobject.Raycast(left.ray, length, IGNORE_LAYER);
            if (left.hit)
            {
                print("left hit: " + left.hit.collider.name);
            }
        }
    }
}
