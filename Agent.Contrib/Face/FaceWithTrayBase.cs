using System;
using Agent.Contrib.Notifications;
using Agent.Contrib.Settings;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace Agent.Contrib.Face
{
    public abstract class FaceWithTrayBase : IFace
    {
        protected Font smallFont = ContribResources.GetFont(ContribResources.FontResources.small);
        public Settings.ISettings Settings { get; set; }
        protected IProvideNotifications _notificationProvider;
        protected Agent.Contrib.Drawing.Drawing drawing = new Agent.Contrib.Drawing.Drawing();
        public FaceWithTrayBase(IProvideNotifications notificationProvider, ISettings settings)
        {
            Settings = settings;
            _notificationProvider = notificationProvider;
            notificationProvider.OnNotificationReceived += notificationProvider_OnNotificationReceived;
        }

        private void notificationProvider_OnNotificationReceived(INotification notification)
        {
            if (_screen != null)
            {
                _screen.Clear();           
                Render(_screen);
                _screen.Flush();
            }            
        }

        protected Bitmap _screen = null;

        public abstract void Render(Bitmap screen);

        public int UpdateSpeed
        {
            get { return 60*1000; }
        }
    }
}
