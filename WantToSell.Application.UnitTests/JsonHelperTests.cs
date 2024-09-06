using System.Text.Json;
using WantToSell.Application.Helpers;
using Xunit;

namespace WantToSell.Application.UnitTests;

public class JsonHelperTests
{
    private class TestClass
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public TestClass TestObject { get; set; }
    }
    
    [Fact]
    public void Serialize_WithObject_ShouldReturnIndentedJson()
    {
        // Arrange
        var testObj = new TestClass
        {
            Name = "John",
            Age = 30
        };

        // Act
        var json = JsonHelper.Serialize(testObj);

        // Assert
        Assert.Contains("\n", json);
        Assert.Contains("\"Name\": \"John\"", json);
        Assert.Contains("\"Age\": 30", json);
    }

    [Fact]
    public void Serialize_WithPrimitiveType_ShouldReturnJson()
    {
        // Arrange
        var value = 10;

        // Act
        var json = JsonHelper.Serialize(value);

        // Assert
        Assert.Equal("10", json);
    }

    [Fact]
    public void Serialize_WithNullObject_ShouldReturnEmptyJsonObject()
    {
        // Arrange
        TestClass testObj = null;

        // Act
        var json = JsonHelper.Serialize(testObj);

        // Assert
        Assert.Equal("null", json);
    }

    [Fact]
    public void Serialize_WithObjectHavingNullProperties_ShouldIgnoreNullProperties()
    {
        // Arrange
        var testObj = new TestClass
        {
            Name = "John",
            Age = 30,
            TestObject = null
        };

        // Act
        var json = JsonHelper.Serialize(testObj);

        // Assert
        Assert.Contains("\"Name\": \"John\"", json);
        Assert.Contains("\"Age\": 30", json);
        Assert.DoesNotContain("Friend", json); 
    }

    [Fact]
    public void Deserialize_WithValidJson_ShouldReturnObject()
    {
        // Arrange
        var json = "{\"Name\":\"John\",\"Age\":30}";

        // Act
        var result = JsonHelper.Deserialize<TestClass>(json);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("John", result.Name);
        Assert.Equal(30, result.Age);
    }

    [Fact]
    public void Deserialize_WithInvalidJson_ShouldThrowException()
    {
        // Arrange
        var json = "{\"Name\":\"John\",\"Age\":30"; // invalid - missing closing bracket

        // Act & Assert
        Assert.Throws<JsonException>(() => JsonHelper.Deserialize<TestClass>(json));
    }

}