using System.Collections;
using System.Collections.Generic;
using trrne.WisdomTeeth;
using UnityEngine;

namespace trrne.Body
{
    public class Fan : Objectt
    {
        public float power;

        GameObject collision;

        protected override void Start()
        {
            base.Start();
            collision = transform.GetChildObject();
        }

        protected override void Behavior()
        {
        }
    }
}
