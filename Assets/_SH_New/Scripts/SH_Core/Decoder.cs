using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SpectrumHunterClient
{
    public class Decoder
    {
        [Serializable]
        public struct Message
        {
            public string msgType;
        }

        public void Decode(string message)
        {
            try
            {
                MsgBase msg = JsonUtility.FromJson<MsgBase>(message);

                switch (msg.msgType)
                {
                    case "PosRotMsg":
                        MsgPosRotUpdate posRotMsg = JsonUtility.FromJson<MsgPosRotUpdate>(message);
                        Debug.Log($"MsgPosRotUpdate received: [{ posRotMsg.ToString() }]");
                        break;
                    case "PosUpdate":
                        MsgPosUpdate PosUpdateMsg = JsonUtility.FromJson<MsgPosUpdate>(message);
                        Debug.Log($"PosUpdate received: [{ PosUpdateMsg.ToString() }]");
                        break;
                    case "LobUpdate":
                        MsgLobUpdate LobUpdateMsg = JsonUtility.FromJson<MsgLobUpdate>(message);
                        Debug.Log($"LobUpdate received: [{ LobUpdateMsg.ToString() }]");
                        // Actions to take
                        MessageHandlerClient.Instance.SignalData.bearing = LobUpdateMsg.angle;
                        MessageHandlerClient.Instance.StatusBarData.LastMessageReceived = LobUpdateMsg.ToString();
                        Quaternion lob = Quaternion.Euler(0, LobUpdateMsg.angle, 0);
                        SH_EventManager.Instance.DoUpdatePointerArrowRotation(lob);
                        //LobManager.Instance.CreateOrUpdateLOB(LobUpdateMsg.id, LobUpdateMsg.angle, 8.0f);
                        break;

                }
            }
            catch (Exception e)
            {
                Debug.Log("Could not DeSerialize FromJson<PosRotMsg>(message).  Exception Message: " + e.Message);
            }            
        }

        public string DecodeToString(string message)
        {
            string sendback = String.Empty;
            try
            {
                
                MsgBase msg = JsonUtility.FromJson<MsgBase>(message);

                switch (msg.msgType)
                {
                    case "PosRotMsg":
                        MsgPosRotUpdate posRotMsg = JsonUtility.FromJson<MsgPosRotUpdate>(message);
                        sendback = posRotMsg.ToString();
                        break;
                    case "PosUpdate":
                        MsgPosUpdate PosUpdateMsg = JsonUtility.FromJson<MsgPosUpdate>(message);
                        sendback = PosUpdateMsg.ToString();
                        break;
                    case "LobUpdate":
                        MsgLobUpdate LobUpdateMsg = JsonUtility.FromJson<MsgLobUpdate>(message);
                        sendback = LobUpdateMsg.ToString();
                        break;
                }
            }
            catch (Exception e)
            {
                Debug.Log("Could not DeSerialize FromJson<PosRotMsg>(message).  Exception Message: " + e.Message);
            }
            return sendback;
        }
    }
}
