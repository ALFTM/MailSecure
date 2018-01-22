using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace MailSecure
{
    public class NotificationSystem
    {
        public void DisplayNotification(string message)
        {
            // Get a toast XML template
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText04);

            // Fill in the text elements
            XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
            stringElements[0].AppendChild(toastXml.CreateTextNode(message));

            // Specify the absolute path to an image
            String imagePath = "C://logo_white_black.png";
            XmlNodeList imageElements = toastXml.GetElementsByTagName("image");
            imageElements[0].Attributes.GetNamedItem("src").NodeValue = imagePath;
            ToastNotification toast = new ToastNotification(toastXml);

            ToastNotificationManager.CreateToastNotifier("MailSecure").Show(toast);
        }
    }
}
