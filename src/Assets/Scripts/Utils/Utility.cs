using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class Utility
    {
        public static float ConsumeRateBySeconds(float seconds) => Time.deltaTime / seconds;
    }
}
