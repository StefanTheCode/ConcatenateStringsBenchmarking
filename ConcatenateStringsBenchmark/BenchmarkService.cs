using System.Text;
using BenchmarkDotNet.Attributes;

namespace ConcatenateStringsBenchmark
{
    [MemoryDiagnoser]
    public class BenchmarkService
    {
        [Params(10, 100, 1000)]
        public  int Length;

        public  string testString = "Test";

        public  string[] appendix;

        public BenchmarkService()
        {
        }

        [GlobalSetup]
        public void Setup()
        {
            this.appendix = new string[this.Length];

            for (var i = 0; i < this.Length; i++)
            {
                this.appendix[i] = "Test";
            }
        }

        [Benchmark]
        public string UsingPlus()
        {
            testString = "Test";

            for (int i = 0; i < Length; i++)
            {
                testString += appendix[i];
            }

            return testString;
        }

        [Benchmark]
        public string UsingConcat()
        {
            testString = "Test";

            for (int i = 0; i < Length; i++)
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

            for (int i = 0; i < Length; i++)
            {
                testString = string.Format("{0}{1}", testString, appendix[i]);
            }

            return testString;
        }

        [Benchmark]
        public string UsingStringBuilder()
        {
            testString = "Test";
            StringBuilder builder = new StringBuilder(testString);

            for (int i = 0; i < Length; i++)
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

            StringBuilder builder = new StringBuilder(testString);

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
    }
}