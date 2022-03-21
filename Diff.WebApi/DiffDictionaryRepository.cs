namespace Diff.WebApi;

public class DiffDictionaryRepository : IDiffRepository
{
    private Dictionary<string, string> _left;
    private Dictionary<string, string> _right;

    public DiffDictionaryRepository()
    {
        _left = new Dictionary<string, string>();
        _right = new Dictionary<string, string>();
    }

    public void SetLeft(string id, string val)
    {
        _left[id] = DeserializeVal(val);
    }

    public void SetRight(string id, string val)
    {
        _right[id] = DeserializeVal(val);
    }

    public string GetLeft(string id)
    {
        return (_left.ContainsKey(id)) ? _left[id] : null;
    }

    public string GetRight(string id)
    {
        return (_right.ContainsKey(id)) ? _right[id] : null;
    }

    private string DeserializeVal(string val)
    {
        try
        {
            var decodedBase64 = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(val));
            var inputObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Domain.DiffInput>(decodedBase64);
            return inputObj.Input;
        }
        catch (Exception)
        {
            throw;
        }
    }
}