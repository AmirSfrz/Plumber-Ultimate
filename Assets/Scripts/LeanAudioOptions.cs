/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;

public class LeanAudioOptions
{
	public enum LeanAudioWaveStyle
	{
		Sine,
		Square,
		Sawtooth,
		Noise
	}

	public LeanAudioWaveStyle waveStyle;

	public Vector3[] vibrato;

	public Vector3[] modulation;

	public int frequencyRate = 44100;

	public float waveNoiseScale = 1000f;

	public float waveNoiseInfluence = 1f;

	public bool useSetData = true;

	public LeanAudioStream stream;

	public LeanAudioOptions setFrequency(int frequencyRate)
	{
		this.frequencyRate = frequencyRate;
		return this;
	}

	public LeanAudioOptions setVibrato(Vector3[] vibrato)
	{
		this.vibrato = vibrato;
		return this;
	}

	public LeanAudioOptions setWaveSine()
	{
		waveStyle = LeanAudioWaveStyle.Sine;
		return this;
	}

	public LeanAudioOptions setWaveSquare()
	{
		waveStyle = LeanAudioWaveStyle.Square;
		return this;
	}

	public LeanAudioOptions setWaveSawtooth()
	{
		waveStyle = LeanAudioWaveStyle.Sawtooth;
		return this;
	}

	public LeanAudioOptions setWaveNoise()
	{
		waveStyle = LeanAudioWaveStyle.Noise;
		return this;
	}

	public LeanAudioOptions setWaveStyle(LeanAudioWaveStyle style)
	{
		waveStyle = style;
		return this;
	}

	public LeanAudioOptions setWaveNoiseScale(float waveScale)
	{
		waveNoiseScale = waveScale;
		return this;
	}

	public LeanAudioOptions setWaveNoiseInfluence(float influence)
	{
		waveNoiseInfluence = influence;
		return this;
	}
}
