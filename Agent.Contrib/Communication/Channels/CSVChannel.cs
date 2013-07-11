using System.Text;
using Agent.Contrib.Util;
using Microsoft.SPOT;

namespace Agent.Contrib.Communication.Channels
{
    public class CSVChannel : BaseChannel, IChannel
    {
        public CSVChannel()
        {
            Seperator = ',';
        }
        public char Seperator { get; set; }
        /// <summary>
        /// takes our serial port and converts grabs the data, converts it to a string and then returns a string[]
        /// </summary>
        /// <param name="port"></param>
        /// <returns>string[]</returns>
        public override object Read(System.IO.Ports.SerialPort port)
        {
            string result = (base.GetString(port) as string);
            return result.Split(Seperator);

        }

        /// <summary>
        /// Takes a string[], and converts it to a UTF8 string and then to byte[] and writes it to the serial port
        /// </summary>
        /// <param name="port"></param>
        /// <param name="Data"></param>
        public override void Write(System.IO.Ports.SerialPort port, object data)
        {
            string[] payload = (data as string[]);
            var d = System.Text.Encoding.UTF8.GetBytes(Strings.Join(payload, Seperator));
            port.Write(d, 0, d.Length);
        }
    }
}
