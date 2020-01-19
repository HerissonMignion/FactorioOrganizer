using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioOrganizer
{

	/*
	 * there are sometimes messages that always appear when doing some specific things. as a user, you have seen this message so much that you remember what it is and position your
	 * mouse to fast click the ok button so it disappear fast. these message are very annoying and this class is here to remember which message has been shown to the user
	 * and will not show again a previous message.
	 * 
	 */

	public static class MsgOnce
	{

		

		private static List<string> listIDs = new List<string>(); //the list of all msg ID that has been shown.

		//this is the void to call to pop a message box.
		//msgID is a unique string message identifier. this void will store this id in the list above and will not show the message if it has already been added to the list above.
		//msg is the actual message to show.
		public static void Show(string msgID, string msg)
		{
			bool AlreadyShown = false;
			//search if the message id was already been added into the list
			foreach (string actualid in MsgOnce.listIDs)
			{
				if (actualid == msgID)
				{
					AlreadyShown = true;
					break;
				}
			}
			//if the message hasn't already been shown, we pop it
			if (!AlreadyShown)
			{
				//add the id
				MsgOnce.listIDs.Add(msgID);

				//show the message
				System.Windows.Forms.MessageBox.Show(msg);
			}
		}



	}
}
