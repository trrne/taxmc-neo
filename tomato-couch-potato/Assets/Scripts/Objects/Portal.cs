using Cysharp.Threading.Tasks;
using DG.Tweening;
using trrne.Pancreas;
using UnityEngine;
using System.Collections;

namespace trrne.Heart
{
    public class Portal : Object
    {
        [SerializeField]
        Vector2 to;

        [SerializeField]
        float teleportSpeed = 3f;

        [SerializeField]
        float speed_range = 30;

        (GameObject[] frames, float[] speeds) child;
        float myspeed;
        bool warping = false;
        int loop;

        protected override void Start()
        {
            base.Start();

            // パーティクルを除くため-1
            loop = transform.childCount - 1;

            child.frames = new GameObject[loop];
            child.speeds = new float[loop];
            for (int i = 0; i < loop; i++)
            {
                child.frames[i] = transform.GetChildObject(i);
                child.speeds[i] = Rand.Float(-speed_range, speed_range);
            }

            myspeed = Rand.Float(-speed_range, speed_range);

            foreach (var frame in child.frames)
            {
                frame.GetComponent<SpriteRenderer>().SetAlpha(0.75f);
            }
        }

        protected override void Behavior()
        {
            for (int i = 0; i < loop; i++)
            {
                // フレームを回転させる
                child.frames[i].transform.Rotate(Time.deltaTime * child.speeds[i] * Vector100.Z);
            }

            // ついでに中心も回転させる
            transform.Rotate(Time.deltaTime * myspeed * Vector100.Z);
        }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (!warping && info.CompareBoth(Constant.Layers.Player, Constant.Tags.Player))
            {
                var player = info.Get<Player>();
                if (!player.IsDieProcessing)
                {
                    warping = true;
                    effects.TryGenerate(transform.position);

                    info.transform.DOMove(to, teleportSpeed)
                        .SetEase(Ease.OutCubic)
                        .OnPlay(() => player.IsTeleporting = true)
                        .OnComplete(() => player.IsTeleporting = false);

                    warping = false;
                    // StartCoroutine(AfterDelay(info));
                }
            }
        }

        IEnumerator AfterDelay(Collider2D info)
        {
            yield return null;

            info.transform.SetPosition(to);
            warping = false;
        }
    }
}
