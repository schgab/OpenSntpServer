using System;

namespace OpenSntpServer.NtpBase
{
    public abstract class NtpPacket
    {
        public static DateTime BaseDate = new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc);


        private byte[] _bytes;
        public virtual byte[] Bytes
        {
            get { return _bytes; }
            protected set { _bytes = value; }
        }


        public DateTime UTCTime => calculateUTCTime();

        private DateTime calculateUTCTime()
        {
            ulong seconds = SwapEndianess(BitConverter.ToUInt32(Bytes, 40));
            ulong fraction = SwapEndianess(BitConverter.ToUInt32(Bytes, 44));
            var dt = BaseDate.AddMilliseconds(seconds * 1000);
            double ms = (fraction * 1000.0) / (1UL << 32);
            dt.AddMilliseconds(ms);
            return dt;
        }


        public NtpPacket()
        {
            Bytes = new byte[48];
        }

        protected uint SwapEndianess(uint x)
        {
            return (((x & 0xff000000) >> 24) +
                    ((x & 0x00ff0000) >> 8) +
                    ((x & 0x0000ff00) << 8) +
                    ((x & 0x000000ff) << 24));
        }

        protected void InsertUIntBE(uint num, int offset)
        {
            _bytes[offset] = (byte)(num >> 24);
            _bytes[offset + 1] = (byte)(num >> 16);
            _bytes[offset + 2] = (byte)(num >> 8);
            _bytes[offset + 3] = (byte)(num & 0x000000FF);
        }


    }
}
