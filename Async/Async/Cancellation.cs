namespace Async;

public static class Cancellation
{
    // preemptive vs cooperative cancellation?
    public static async Task<int> CancellableTask(CancellationToken cancellationToken)
    {
        int result = default;

        do
        {
            await Task.Delay(1, cancellationToken); // what happens?
            result++;
        }
        while (!cancellationToken.IsCancellationRequested);

        return result;
    }

    public static int CancellableNotTask(CancellationToken cancellationToken)
    {
        int result = default;

        do
        {
            result++;
        }
        while (!cancellationToken.IsCancellationRequested);

        return result;
    }
}
