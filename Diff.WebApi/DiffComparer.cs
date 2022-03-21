namespace Diff.WebApi;

public class DiffComparer : IDiffComparer
{
    private readonly IDiffRepository _diffRepository;
    public DiffComparer (IDiffRepository diffRepository)
    {
        _diffRepository = diffRepository;
    }

    public string Compare(string id)
    {
        var left = _diffRepository.GetLeft(id);
        var right = _diffRepository.GetRight(id);

        if (left == null)
            return $"left not set for id '{id}'";

        if (right == null)
            return $"right not set for id '{id}'";

        if (left == right)
            return "inputs were equal";

        if (left.Length != right.Length)
            return "inputs are of different size";

        return GetDifference(left, right);
    }

    private static string GetDifference(string left, string right)
    {
        var offset = -1;
        var length = -1;
        var output = new List<string>();

        for (var i = 0; i < left.Length; i++)
        {
            var sameChar = (left[i] == right[i]);

            if (length == -1)
            {
                if (!sameChar)
                {
                    offset = i;
                    length = 1;
                }
            }
            else
            {
                if (!sameChar)
                {
                    length++;
                }
                else
                {
                    output.Add($"difference: offset {offset}, length {length}");
                    offset = -1;
                    length = -1;
                }
            }
        }
        if (length > 0)
            output.Add($"difference: offset {offset}, length {length}");

        return string.Join(",\n", output.ToArray());
    }
}