using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class Carrot : Object
    {
        [SerializeField]
        [Tooltip("limit回踏んだらアウト")]
        int limitSteps = 2;

        CarrotFlag flag;
        new BoxCollider2D collider;

        /// <summary>
        /// 耐久値ぜろだったらtrue
        /// </summary>
        public bool Mendable { get; private set; }

        public float Ratio => (float)flag.Count / limitSteps;

        protected override void Start()
        {
            base.Start();

            flag = transform.GetFromChild<CarrotFlag>();
            flag.Count = 0;

            sr.sprite = sprites[0];

            collider = GetComponent<BoxCollider2D>();
        }

        protected override void Behavior()
        {
            if (!Mendable && flag.Count >= limitSteps)
            {
                effects.TryGenerate(transform.position);

                Mendable = true;
                sr.enabled = false;
                collider.enabled = false;
            }

            sr.sprite = sprites[Ratio < .5f ? 0 : 1];
        }

        public void Mend()
        {
            Mendable = false;
            flag.Count = 0;
            sr.enabled = true;
            collider.enabled = true;
        }
    }
}
