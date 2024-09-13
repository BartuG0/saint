using System;
using System.Collections.Generic;

namespace FreecamV
{
	// Token: 0x02000002 RID: 2
	internal class Config
	{
		// Token: 0x04000001 RID: 1
		public static float DefaultSpeed = 5f;

		// Token: 0x04000002 RID: 2
		public static float ShiftSpeed = 20f;

		// Token: 0x04000003 RID: 3
		public static float Precision = 1f;

		// Token: 0x04000004 RID: 4
		public static float FilterIntensity = 1f;

		// Token: 0x04000005 RID: 5
		public static float SlowMotionMultiplier = 5.5f;

		// Token: 0x04000006 RID: 6
		public static List<string> Filters = new List<string>
		{
			"None",
			"NG_filmic01",
			"NG_filmic02",
			"NG_filmic03",
			"NG_filmic04",
			"NG_filmic05",
			"NG_filmic06",
			"NG_filmic07",
			"NG_filmic08",
			"NG_filmic09",
			"NG_filmic10",
			"NG_filmic11",
			"NG_filmic12",
			"NG_filmic13",
			"phone_cam",
			"phone_cam1",
			"phone_cam2",
			"phone_cam3",
			"phone_cam4",
			"phone_cam5",
			"phone_cam6",
			"phone_cam7",
			"phone_cam8",
			"phone_cam9",
			"phone_cam10",
			"phone_cam11",
			"phone_cam12"
		};
	}
}
