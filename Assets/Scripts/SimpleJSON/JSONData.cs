/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System.IO;

namespace SimpleJSON
{
	public class JSONData : JSONNode
	{
		private string m_Data;

		public override string Value
		{
			get
			{
				return m_Data;
			}
			set
			{
				m_Data = value;
			}
		}

		public JSONData(string aData)
		{
			m_Data = aData;
		}

		public JSONData(float aData)
		{
			AsFloat = aData;
		}

		public JSONData(double aData)
		{
			AsDouble = aData;
		}

		public JSONData(bool aData)
		{
			AsBool = aData;
		}

		public JSONData(int aData)
		{
			AsInt = aData;
		}

		public override string ToString()
		{
			return "\"" + JSONNode.Escape(m_Data) + "\"";
		}

		public override string ToString(string aPrefix)
		{
			return "\"" + JSONNode.Escape(m_Data) + "\"";
		}

		public override void Serialize(BinaryWriter aWriter)
		{
			JSONData jSONData = new JSONData(string.Empty);
			jSONData.AsInt = AsInt;
			if (jSONData.m_Data == m_Data)
			{
				aWriter.Write((byte)4);
				aWriter.Write(AsInt);
				return;
			}
			jSONData.AsFloat = AsFloat;
			if (jSONData.m_Data == m_Data)
			{
				aWriter.Write((byte)7);
				aWriter.Write(AsFloat);
				return;
			}
			jSONData.AsDouble = AsDouble;
			if (jSONData.m_Data == m_Data)
			{
				aWriter.Write((byte)5);
				aWriter.Write(AsDouble);
				return;
			}
			jSONData.AsBool = AsBool;
			if (jSONData.m_Data == m_Data)
			{
				aWriter.Write((byte)6);
				aWriter.Write(AsBool);
			}
			else
			{
				aWriter.Write((byte)3);
				aWriter.Write(m_Data);
			}
		}
	}
}
