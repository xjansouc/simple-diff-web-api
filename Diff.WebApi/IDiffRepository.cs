namespace Diff.WebApi;

public interface IDiffRepository
{
    void SetLeft(string id, string val);

    void SetRight(string id, string val);

    string GetLeft(string id);

    string GetRight(string id);
}