using System;

namespace SpectrumHunterClient
{
    public class Server : IEquatable<Server>
    {
        public string ServerIp { get; set; }

        public Server(string ip)
        {
            ServerIp = ip;
        }

        public bool Equals(Server other)
        {
            if (other == null) return false;
            return (this.ServerIp.Equals(other.ServerIp));
        }
    }
}
