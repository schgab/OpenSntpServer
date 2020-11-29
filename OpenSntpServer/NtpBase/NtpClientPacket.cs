namespace OpenSntpServer.NtpBase
{
    public class NtpClientPacket : NtpPacket
    {
        public NtpClientPacket(byte[] serverResponse)
        {
            Bytes = serverResponse;
        }
    }
}
