using System.IO;
using System.Text;

namespace XmlStorage.Utils
{
    public class EncodedStringWriter : StringWriter
    {
        public override Encoding Encoding => this.encoding;

        protected readonly Encoding encoding = Encoding.UTF8;


        public EncodedStringWriter() : base()
        {
        }

        public EncodedStringWriter(Encoding encoding) : this()
        {
            this.encoding = encoding ?? this.encoding;
        }
    }
}
