using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace AGENT.Contrib.Drawing
{
    public class CompressedBitmapReader
    {
        byte[] _data = null;
        int _bytePosition = 0;
        int _counter = 0;
        int _pixelCounter = 0;
        Color _currColor = Color.White;

        public byte[] Data
        {
            set
            {
                _data = value;
            }
        }

        public int OffsetX
        {
            get
            {
                return (int)_data[0];
            }
        }

        public int OffsetY
        {
            get
            {
                return (int)_data[1];
            }
        }

        public int Width
        {
            get
            {
                //return IsUsingBytes ? (int)_data[2] + 1 : (int)_data[2] + 1 - 128;
                return (int)_data[2] + 1;
            }
        }

        //bool IsUsingBytes
        //{
        //    get
        //    {
        //        return _data[2] < 128;
        //    }
        //}

        public bool GetFirstPixel(ref int x, ref int y, ref Color color)
        {
            _pixelCounter = 0;
            _bytePosition = 3;
            _currColor = Color.White;
            _counter = 0;

            return GetNextPixel(ref x, ref y, ref color);;
        }

        public bool GetNextPixel(ref int x, ref int y, ref Color color)
        {
            x = OffsetX + _pixelCounter % Width;
            y = OffsetY + _pixelCounter / Width;

            while (_bytePosition < _data.Length)
            {
                if (0 == _data[_bytePosition])
                {
                    //swap color
                    _currColor = _currColor == Color.White ? Color.Black : Color.White;
                    ++_bytePosition;
                    continue;
                }

                if (_counter < _data[_bytePosition])
                {
                    color = _currColor;
                    ++_counter;
                    ++_pixelCounter;
                    return true;
                }
                else
                {
                    //swap color
                    _currColor = _currColor == Color.White ? Color.Black : Color.White;
                    //move byte position
                    ++_bytePosition;
                    //reset counter
                    _counter = 0;
                }
            }

            return false;
        }
    }
}
