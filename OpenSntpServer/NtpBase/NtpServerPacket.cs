using System;

namespace OpenSntpServer.NtpBase
{
    public class NtpServerPacket : NtpPacket
    {

        /// <summary>
        /// Adds the Transmit timestamp when getter is used for most accurate result
        /// </summary>
        public override byte[] Bytes { get => InsertTransmitTime(); protected set => base.Bytes = value; }

        public const int RESPONSE_SIZE = 48;

        public NtpServerPacket(byte[] clientRequest)
        {
            PrepBytes(clientRequest);
        }


        private byte[] InsertTransmitTime()
        {
            var transmitTime = GetTimeStamp();
            InsertUIntBE(transmitTime.sec, 40);
            InsertUIntBE(transmitTime.frac, 44);
            return base.Bytes;
        }

        private void PrepBytes(byte[] data)
        {
            var arrival = GetTimeStamp();
            var reference = arrival.sec - 60; //Fake reference
            byte firstByte = (byte)(data[0] & 0b00111000); //Copy version from client
            firstByte = (byte)(firstByte | 0b00000100); //Set mode 4 = server
            base.Bytes[0] = firstByte;
            base.Bytes[1] = 1; //Stratum 
            base.Bytes[2] = data[2]; //copy poll
            base.Bytes[3] = unchecked((byte)(-6)); //Precision
            //Skip 8 Bytes Root Delay Root Dispersion
            //LOCL as reference
            base.Bytes[12] = 0x4C;
            base.Bytes[13] = 0x4F;
            base.Bytes[14] = 0x43;
            base.Bytes[15] = 0x4C;
            InsertUIntBE(reference, 16);
            //Copy client transmit
            for (int i = 0; i < 8; i++)
            {
                base.Bytes[24 + i] = data[40 + i];
            }
            InsertUIntBE(arrival.sec, 32);
            InsertUIntBE(arrival.frac, 36);
        }

        private (uint sec, uint frac) GetTimeStamp()
        {
            var nowUtc = DateTime.Now.ToUniversalTime();
            var seconds = (nowUtc.Subtract(BaseDate).TotalSeconds);
            var secs = (uint)Math.Floor(seconds);
            var fractions = (uint)((seconds - secs) * (1UL << 32));
            return (secs, fractions);
        }




    }
}
