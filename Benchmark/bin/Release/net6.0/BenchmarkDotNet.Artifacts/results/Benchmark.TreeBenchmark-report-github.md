``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.2604/22H2/2022Update)
11th Gen Intel Core i5-11400 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK=7.0.202
  [Host]     : .NET 6.0.15 (6.0.1523.11507), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.15 (6.0.1523.11507), X64 RyuJIT AVX2


```
|           Method |            Mean |         Error |        StdDev | Rank |   Gen0 | Allocated |
|----------------- |----------------:|--------------:|--------------:|-----:|-------:|----------:|
|    TreeBuildTest | 8,082,550.39 ns | 46,910.339 ns | 36,624.507 ns |    2 |      - |   37537 B |
| SortRootNodeTest |        54.87 ns |      0.508 ns |      0.475 ns |    1 | 0.0370 |     232 B |
