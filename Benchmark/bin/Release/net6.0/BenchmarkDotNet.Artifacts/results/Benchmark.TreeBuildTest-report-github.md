``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.2604/22H2/2022Update)
11th Gen Intel Core i5-11400 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK=7.0.200
  [Host]     : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT AVX2


```
| Method |     Mean |    Error |   StdDev | Rank |      Gen0 |    Gen1 | Allocated |
|------- |---------:|---------:|---------:|-----:|----------:|--------:|----------:|
|   Test | 28.52 ms | 0.116 ms | 0.109 ms |    1 | 8937.5000 | 62.5000 |  53.47 MB |
