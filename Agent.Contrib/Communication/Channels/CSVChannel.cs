using System.Text;
using Agent.Contrib.Util;
using Microsoft.SPOT;

namespace Agent.Contrib.Communication.Channels
{
    public class CSVChannel : StringChannel, IChannel
    {
        public CSVChannel(char seperator = ',')
        {
            Seperator = seperator;
        }
        public char Seperator { get; set; }


        public object ConvertTo(byte[] input)
        {
            return ((string)base.ConvertTo(input)).Split(Seperator);
        }

        public byte[] ConvertFrom(object data)
        {
            var sList = (string[]) data;
            var full = Strings.Join((string[])sList, Seperator);
            return base.ConvertFrom(full);
        }
    }
}
