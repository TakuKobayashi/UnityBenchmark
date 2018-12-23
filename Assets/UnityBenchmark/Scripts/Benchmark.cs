using System;
using System.Collections;
using UnityEngine;

namespace UnityBenchmark
{
    public class Benchmark
    {
        /// <summary>
        /// <para>引数のActionの中で行われる処理にかかる時間をms単位で計測し、Logに出力する</para>
        /// <para>【第1引数】実行したい処理</para>
        /// </summary>
        public static TimeSpan Execute(Action process)
        {
            return Execute(1, process);
        }

        /// <summary>
        /// <para>引数のActionの中で行われる処理にかかる時間をms単位で計測し、Logに出力する</para>
        /// <para>【第1引数】実行したい処理</para>
        /// </summary>
        public static TimeSpan Execute(int times, Action process)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            for(int i = 0;i < times; ++i)
            {
                process();
            }
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            Debug.Log(string.Format("{0} ms", ts.TotalMilliseconds));
            return ts;
        }

        /// <summary>
        /// <para>引数のActionの中で行われる処理にかかる時間をms単位で計測し、Logに出力する(Corutineで行う場合)</para>
        /// <para>【第1引数】実行したい処理(コルーチンの処理yield returnを書いて実行する)</para>
        /// </summary>
        public static BenchmarkBehaviour ExecuteCoroutine(Func<IEnumerator> process)
        {
            return ExecuteCoroutine(1, process);
        }

        /// <summary>
        /// <para>引数のActionの中で行われる処理にかかる時間をms単位で計測し、Logに出力する(Corutineで行う場合)</para>
        /// <para>【第1引数】実行したい処理(コルーチンの処理yield returnを書いて実行する)</para>
        /// </summary>
        public static BenchmarkBehaviour ExecuteCoroutine(int times, Func<IEnumerator> process)
        {
            GameObject benchMarkObject = new GameObject();
            BenchmarkBehaviour benchmarkBehaviour = benchMarkObject.AddComponent<BenchmarkBehaviour>();
            benchmarkBehaviour.Execute(times, process);
            return benchmarkBehaviour;
        }
    }
}
