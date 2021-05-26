
using System;
using System.Linq;

namespace TelEnvyXmlLib.Sockets
{
    //class Framer
    //{
    //    public static byte[] nextToken(Stream input, byte[] delimiter)
    //    {
    //        int  nextByte;

    //        // if the stream has already ended return null
    //        if ((nextByte = input.ReadByte()) == -1)
    //        {
    //            MemoryStream tokenBuffer = new MemoryStream();
    //            do
    //            {
    //                tokenBuffer.WriteByte((byte)nextByte);
    //                byte[] currentToken = tokenBuffer.ToArray();
    //                if (endsWith(currentToken,delimiter))
    //                    {
    //                    int tokenLength = currentToken.Length - delimiter.Length;
    //                    byte[] token = new byte[tokenLength];
    //                    Array.Copy(currentToken, 0, token, 0, tokenLength);
    //                    }
    //                
    //            } while ((nextByte = input.ReadByte()) != -1);
    //        }
    //    }
    //    private static bool endsWith(byte[] value, byte[] suffix)
    //    {
    //        if (value.Length < suffix.Length) return false;
    //        for (int offset = 0; offset < suffix.Length; offset++)
    //        {
    //            if (value[value.Length - offset] != suffix[suffix.Length - offset])
    //                return false;
    //        }
    //        return true;
    //    }

    //}
}
