using System;
using System.Buffers.Binary;
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


        public DateTime UTCTime => CalculateUTCTime();

        private DateTime CalculateUTCTime()
        {
            ulong seconds = BinaryPrimitives.ReadUInt32BigEndian(Bytes.AsSpan(40,4));
            ulong fraction = BinaryPrimitives.ReadUInt32BigEndian(Bytes.AsSpan(44,4));            
            var dt = BaseDate.AddMilliseconds(seconds * 1000);
            double ms = (fraction * 1000.0) / (1UL << 32);
            dt.AddMilliseconds(ms);
            return dt;
        }


        public NtpPacket()
        {
            Bytes = new byte[48];
        }
    
        protected void ChangeUIntBE(uint num, int offset)
        {
            BinaryPrimitives.WriteUInt32BigEndian(_bytes.AsSpan(offset),num);
        }


    }
}
