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
        private float notificationTime = 3f;

        private static Vector2 notificationSize = new Vector2(300, 28);
        private static float verticalSpacing = 10f;

        public static NotificationManager Instance => _instance.Value;

        public sealed class Notification
        {
            public string Message;
            public float ElapsedTime;
            public Vector2 CurrentPosition;
            public bool IsSlidingOut;
            public Vector2 SlideOutTarget;

            public Notification(string message)
            {
                Message = message;
                ElapsedTime = 0f;
                CurrentPosition = new Vector2((Screen.width - notificationSize.x) / 2f, -notificationSize.y);
                IsSlidingOut = false;
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

                if (notification.ElapsedTime > notificationTime && !notification.IsSlidingOut)
                {
                    notification.IsSlidingOut = true;
                    notification.SlideOutTarget = new Vector2(notification.CurrentPosition.x, -notificationSize.y);
                }
            }

            notifications.RemoveAll(n => n.IsSlidingOut && n.CurrentPosition.y <= -notificationSize.y);
        }

        public void OnGUI()
        {
            int index = 0;
            foreach (var notification in notifications)
            {
                if (!notification.IsSlidingOut)
                {
                    Vector2 targetPosition = new Vector2((Screen.width - notificationSize.x) / 2f, 50f + ((notificationSize.y + verticalSpacing) * index));
                    notification.CurrentPosition = MathHelper.ExpLerp(notification.CurrentPosition, targetPosition, 5f, Time.deltaTime);
                }
                else
                {
                    notification.CurrentPosition = MathHelper.ExpLerp(notification.CurrentPosition, notification.SlideOutTarget, 5f, Time.deltaTime);
                }

                Rect rect = new Rect(notification.CurrentPosition, notificationSize);
                GUI.Box(rect, "<size=18>" + notification.Message + "</size>");

                if (!notification.IsSlidingOut)
                    index++;
            }
        }
    }
}