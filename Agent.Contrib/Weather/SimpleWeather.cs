using System;
using System.Collections;
using Microsoft.SPOT;

namespace Agent.Contrib.Weather
{
    public class SimpleWeather : IProvideWeather
    {

        public IForecast CurrentForecast
        {
            get
            {
                return new DayForecast()
                    {
                        ChanceOfPrecipitation = 35,
                        TimeStamp = Settings.SettingsProvider.Current.Now,
                        Details = "Sunny with a chance of showers",
                        IsCelsius = true,
                        Temperature = 30,
                        TimeName = "Day"
                    };
            }
        }

        private System.Random rnd = new Random();

        private IForecast SampleForecast(int dayOffSet)
        {
            var cf = CurrentForecast;
            cf.TimeStamp = cf.TimeStamp.AddDays(dayOffSet);
            cf.Temperature = rnd.Next(40);
            return cf;
        }

        public ArrayList CurrentWeekForecast
        {
            get
            {
                return new ArrayList()
                    {
                        SampleForecast(0),
                        SampleForecast(1),
                        SampleForecast(2),
                        SampleForecast(3),
                        SampleForecast(4),
                        SampleForecast(5),
                        SampleForecast(6),
                        SampleForecast(7)
                    };
            }
        }
    }
}