using System;

namespace SpectrumHunterClient
{
    [Serializable]
    public class MsgLobUpdate : MsgBase
    {
        public float angle;

        public MsgLobUpdate(string msgType, int id, float angle)
        {
            this.msgType = msgType;
            this.id = id;
            this.angle = angle;
        }

        public override string ToString()
        {
            return $"{ base.ToString() } angle: { angle.ToString() }";
        }
    }
}

