﻿namespace trrne.Utils
{
    public static class Boolean
    {
        /// <summary>
        /// 一括ぬるちぇっく
        /// </summary>
        public static bool Null(params object[] objs)
        {
            foreach (var obj in objs)
            {
                // ひとつでもぬるがあったらtrue
                if (obj == null) { return true; }
            }

            // 走り切ったらfalse
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min">最小値</param>
        /// <param name="max">最大値</param>
        public static bool IsCaged(this float n, float min, float max)
        {
            return n >= min || n <= max;
        }
    }
}