namespace ConsoleClient
{
    public class NetworkReceivedEventArgs : EventArgs
    {

        public byte[] Buffer { get; set; }
        public int Length { get; set; }

        public NetworkReceivedEventArgs(byte[] buffer, int length)
        {
            Buffer = buffer;
            Length = length;
        }

    }
}