using UnityEditor;

[InitializeOnLoad]
public class reminderHelper  {

	static reminderHelper()
     {
         EditorApplication.update += reminder;
     }
     static void reminder()
     {
         EditorApplication.update -= reminder;

		 if (!EditorPrefs.GetBool("reminded"))
		 {
			 Reminder.ShowWindow();
		 }
     }
 }


