using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;

namespace Agent.Contrib.Weather
{

    public delegate void WeatherUpdated(IWeatherForecast _weatherForecastProvider);

    public class SimpleWeatherForecast : IWeatherForecast
    {

        private IForecast RandomForecast
        {
            get
            {
                 return new DayForecast()
                    {
                        ChanceOfPrecipitation = rnd.Next(100),
                        TimeStamp = Settings.SettingsProvider.Current.Now,
                        Details = "Sunny with a chance of showers",
                        IsCelsius = true,
                        Temperature = rnd.Next(40),
                        TimeName = "Day"
                    };
            }
        }
        public IForecast CurrentForecast
        {
            get { return currentWeek[0] as IForecast; }
        }

        public SimpleWeatherForecast()
        {

            SetupNewSetOfRandomData();
            geoTimer = new Timer(updateWeather, null, 5000, 5000);
        }
        private void SetupNewSetOfRandomData()
        {
            currentWeek = new ArrayList()
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

        private System.Threading.Timer geoTimer;
        private void updateWeather(object state)
        {
            if (OnWeatherUpdated != null)
            {
                Debug.Print("Weather info has been upadted:" + CurrentForecast.TimeStamp.ToString());
                SetupNewSetOfRandomData();
                Debug.Print("Weather info has been upadted:" + CurrentForecast.TimeStamp.ToString());
                OnWeatherUpdated(this);
            }
        }

        private System.Random rnd = new Random();

        private IForecast SampleForecast(int dayOffSet)
        {
            var cf = RandomForecast;
            cf.TimeStamp = cf.TimeStamp.AddDays(dayOffSet);
            return cf;
        }

        private ArrayList currentWeek = null;

        public ArrayList CurrentWeekForecast
        {
            get { return currentWeek; }
        }

        public event WeatherUpdated OnWeatherUpdated;
    }
}