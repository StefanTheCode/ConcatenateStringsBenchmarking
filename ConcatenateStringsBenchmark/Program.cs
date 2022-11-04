using BenchmarkDotNet.Running;
using ConcatenateStringsBenchmark;

BenchmarkRunner.Run<BenchmarkService>();


//BenchmarkService service = new();

//string concat = service.UsingConcat();
//string stringBuilder = service.UsingStringBuilder();
//string stringBuilderAppendJoin = service.UsingStringBuilderAppendJoin();
//string concatWithArray = service.UsingConcatWithArray();
//string format = service.UsingFormat();
//string join = service.UsingJoin();
//string linq = service.UsingLinqAggregate();
//string plusOperator = service.UsingPlus();