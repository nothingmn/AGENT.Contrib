using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;

namespace GifCompressor
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                System.Console.WriteLine("Usage: app.exe image.gif");
                return;
            }

            foreach (string file in args)
            {
                byte offsetX = 0;
                byte offsetY = 0;

                string txtPath = file.Substring(0, file.Length - 4) + ".txt";
                if(File.Exists(txtPath))
                {
                    using (StreamReader ftxt = File.OpenText(txtPath))
                    {
                        offsetX = (byte)int.Parse(ftxt.ReadLine());
                        offsetY = (byte)int.Parse(ftxt.ReadLine());
                    }
                }

                using (FileStream fout = File.Create(file + ".bin"))
                {
                    //save offset
                    byte[] offset = { offsetX, offsetY };
                    fout.Write(offset, 0, offset.Length);

                    using (FileStream fs = File.OpenRead(file))
                    {
                        using (Bitmap bmp = new Bitmap(fs))
                        {
                            List<int> numbers = new List<int>();

                            bool white = true;
                            int currCount = 0;
                            
                            for (int y = 0; y < bmp.Height; ++y)
                            {
                                for (int x = 0; x < bmp.Width; ++x)
                                {
                                    Color c = bmp.GetPixel(x, y);

                                    if (c.R == 0 && c.G == 0 && c.B == 0)
                                    {
                                        if (white)
                                        {
                                            numbers.Add(currCount);
                                            white = false;
                                            currCount = 0;
                                        }

                                        ++currCount;
                                    }
                                    else //this is white
                                    {
                                        if (!white)
                                        {
                                            numbers.Add(currCount);
                                            white = true;
                                            currCount = 0;
                                        }

                                        ++currCount;
                                    }
                                }
                            }

                            if (currCount > 0)
                            {
                                numbers.Add(currCount);
                            }

                            //use bytes
                            //save width
                            fout.Write(new byte[] { (byte)(bmp.Width - 1) }, 0, 1);

                            foreach (int num in numbers)
                            {
                                int number = num;
                                if (number > 255)
                                {
                                    while (number > 255)
                                    {
                                        fout.Write(new byte[] { (byte)255, (byte)0 }, 0, 2);
                                        number -= 255;
                                    }

                                    if (number > 0)
                                    {
                                        fout.Write(new byte[] { (byte)number }, 0, 1);
                                    }
                                }
                                else
                                {
                                    fout.Write(new byte[] { (byte)number }, 0, 1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
