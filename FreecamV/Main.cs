using System;
using System.Windows.Forms;
using GTA;

namespace FreecamV
{
	// Token: 0x02000004 RID: 4
	public class Main : Script
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000036E4 File Offset: 0x000018E4
		public Main()
		{
			Config.DefaultSpeed = base.Settings.GetValue<float>("Settings", "Speed", 4f);
			Config.ShiftSpeed = base.Settings.GetValue<float>("Settings", "BoostSpeed", 20f);
			Config.FilterIntensity = base.Settings.GetValue<float>("Settings", "FilterIntensity", 1f);
			Config.SlowMotionMultiplier = base.Settings.GetValue<float>("Settings", "SlowMotionMult", 8.5f);
			Config.Precision = base.Settings.GetValue<float>("Settings", "Precision", 1f);
			Freecam.Init();
			base.KeyDown += delegate(object sender, KeyEventArgs e)
			{
				if (e.KeyCode == base.Settings.GetValue<Keys>("Keys", "Toggle", Keys.J))
				{
					Freecam.Toggle();
				}
			};
			base.Tick += delegate(object sender, EventArgs e)
			{
				Freecam.Tick();
			};
		}
	}
}
