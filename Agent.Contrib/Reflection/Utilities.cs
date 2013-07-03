using System;
using System.Collections;
using Microsoft.SPOT;

namespace Agent.Contrib.Reflection
{
    public class Utilities
    {
        public static bool IsInterfaceType(Type sourceType, Type interfaceType)
        {
            var interfaces = sourceType.GetInterfaces();
            foreach (var i in interfaces)
            {
                if (i == interfaceType) return true;
            }
            return false;
        }

        public static object LoadByType(Type type)
        {
            try
            {
                if (type != null)
                {
                    var ctor = type.GetConstructor(new Type[0]);
                    if (ctor != null)
                    {
                        return ctor.Invoke(new object[0]);
                    }
                }
            }
            catch (Exception)
            {
            }

            return null;
        }

        public static ArrayList FindByInterface(Type interfaceType)
        {
            ArrayList list = new ArrayList();
            Type[] types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
            foreach (var t in types)
            {
                try
                {
                    if (IsInterfaceType(t, interfaceType))
                    {
                        list.Add(t);
                    }
                }
                catch (Exception)
                {
                }
            }
            return list;
        }
    }
}
