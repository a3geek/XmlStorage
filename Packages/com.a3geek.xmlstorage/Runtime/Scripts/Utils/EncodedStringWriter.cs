using System.IO;
using System.Text;

namespace XmlStorage.Utils
{
    internal class EncodedStringWriter : StringWriter
    {
        public override Encoding Encoding => this.encoding;

        protected readonly Encoding encoding = Encoding.UTF8;


        public EncodedStringWriter() { }

        public EncodedStringWriter(in Encoding encoding) : this()
        {
            this.encoding = encoding ?? this.encoding;
        }
    }
}
