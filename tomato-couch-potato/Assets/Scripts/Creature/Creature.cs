using Cysharp.Threading.Tasks;
using UnityEngine;

namespace trrne.Core
{
    public abstract class Creature : MonoBehaviour
    {
        [SerializeField]
        protected GameObject diefx;

        public bool Enable { get; set; }

        protected SpriteRenderer sr;

        protected virtual void Start()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        protected virtual void Update()
        {
            if (this == null || !Enable)
                return;

            Movement();
            Behavior();
        }

        /// <summary>
        /// 移動
        /// </summary>
        protected abstract void Movement();

        /// <summary>
        /// 振舞 / プレイヤー検知など
        /// </summary>
        protected abstract void Behavior();

        /// <summary>
        /// 死<br/>asyncつける
        /// </summary>
        public abstract UniTask Die();
    }
}
