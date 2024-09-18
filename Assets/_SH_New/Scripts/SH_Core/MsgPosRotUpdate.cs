using System;

namespace SpectrumHunterClient
{
    [Serializable]
    public class MsgPosRotUpdate : MsgBase
    {
        public float posX;
        public float posY;
        public float posZ;
        public float rotX;
        public float rotY;
        public float rotZ;

        public MsgPosRotUpdate(string msgType, int id, float posX, float posY, float posZ, float rotX, float rotY, float rotZ)
        {
            this.msgType = msgType;
            this.id = id;
            this.posX = posX;
            this.posY = posY;
            this.posZ = posZ;
            this.rotX = rotX;
            this.rotY = rotY;
            this.rotZ = rotZ;
        }

        public override string ToString()
        {
            return $"{ base.ToString() } posX: { posX.ToString() } posY: { posY.ToString() } posZ: { posZ.ToString() } " +
                $"rotX: { rotX.ToString() } rotY: { rotY.ToString() } rotZ: { rotZ.ToString() }";
        }
    }
}

