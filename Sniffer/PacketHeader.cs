using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniffer
{
    class PacketHeader
    {
        public byte[] etherDest = new byte[6];
        public byte[] etherSource = new byte[6];
        public ushort type;
        public byte version;
        public byte headerLength;
        public ushort typeOfServices;
        public ushort totalLength;
        public ushort identification;
        public byte flags;
        public byte fragmentOffset;
        public byte TTL;
        public byte protocol;
        public ushort headerChecksum;
        public UInt32 sourceIpAddress;
        public UInt32 destIpAddress;
        public byte[] data;
        public string sourceIP;
        public string destIP;

    }
   
}
