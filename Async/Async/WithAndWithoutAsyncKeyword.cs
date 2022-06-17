namespace Async;

public static class WithAndWithoutAsyncKeyword
{
    private static readonly Task<string> MockTask = Task.FromResult("hello");

    public static Task<string> Without()
    {
        return MockTask;
    }

    public static async Task<string> With()
    {
        return await MockTask;
    }

    public static Task<string> HttpCallWithout()
    {
        using var client = new HttpClient();

        return client.GetStringAsync("https://mph.stormware.cz/api/invoice/");
    }

    public static async Task<string> HttpCallWith()
    {
        using var client = new HttpClient();

        return await client.GetStringAsync("https://mph.stormware.cz/api/invoice/");
    }
}
