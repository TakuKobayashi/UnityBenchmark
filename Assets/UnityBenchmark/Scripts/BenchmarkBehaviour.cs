using System;
using System.Collections;
using UnityEngine;

namespace UnityBenchmark
{
    public class BenchmarkBehaviour : MonoBehaviour
    {
        internal void Execute(int times, Func<IEnumerator> process)
        {
            StartCoroutine(ExecuteCoroutine(times, process));
        }

        private IEnumerator ExecuteCoroutine(int times, Func<IEnumerator> process)
        {
            System.Diagnostics.Stopwatch totalStopWatch = new System.Diagnostics.Stopwatch();
            TimeSpan totalTime = new TimeSpan();
            for (int i = 0; i < times; ++i)
            {
                System.Diagnostics.Stopwatch stepStopWatch = new System.Diagnostics.Stopwatch();
                TimeSpan stepTime = new TimeSpan();
                totalStopWatch.Start();
                IEnumerator ie = process();
                while (ie.MoveNext())
                {
                    stepStopWatch.Start();
                    yield return ie.Current;
                    stepStopWatch.Stop();
                    TimeSpan onestepTime = stepStopWatch.Elapsed - stepTime;
                    stepTime += onestepTime;
                    totalStopWatch.Stop();
                    Debug.Log(string.Format("onesteptime: {0} ms", stepTime.TotalMilliseconds));
                    totalStopWatch.Start();
                }
                totalStopWatch.Stop();
                TimeSpan currentTime = totalStopWatch.Elapsed - totalTime;
                totalTime += currentTime;
                Debug.Log(string.Format("onetime: {0} ms", currentTime.TotalMilliseconds));
            }
            Debug.Log(string.Format("total: {0} ms", totalTime.TotalMilliseconds));
            Destroy(this.gameObject);
        }
    }
}
