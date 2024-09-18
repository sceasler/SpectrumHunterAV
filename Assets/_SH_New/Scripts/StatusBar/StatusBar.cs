using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpectrumHunterClient
{
    public class StatusBar
    {
        public string TextualStatus { get; set; }
        public string LastMessageReceived { get; set; }
        public string TimeOfLastMessage { get; set; }
        public Status CurrentStatus { get; set; }
        public string ServerIp { get; set; }
        public StatusBar() {
            TextualStatus = "No Server";
            LastMessageReceived = string.Empty;
            TimeOfLastMessage = "None";
            CurrentStatus = Status.Neutral;
            ServerIp = string.Empty;
        }

    }
}

