using System;

namespace SpectrumHunterClient
{
    [Serializable]
    public class MsgBase
    {
        public string msgType;
        public int id;

        public override string ToString()
        {
            return $"msgType: { msgType } id: { id }";
        }

        //[NonSerialized]
        //public string exampleofnonserialized;
    }

}
