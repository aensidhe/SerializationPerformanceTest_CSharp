using System.IO;
using MsgPack.Serialization;

namespace SerializationPerformanceTest.Testers
{
    class MsgPackCli<TTestObject> : SerializationTester<TTestObject>
    {
        private readonly MessagePackSerializer<TTestObject> serializer;


        public MsgPackCli(TTestObject testObject)
            : base(testObject)
        {
            serializer = MessagePackSerializer.Get<TTestObject>();
        }


        protected override TTestObject Deserialize()
        {
            base.MemoryStream.Position = 0;
            return serializer.Unpack(base.MemoryStream);
        }
        
        protected override MemoryStream Serialize()
        {
            var stream = new MemoryStream();
            serializer.Pack(stream, base.TestObject);
            return stream;
        }

    }
}