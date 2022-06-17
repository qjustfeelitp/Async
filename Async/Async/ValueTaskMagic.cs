using System.Security.Cryptography;

namespace Async;

public static class ValueTaskMagic
{
    private static int? cachedValue;

    public static async ValueTask<int> GetValue()
    {
        if (cachedValue is null)
        {
            await Task.Delay(1_000);
            cachedValue = RandomNumberGenerator.GetInt32(1_000);
        }

        return cachedValue.Value;
    }
}
