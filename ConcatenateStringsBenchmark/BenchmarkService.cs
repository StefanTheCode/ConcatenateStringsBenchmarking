using System.Text;
using BenchmarkDotNet.Attributes;

namespace ConcatenateStringsBenchmark;

[MemoryDiagnoser]
public class BenchmarkService
{
    [Params(10, 100, 1000)]
    public int Length;

    public string testString = "Test";

    public string[] appendix;

    [GlobalSetup]
    public void Setup()
    {
        appendix = new string[Length];

        for (var i = 0; i < Length; i++)
        {
            appendix[i] = "Test";
        }
    }

    [Benchmark]
    public string UsingPlus()
    {
        testString = "Test";

        for (var i = 0; i < Length; i++)
        {
            testString += appendix[i];
        }

        return testString;
    }

    [Benchmark]
    public string UsingConcat()
    {
        testString = "Test";

        for (var i = 0; i < Length; i++)
        {
            testString = string.Concat(testString, appendix[i]);
        }

        return testString;
    }

    [Benchmark]
    public string UsingConcatWithArray()
    {
        testString = "Test";

        testString = string.Concat(appendix);

        return testString;
    }

    [Benchmark]
    public string UsingJoin()
    {
        testString = "Test";

        testString += string.Join(string.Empty, appendix);

        return testString;

    }

    [Benchmark]
    public string UsingFormat()
    {
        testString = "Test";

        for (var i = 0; i < Length; i++)
        {
            testString = string.Format("{0}{1}", testString, appendix[i]);
        }

        return testString;
    }

    [Benchmark]
    public string UsingStringBuilder()
    {
        testString = "Test";
        var builder = new StringBuilder(testString);

        for (var i = 0; i < Length; i++)
        {
            builder.Append(appendix[i]);
        }

        testString = builder.ToString();

        return testString;
    }

    [Benchmark]
    public string UsingStringBuilderAppendJoin()
    {
        testString = "Test";

        var builder = new StringBuilder(testString);

        builder.AppendJoin(String.Empty, appendix);

        testString = builder.ToString();

        return testString;
    }

    [Benchmark]
    public string UsingLinqAggregate()
    {
        testString = "Test";
        testString += appendix.Aggregate((partialPhrase, word) => $"{partialPhrase}{word}");

        return testString;
    }

    [Benchmark]
    public string UsingStringCreate()
    {
        var bufferSize = Length * testString.Length;
        return string.Create(bufferSize, (testString, Length), static (buffer, state) =>
        {
            var testStringSpan = state.testString.AsSpan();
            for (var i = 0; i < state.Length; i++)
            {
                var pos = i * state.testString.Length;
                testStringSpan.CopyTo(buffer[pos..]);
            }
        });
    }
}