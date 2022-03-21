using Xunit;

namespace Diff.Tests;

public class DiffComparerTests
{
    [Fact]
    public void TestEqual()
    {
        var repo = new WebApi.DiffDictionaryRepository();
        repo.SetLeft("111", "eyJpbnB1dCI6InRlc3RWYWx1ZSJ9");
        repo.SetRight("111", "eyJpbnB1dCI6InRlc3RWYWx1ZSJ9");

        var sut = new WebApi.DiffComparer(repo);

        Assert.Equal("inputs were equal", sut.Compare("111"));
    }

    [Fact]
    public void TestNotSameLength()
    {
        var repo = new WebApi.DiffDictionaryRepository();
        repo.SetLeft("111", "eyJpbnB1dCI6InRlc3RWYWx1ZSJ9");
        repo.SetRight("111", "eyJpbnB1dCI6InNvbWUgdmFsdWUgdG8gYmUgY29tcGFyZWQifQ==");

        var sut = new WebApi.DiffComparer(repo);

        Assert.Equal("inputs are of different size", sut.Compare("111"));
    }

    [Fact]
    public void TestLeftNotSet()
    {
        var repo = new WebApi.DiffDictionaryRepository();
        repo.SetRight("111", "eyJpbnB1dCI6InRlc3RWYWx1ZSJ9");

        var sut = new WebApi.DiffComparer(repo);

        Assert.Equal("left not set for id '111'", sut.Compare("111"));
    }

    [Fact]
    public void TestRightNotSet()
    {
        var repo = new WebApi.DiffDictionaryRepository();
        repo.SetLeft("111", "eyJpbnB1dCI6InRlc3RWYWx1ZSJ9");

        var sut = new WebApi.DiffComparer(repo);

        Assert.Equal("right not set for id '111'", sut.Compare("111"));
    }
}