using Xunit;

namespace Diff.Tests;

public class DiffDictionaryRepositoryTests
{
    [Fact]
    public void TestLeft()
    {
        var sut = new WebApi.DiffDictionaryRepository();

        sut.SetLeft("111", "eyJpbnB1dCI6InRlc3RWYWx1ZSJ9");

        Assert.Equal("testValue", sut.GetLeft("111"));
    }

    [Fact]
    public void TestRight()
    {
        var sut = new WebApi.DiffDictionaryRepository();

        sut.SetRight("111", "eyJpbnB1dCI6InRlc3RWYWx1ZSJ9");

        Assert.Equal("testValue", sut.GetRight("111"));
    }

    [Fact]
    public void TestLeft_NotExists()
    {
        var sut = new WebApi.DiffDictionaryRepository();

        Assert.Null(sut.GetLeft("111"));
    }
}