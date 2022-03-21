using System.Text;

namespace Diff.TestClient;

public class TestApiDiff
{
    public async Task Run()
    {
        HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7272")
        };

        await Post(client, "/v1/diff/111/left",  "some value to be compared");
        await Post(client, "/v1/diff/111/right", "some value to be compared");
        string s1 = await Get(client, "/v1/diff/111");
        // inputs were equal

        await Post(client, "/v1/diff/222/left",  "some longer value to be compared");
        await Post(client, "/v1/diff/222/right", "some value to be compared");
        string s2 = await Get(client, "/v1/diff/222");
        // inputs are of different size

        await Post(client, "/v1/diff/333/left",  "test 123");
        await Post(client, "/v1/diff/333/right", "test 124");
        string s3 = await Get(client, "/v1/diff/333");
        // difference: offset 7, length 1

        await Post(client, "/v1/diff/444/left",  "test string 1234567");
        await Post(client, "/v1/diff/444/right", "test gnirts 1244567");
        string s4 = await Get(client, "/v1/diff/444");
        // difference: offset 5, length 6,
        // difference: offset 14, length 1
    }

    private async Task Post(HttpClient client, string route, string text)
    {
        var serializedText = Serialize(text);
        var encodedText = string.Concat("\"", EncodeToBase64(serializedText), "\"");
        Console.WriteLine($"\nSending POST {route}: {serializedText} = {encodedText}");
        var stringContent = new StringContent(encodedText, Encoding.UTF8, "application/custom");
        var response = await client.PostAsync(route, stringContent);
        Console.WriteLine($"Response status code {response.StatusCode}");
    }

    private async Task<string> Get(HttpClient client, string route)
    {
        Console.WriteLine($"\nSending GET {route}");
        var response = await client.GetAsync(route);
        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response status code {response.StatusCode}");
        Console.WriteLine($"Result: '{result}'");
        return result;
    }

    private string Serialize(string test)
    {
        var obj = new Domain.DiffInput
        {
            Input = test
        };

        return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
    }

    private string EncodeToBase64(string serializedObj)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(serializedObj));
    }
}
