using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoTasks.Pubnub
{
    public class NotificationCreator
    {
        private PubnubAPI pubnub;
        private static NotificationCreator creator;
        private string channel;

        private NotificationCreator()
        {
            this.pubnub = new PubnubAPI(
                "pub-c-64796d9d-5e70-4409-b54d-44a0351fee2e",               // PUBLISH_KEY
                "sub-c-b55bc4f6-3d9c-11e4-8e82-02ee2ddab7fe",               // SUBSCRIBE_KEY
                "sec-c-ZDZlZGFhYmUtYjdmOS00MDQ3LWFiYjYtNTYyY2Y1YmRkZmJl",   // SECRET_KEY
			    true                                                        // SSL_ON?
		    );

            this.channel = "TodoNotification";
        }

        public static NotificationCreator Instance
        {
            get
            {
                if (creator == null)
                {
                    creator = new NotificationCreator();
                }

                return creator;
            }
        }

        public void AddTaskNotification(string content, DateTime added)
        {
            var message = String.Format("New task {0} added on {1}", content, added);
            pubnub.Publish(channel, message);
        }

        public void ChangeTaskNotification(string content, string newContent)
        {
            var message = String.Format("Task {0} was changed to {1}", content, newContent);
            pubnub.Publish(channel, message);
        }

        public void DeleteTaskNotification(string content)
        {
            var message = String.Format("Task {0} was deleted", content);
            pubnub.Publish(channel, message);
        }
    }
}
