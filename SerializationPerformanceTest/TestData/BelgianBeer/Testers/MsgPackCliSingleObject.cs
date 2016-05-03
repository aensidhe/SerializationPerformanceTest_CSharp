using System.IO;
using MsgPack.Serialization;

namespace SerializationPerformanceTest.Testers
{
    class MsgPackCliSingleObject<TTestObject> : SerializationTester<TTestObject>
    {
        private readonly MessagePackSerializer<TTestObject> serializer;


        public MsgPackCliSingleObject(TTestObject testObject)
            : base(testObject)
        {
            serializer = MessagePackSerializer.Get<TTestObject>();
        }


        protected override TTestObject Deserialize()
        {
            base.MemoryStream.Position = 0;
            return serializer.UnpackSingleObject(base.MemoryStream.ToArray());
        }

        protected override MemoryStream Serialize()
        {
            return new MemoryStream(serializer.PackSingleObject(base.TestObject));
        }

    }
}