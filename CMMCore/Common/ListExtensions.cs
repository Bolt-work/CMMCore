using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using System.Collections.Concurrent;

namespace CMMCore.Common;

public static class ListExtensions
{
    public static Task ParallelForEachAsync<T>(this List<T> list,
    IEnumerable<T> source,
    int degreeOfParallelization,
    Func<T, Task> body)
    {
        async Task AwaitPartition(IEnumerator<T> partition)
        {
            using (partition)
            {
                while (partition.MoveNext())
                {
                    await body(partition.Current);
                }
            }
        }

        return Task.WhenAll(
            Partitioner
            .Create(source)
            .GetPartitions(degreeOfParallelization)
            .AsParallel().Select(AwaitPartition)
            );
    }
}
