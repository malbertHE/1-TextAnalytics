using System.Collections.Generic;

namespace MAF.Entropy
{
    class ThreadParams
    {
        internal int TextListCount { get { return textList.Count; } }

        internal void AddTextList(string pLine)
        {
            textList.Add(pLine);
        }

        internal string GetTextList(int pIdx)
        {
            return textList[pIdx];
        }

        internal EntropyResult FindEntropyResult(string pLogicName)
        {
            return entropyResultList.Find(x => x.Logic.Name == pLogicName);
        }

        internal void AddEntropyResult(EntropyResult pEntropyResult)
        {
            entropyResultList.Add(pEntropyResult);
        }

        internal List<EntropyResult> EntropyResultList { get { return entropyResultList; } } //Nincs védve a lista!


        List<string> textList = new List<string>();
        List<EntropyResult> entropyResultList = new List<EntropyResult>();
    }
}