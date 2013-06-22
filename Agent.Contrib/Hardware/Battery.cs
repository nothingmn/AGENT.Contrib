using System;
using Microsoft.SPOT;

namespace Agent.Contrib.Hardware
{
    public class Battery
    {

        /// <summary>
        /// The battery's current recharging status.
        /// </summary>
        public static int Level
        {
            get { return Microsoft.SPOT.Hardware.Battery.StateOfCharge(); }
        }

        /// <summary>
        /// The battery's voltage, in millivolts.
        /// </summary>
        public static int Voltage
        {
            get { return Microsoft.SPOT.Hardware.Battery.ReadVoltage(); }
        }

        /// <summary>
        /// The current temperature of the battery.
        /// </summary>
        public static int Temperature
        {
            get { return Microsoft.SPOT.Hardware.Battery.ReadTemperature(); }
        }

        /// <summary>
        /// true if the hardware is connected to a battery charger; otherwise, false.
        /// </summary>
        public static bool Charging
        {
            get { return Microsoft.SPOT.Hardware.Battery.OnCharger(); }
        }

        /// <summary>
        /// true only if the battery is fully charged; otherwise, false.
        /// </summary>
        public static bool FullyCharged
        {
            get { return Microsoft.SPOT.Hardware.Battery.IsFullyCharged(); }
        }

        /// <summary>
        /// The description of the battery charger model.
        /// </summary>
        public static Microsoft.SPOT.Hardware.Battery.ChargerModel ChargerModel
        {
            get { return Microsoft.SPOT.Hardware.Battery.GetChargerModel(); }
        }

        
    }
}
