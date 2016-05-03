using System.IO;

namespace SerializationPerformanceTest.Testers
{
    class Protobuf<TTestObject> : SerializationTester<TTestObject>
    {
        public Protobuf(TTestObject testObject)
            : base(testObject)
        {
        }

        protected override TTestObject Deserialize()
        {
            base.MemoryStream.Seek(0, 0);
            return ProtoBuf.Serializer.Deserialize<TTestObject>(base.MemoryStream);
        }
        
        protected override MemoryStream Serialize()
        {
            var stream = new MemoryStream();
            ProtoBuf.Serializer.Serialize(stream, base.TestObject);
            return stream;
        }
    }
}