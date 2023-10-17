﻿using System;

namespace trrne.Pancreas
{
    [Serializable]
    public class Karappoyanke : Exception
    {
        public Karappoyanke() : base("からっぽえんぷてぃ") {; }
        public Karappoyanke(string msg = null) : base(msg) {; }
    }
}
