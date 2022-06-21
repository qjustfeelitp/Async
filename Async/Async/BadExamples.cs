namespace Async;

public static class BadExamples
{
    /// <summary>
    /// Once you go async, all of your callers SHOULD be async, since efforts to be async amount to nothing unless the entire callstack is async.
    /// In many cases, being partially async can be worse than being entirely synchronous. Therefore it is best to go all in, and make everything async at once.
    /// </summary>
    public static void Example01()
    {
        // BAD! sync over async!!! can cause deadlock and break app (not in asp net core, because it has no synchronization context - to which thread to return, it can start request on one thread and finish on another)
        int a = default;

        int result = Task.Run(() => a + 1).Result;

        //int result = Task.Run(() => a + 1).GetAwaiter().GetResult();

        Console.WriteLine(result);
    }

    public static async Task Example02()
    {
        // what will the result be?
        var obj = new SomeObject()
        {
            SomeData = default
        };

        await Task.Run(() => obj.SomeData += 1);

        Do(obj);

        Console.WriteLine(obj.SomeData);
    }

    private static async void Do(SomeObject input)
    {
        await Task.Delay(10);
        input.SomeData = 1_000;
    }

    /// <summary>
    /// Async void methods can't be tracked and therefore unhandled exceptions can result in application crashes.
    /// </summary>
    public static async void Example03()
    {
        await Task.Run(() => throw new Exception("KABOOM"));
    }

    public static Task<int> Example04()
    {
        return Task.Run(() => 150);
    }

    /// <summary>
    /// CancellationTokenSource objects that are used for timeouts (are created with timers or uses the CancelAfter method), can put pressure on the timer queue if not disposed.
    /// This example does not dispose the CancellationTokenSource and as a result the timer stays in the queue for 10 seconds after each request is made.
    /// </summary>
    /// <returns></returns>
    public static async Task<string> HttpClientAsyncWithCancellation()
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

        using (var client = new HttpClient())
        {
            var response = await client.GetAsync("https://google.com", cts.Token);

            return await response.Content.ReadAsStringAsync(cts.Token);
        }
    }
}

public class SomeObject
{
    public int SomeData { get; set; }
}