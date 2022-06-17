using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Async;

/// <summary>
/// COMPILER JUST LOOKS FOR GETAWAITER THAT RETURNS CORRECT TASKAWAITER
/// </summary>
public static class Extensions
{
    public static TaskAwaiter GetAwaiter(this int i)
    {
        Console.WriteLine($"waited for {i}");

        return Task.Delay(i).GetAwaiter();
    }

    public static TaskAwaiter GetAwaiter(this TimeSpan span)
    {
        Console.WriteLine($"waited for {span:g}");

        return Task.Delay(span).GetAwaiter();
    }

    public static TaskAwaiter<int> GetAwaiter(this Process process)
    {
        var tcs = new TaskCompletionSource<int>();
        process.EnableRaisingEvents = true;

        process.Exited += (_, _) =>
        {
            Console.WriteLine("Notepad exited.");
            tcs.TrySetResult(process.ExitCode);
        };

        if (process.HasExited)
        {
            tcs.TrySetResult(process.ExitCode);
        }

        return tcs.Task.GetAwaiter();
    }

    public static TaskAwaiter GetAwaiter(this IEnumerable<Task> tasks)
    {
        return Task.WhenAll(tasks).GetAwaiter();
    }
}
