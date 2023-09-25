using Cysharp.Threading.Tasks;
using trrne.Appendix;
using UnityEngine;

namespace trrne.Body
{
    // [RequireComponent(typeof(Health))]
    public class Newbie : Enemy
    {
        public enum StartFacing { Left, Right, Random }
        [SerializeField]
        StartFacing facing = StartFacing.Right;

        (Ray ray, RaycastHit2D hit) horizon, top, bottom;
        readonly (float distance, int detect) layer = (1.2f, Fixed.Layers.Player | Fixed.Layers.Ground);

        Player player;
        // Health health;

        (float basis, float real) speed = (2f, 0);

        void Start()
        {
            player = Gobject.GetWithTag<Player>(Fixed.Tags.Player);

            speed.real = facing switch
            {
                StartFacing.Left => -speed.basis,
                StartFacing.Right => speed.basis,
                StartFacing.Random or _ => Rand.Int(max: 2) switch
                {
                    1 => -speed.basis,
                    _ => speed.basis
                }
            };
        }

        protected override async void Behavior()
        {
            horizon.ray = new(transform.position - (Coordinate.x * layer.distance / 2), transform.right);
            horizon.hit = Physics2D.Raycast(horizon.ray.origin, horizon.ray.direction, layer.distance, layer.detect);

            if (horizon.hit)
            {
                switch (horizon.hit.GetLayer())
                {
                    // プレイヤーにあたったらDie()
                    case Fixed.Layers.Player:
                        await player.Die();
                        break;

                    // プレイヤー以外にあたったら進行方向を反転
                    // case Constant.Layers.Ground:
                    default:
                        speed.real *= -1;
                        break;
                }
            }

            Vector2 rayconf = transform.position + (layer.distance * Coordinate.y / 2);
            top.ray = new(rayconf, transform.up);
            top.hit = Physics2D.Raycast(top.ray.origin, top.ray.direction, layer.distance, layer.detect);

            if (top.hit && top.hit.Compare(Fixed.Tags.Player))
            {
                Die();
            }

            bottom.ray = new(rayconf, -transform.up);
            bottom.hit = Physics2D.Raycast(bottom.ray.origin, bottom.ray.direction, layer.distance, layer.detect);

            if (bottom.hit && bottom.hit.Compare(Fixed.Tags.Player))
            {
                await bottom.hit.Get<Player>().Die();
            }

#if DEBUG
            Debug.DrawRay(horizon.ray.origin, horizon.ray.direction * layer.distance, Color.red);
            Debug.DrawRay(top.ray.origin, top.ray.direction * layer.distance, Color.blue);
#endif
        }

        protected override async void Die()
        {
            // エフェクト生成
            // dieFX.Generate(transform.position);

            // すこーし待機
            await UniTask.WaitForSeconds(0.1f);

            // オブジェクト破壊
            Destroy(gameObject);
        }

        protected override void Move()
        {
            transform.Translate(Time.deltaTime * speed.real * Coordinate.x);
        }
    }
}
