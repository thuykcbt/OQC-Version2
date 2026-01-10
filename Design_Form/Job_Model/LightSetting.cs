using Google.Apis.Sheets.v4.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Design_Form.Job_Model
{
	public class LightSetting
	{
		public int LightId { get; set; }
		public bool IsEnabled { get; set; } = true;
		public int Intensity { get; set; } = 200;
		public string Name { get; set; }
		
		public LightSetting(int lightId, string name)
		{
			LightId = lightId;
			Name = name;
		}

	}
	public class ShotSetting
	{
		public int ShotIndex { get; set; } // Lần chụp thứ mấy
		public List<LightSetting> Lights { get;  set; }
		int number_light = 4;
		public string Name_Shot { get; set; }
		
		public ShotSetting(string name_Shot)
		{
			Lights = new List<LightSetting>();
			LightSetting lightSetting = new LightSetting(0, "light_1");
			Lights.Add(lightSetting);
			lightSetting = new LightSetting(1, "light_2");
			Lights.Add(lightSetting);
			lightSetting = new LightSetting(2, "light_3");
			Lights.Add(lightSetting);
			lightSetting = new LightSetting(3, "light_4");
			Lights.Add(lightSetting);
			Name_Shot = name_Shot;

		}
		public void AddOrUpdateLight(int lightId, bool enable, int intensity)
		{
			var light = Lights[lightId];
			light.IsEnabled = enable;
			if (!enable)
				light.Intensity = 0;
			else light.Intensity = intensity;
		}
		//public static ShotSetting CreateDefault(string name, int shotIndex)
		//{
		//	var shot = new ShotSetting
		//	{
		//		ShotIndex = shotIndex,
		//		Name_Shot = name
		//	};

		//	for (int i = 0; i < 4; i++)
		//		shot.Lights.Add(new LightSetting(i, $"light_{i + 1}"));

		//	return shot;
		//}
	}
	public class ViewCaptureSetting
	{
		public string ViewName { get; set; }
		[JsonIgnore]
		public int ShotCount => Shots.Count;

		public List<ShotSetting> Shots { get;  set; }
		
		public ViewCaptureSetting()
		{
			Shots = new List<ShotSetting>();
			ShotSetting shotSetting = new ShotSetting("Nomarl_Light");
			Shots.Add(shotSetting);
			shotSetting = new ShotSetting("Low_Light");
			Shots.Add(shotSetting);
		}

		




	}
}
