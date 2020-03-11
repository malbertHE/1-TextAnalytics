using System.Collections.Generic;
using System.Threading;

namespace MAF.Entropy
{
    /// <summary>Entrópia számítás szálainak paraméterei. Technikai osztály.</summary>
    class ThreadObject
    {
        internal int TextListCount { get { return threadParams.TextListCount; } }

        internal void AddTextList(string pLine)
        {
            threadParams.AddTextList(pLine);
        }

        internal string GetTextList(int i)
        {
            return threadParams.GetTextList(i);
        }

        internal void AddEntropyResult(EntropyResult pEntropyResult)
        {
            threadParams.AddEntropyResult(pEntropyResult);
        }

        internal EntropyResult FindEntropyResult(string pLogicName)
        {
            return threadParams.FindEntropyResult(pLogicName);
        }

        internal List<EntropyResult> EntropyResultList { get { return threadParams.EntropyResultList; } }

        internal void Start()
        {
            thread.Start(threadParams);
        }

        internal void SetAndStartThread(Thread pThread)
        {
            thread = pThread;
            thread.Start(threadParams);
        }

        internal void ThreadJoin()
        {
            thread.Join();
        }

        private Thread thread;
        private ThreadParams threadParams = new ThreadParams();

    }
}
