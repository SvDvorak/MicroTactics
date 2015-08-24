namespace Assets
{
    public interface IRandom
    {
        float Value { get; }
    }

    public class Random
    {
        private static IRandom _instance;
        public static IRandom Instance
        {
            get { return _instance ?? (_instance = new UnityRandom()); }
            set { _instance = value; }
        }
    }

    public class UnityRandom : IRandom
    {
        public float Value { get { return UnityEngine.Random.value; } }
    }
}