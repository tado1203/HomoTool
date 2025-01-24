using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomoTool.Helpers;
using UnityEngine;

namespace HomoTool.Managers
{
    public sealed class NotificationManager
    {
        private static readonly Lazy<NotificationManager> _instance = new Lazy<NotificationManager>(() => new NotificationManager());

        private List<Notification> notifications = new List<Notification>();
        private float notificationTime = 5f;

        private static Vector2 notificationSize = new Vector2(300, 28);
        private static float verticalSpacing = 10f;

        public static NotificationManager Instance => _instance.Value;

        public sealed class Notification
        {
            public string Message;
            public float ElapsedTime;
            public Vector2 currentPosition;

            public Notification(string message)
            {
                Message = message;
                ElapsedTime = 0f;
                currentPosition = new Vector2((Screen.width - notificationSize.x) / 2f, -notificationSize.y);
            }
        }

        public void AddNotification(string message)
        {
            notifications.Add(new Notification(message));
        }

        public void OnUpdate()
        {
            foreach (var notification in notifications)
            {
                notification.ElapsedTime += Time.deltaTime;
            }

            // Remove notifications that have exceeded their time
            notifications.RemoveAll(n => n.ElapsedTime > notificationTime);
        }

        public void OnGUI()
        {
            int index = 0;
            foreach (var notification in notifications)
            {
                Vector2 targetPosition = new Vector2((Screen.width - notificationSize.x) / 2f, 50f + ((notificationSize.y + verticalSpacing) * index));
                notification.currentPosition = MathHelper.ExpLerp(notification.currentPosition, targetPosition, 5f, Time.deltaTime);

                Rect rect = new Rect(notification.currentPosition, notificationSize);
                GUI.Box(rect, "<size=18>" + notification.Message + "</size>");

                index++;
            }
        }
    }
}