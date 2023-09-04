public static class RandomExtensions
{
    public static T RandomEnumValue<T>(this System.Random random)
    {
        T[] values = (T[])System.Enum.GetValues(typeof(T));
        return values[random.Next(0, values.Length)];
    }
}
