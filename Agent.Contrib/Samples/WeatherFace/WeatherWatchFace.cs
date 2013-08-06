using System;
using AGENT.Contrib;
using AGENT.Contrib.Drawing;
using AGENT.Contrib.Face;
using AGENT.Contrib.Weather;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace WeatherFace
{
    public class WeatherWatchFace : IFace
    {
        public AGENT.Contrib.Settings.ISettings Settings { get; set; }

        public IWeatherForecast CurrentConditions { get; set; }

        private Drawing drawing = new Drawing();
        private Font font = Resources.GetFont(Resources.FontResources.Digital714point);
        private Font bigfont = Resources.GetFont(Resources.FontResources.Digital748TimeOnly);
        private Font smallFont = Resources.GetFont(Resources.FontResources.small);

        private int days = 3;
        private int buffer = 7;
        private int top = 10;

        public WeatherWatchFace()
        {
            CurrentConditions = WeatherProvider.Current;
            CurrentConditions.OnWeatherUpdated += CurrentConditions_OnWeatherUpdated;
        }

        void CurrentConditions_OnWeatherUpdated(IWeatherForecast _weatherForecastProvider)
        {
            if (_screen != null)
            {
                //clear the display
                _screen.Clear();

                Render(null);


                //flush the image out to the device
                _screen.Flush();
            }
        }

        private Bitmap _screen = null;

        public void Render(Bitmap screen)
        {
            
            if (_screen == null) _screen = screen;
            _screen.DrawLine(Color.White, 2, 0, AGENT.Contrib.Device.Size / 2, AGENT.Contrib.Device.Size, AGENT.Contrib.Device.Size / 2);

            DateTime now = DateTime.Now;
            int counter = (int) now.DayOfWeek;
            counter++;
            int left = buffer;
            for (int x = 0; x <= days; x++)
            {
                if (counter >= 7) counter = 0;
                string dayName = System.Globalization.DateTimeFormatInfo.CurrentInfo.DayNames[counter];
                if (dayName.Length >= 3) dayName = dayName.Substring(0, 3);
                int width = drawing.MeasureString(dayName, font);
                _screen.DrawText(dayName, font, Color.White, left, top);
                IForecast current = CurrentConditions.CurrentWeekForecast[x] as IForecast;
                _screen.DrawText(current.Temperature.ToString(), font, Color.White, left + 4, top + font.Height + 2);
                counter++;
                if (counter > 6) counter = 0;
                left += width + buffer;
            }
            string display = CurrentConditions.CurrentForecast.Temperature.ToString();
            int forecastLeft = AGENT.Contrib.Device.Size - drawing.MeasureString(display, bigfont);
            _screen.DrawText(display, bigfont, Color.White, forecastLeft, (AGENT.Contrib.Device.Size/2) + 2);
            _screen.DrawText(CurrentConditions.CurrentForecast.TimeStamp.ToString(), smallFont, Color.White, 3, (AGENT.Contrib.Device.Size / 2) - smallFont.Height - 1);

        }

        public int UpdateSpeed
        {
            get { return 60*1000; }
        }
    }
}
