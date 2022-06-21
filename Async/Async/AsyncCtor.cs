namespace Async;

public class AsyncCtor
{
    public string SomeData { get; private set; }

    public AsyncCtor()
    {
        Construct();
    }

    private async Task Construct()
    {
        await Task.Delay(1_000);
        this.SomeData = "test";
    }
}

public class AsyncCtorIfNeeded
{
    public string SomeData { get; private set; }

    public AsyncCtorIfNeeded(Action executed) //, Action exceptionAction)
    {
        Construct().ContinueWith(task => { executed(); }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.OnlyOnRanToCompletion);
    }

    private async Task Construct()
    {
        await Task.Delay(1_000);
        this.SomeData = "test";
    }
}
