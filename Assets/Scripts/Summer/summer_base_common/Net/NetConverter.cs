using System;


namespace Summer
{
    public class NetConverter
    {
        public static int BytesToInt(byte[] bytes, int start_index)
        {
            if (bytes.Length < 4) return -1;
            Array.Reverse(bytes, start_index, 4);
            int result = BitConverter.ToInt32(bytes, start_index);
            return result;
        }

        public static byte[] IntToBytes(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return bytes;
        }
    }
}
