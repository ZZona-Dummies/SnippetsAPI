//Rextester.Program.Main is the entry point for your code. Don't change it.
//Compiler version 4.0.30319.17929 for Microsoft (R) .NET Framework 4.5

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Rextester
{
    public class Program
    {


        public static void Main(string[] args)
        {
            SocketBuffer exampleBuffer = new SocketBuffer(new byte[1] { 0 }, "String", 0);

            //Your code goes here
            Console.WriteLine("Marshall size: {0}\nSerialized size: {1}", , GetObjectSize(exampleBuffer));
            // Marshal.OffsetOf(exampleBuffer.GetType(), "splittedData")

            Console.Read();
        }

        private static int GetObjectSize(object TestObject)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            byte[] Array;
            bf.Serialize(ms, TestObject);
            Array = ms.ToArray();
            return Array.Length;
        }
    }

    [Serializable]
    public class SocketBuffer
    {
        public static Dictionary<ushort, ulong> requestIDs = new Dictionary<ushort, ulong>(); //ulong = Numeros de bloques

        public ushort requestID;
        public IEnumerable<ulong> destsId;
        public string OriginalType;
        public IEnumerable<byte> splittedData;

        private SocketBuffer()
        { }

        public SocketBuffer(IEnumerable<byte> data, string type)
            : this(data, type, null)
        { }

        public SocketBuffer(IEnumerable<byte> data, string type, params ulong[] dests)
        {
            requestID = 0; //requestIDs.Keys.ObtainFreeID();
            requestIDs.Add(requestID, 0);

            if (dests != null) destsId = dests;
            splittedData = data;
            OriginalType = type;
        }
    }
}