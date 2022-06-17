namespace Async;

public static class WithAndWithoutAsyncKeyword
{
    private static readonly Task<string> MockTask = Task.FromResult("hello");
    private static readonly Task<string> ExceptionTask = Task.FromException<string>(new Exception("KABOOM!"));

    public static Task<string> Without()
    {
        return MockTask;
    }

    public static async Task<string> With()
    {
        return await MockTask;
    }

    public static Task<string> HttpCallWithout(HttpClient client)
    {
        return client.GetStringAsync("https://api.github.com/users/qjustfeelitp");
    }

    public static async Task<string> HttpCallWith(HttpClient client)
    {
        return await client.GetStringAsync("https://api.github.com/users/qjustfeelitp");
    }
}
