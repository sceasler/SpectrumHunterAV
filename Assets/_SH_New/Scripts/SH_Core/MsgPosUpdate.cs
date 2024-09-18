using System;

namespace SpectrumHunterClient
{
    [Serializable]
    public class MsgPosUpdate : MsgBase
    {
        public float posX;
        public float posY;
        public float posZ;

        public MsgPosUpdate(string msgType, int id, float posX, float posY, float posZ)
        {
            this.msgType = msgType;
            this.id = id;
            this.posX = posX;
            this.posY = posY;
            this.posZ = posZ;
        }

        public override string ToString()
        {
            return $"{ base.ToString() } posX: { posX.ToString() } posY: { posY.ToString() } posZ: { posZ.ToString() }";
        }
    }
}

