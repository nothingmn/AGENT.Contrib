using System;
using System.Collections;
using System.Reflection;
using System.Text;
using Microsoft.SPOT;

namespace Agent.Contrib.Communication.Channels
{
    public class PocoChannel : CSVChannel
    {
        public char PropertySeperator { get; set; }
        public Type ValueType { get; set; }

        public PocoChannel(Type valueType)
        {
            PropertySeperator = '|';
            ValueType = valueType;
        }

        /// <summary>
        /// Read will take the underlying CSV object and translate that back to the properties on the class
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public override object Read(System.IO.Ports.SerialPort port)
        {
            var newObj = Reflection.Utilities.LoadByType(ValueType);
            string[] csvData = (base.Read(port) as string[]);
            //propertyname|propertyvalue,propertyname1|propertyvalue1
            for (int i = 0; i < csvData.Length; i++)
            {
                string propData = csvData[i];
                var both = propData.Split(PropertySeperator);
                string name = both[0];
                string value = both[1];
                ValueType.InvokeMember(name, BindingFlags.Default, null, newObj, new object[] {value as object});
            }
            return newObj;
        }

        /// <summary>
        /// Write will take the properties on the class and translate them to a a CSV stream
        /// </summary>
        /// <param name="port"></param>
        /// <param name="data"></param>
        public override void Write(System.IO.Ports.SerialPort port, object data)
        {
            ArrayList lst = new ArrayList();
            var methods = ValueType.GetMethods();
            foreach (var methodInfo in methods)
            {
                try
                {
                    if (methodInfo.Name.Substring(0, 4) == "get_")
                    {
                        string name = methodInfo.Name.Substring(4);
                        var value = methodInfo.Invoke(data, null);
                        lst.Add(name + PropertySeperator + value);
                    }
                }
                catch (Exception)
                {
                }
            }
            base.Write(port, lst.ToArray());
        }

    }
}
