using System.IO;
using System.Xml.Serialization;

namespace SerializationPerformanceTest.Testers
{
    class XmlSerializer<TTestObject> : SerializationTester<TTestObject>{
        private readonly XmlSerializer serializer;

        public XmlSerializer(TTestObject testObject)
            : base(testObject)
        {
            serializer = new XmlSerializer(typeof(TTestObject));
            
        }

        protected override TTestObject Deserialize()
        {
            base.MemoryStream.Seek(0, 0);
            var deserialize = (TTestObject)serializer.Deserialize(base.MemoryStream);
            return deserialize;
        }

        protected override MemoryStream Serialize()
        {
            var stream = new MemoryStream();
            serializer.Serialize(stream, base.TestObject);
            return stream;
        }
    }
}