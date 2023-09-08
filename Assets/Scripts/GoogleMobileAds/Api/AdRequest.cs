/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using GoogleMobileAds.Api.Mediation;
using System;
using System.Collections.Generic;

namespace GoogleMobileAds.Api
{
	public class AdRequest
	{
		public class Builder
		{
			internal List<string> TestDevices
			{
				get;
				private set;
			}

			internal HashSet<string> Keywords
			{
				get;
				private set;
			}

			internal DateTime? Birthday
			{
				get;
				private set;
			}

			internal Gender? Gender
			{
				get;
				private set;
			}

			internal bool? ChildDirectedTreatmentTag
			{
				get;
				private set;
			}

			internal Dictionary<string, string> Extras
			{
				get;
				private set;
			}

			internal List<MediationExtras> MediationExtras
			{
				get;
				private set;
			}

			public Builder()
			{
				TestDevices = new List<string>();
				Keywords = new HashSet<string>();
				Birthday = null;
				Gender = null;
				ChildDirectedTreatmentTag = null;
				Extras = new Dictionary<string, string>();
				MediationExtras = new List<MediationExtras>();
			}

			public Builder AddKeyword(string keyword)
			{
				Keywords.Add(keyword);
				return this;
			}

			public Builder AddTestDevice(string deviceId)
			{
				TestDevices.Add(deviceId);
				return this;
			}

			public AdRequest Build()
			{
				return new AdRequest(this);
			}

			public Builder SetBirthday(DateTime birthday)
			{
				Birthday = birthday;
				return this;
			}

			public Builder SetGender(Gender gender)
			{
				Gender = gender;
				return this;
			}

			public Builder AddMediationExtras(MediationExtras extras)
			{
				MediationExtras.Add(extras);
				return this;
			}

			public Builder TagForChildDirectedTreatment(bool tagForChildDirectedTreatment)
			{
				ChildDirectedTreatmentTag = tagForChildDirectedTreatment;
				return this;
			}

			public Builder AddExtra(string key, string value)
			{
				Extras.Add(key, value);
				return this;
			}
		}

		public const string Version = "3.13.1";

		public const string TestDeviceSimulator = "SIMULATOR";

		public List<string> TestDevices
		{
			get;
			private set;
		}

		public HashSet<string> Keywords
		{
			get;
			private set;
		}

		public DateTime? Birthday
		{
			get;
			private set;
		}

		public Gender? Gender
		{
			get;
			private set;
		}

		public bool? TagForChildDirectedTreatment
		{
			get;
			private set;
		}

		public Dictionary<string, string> Extras
		{
			get;
			private set;
		}

		public List<MediationExtras> MediationExtras
		{
			get;
			private set;
		}

		private AdRequest(Builder builder)
		{
			TestDevices = new List<string>(builder.TestDevices);
			Keywords = new HashSet<string>(builder.Keywords);
			Birthday = builder.Birthday;
			Gender = builder.Gender;
			TagForChildDirectedTreatment = builder.ChildDirectedTreatmentTag;
			Extras = new Dictionary<string, string>(builder.Extras);
			MediationExtras = builder.MediationExtras;
		}
	}
}
