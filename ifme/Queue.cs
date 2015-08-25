﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

enum QueueProp
{
	FormatMkv,
	FormatMp4,
	PictureResolution,
	PictureFrameRate,
	PictureBitDepth,
	PictureChroma,
	PictureYadifEnable,
	PictureYadifMode,
	PictureYadifField,
	PictureYadifFlag,
	VideoPreset,
	VideoTune,
	VideoType,
	VideoValue,
	VideoCommand,
	AudioEncoder,
	AudioBitRate,
	AudioFreq,
	AudioChannel,
	AudioMerge,
	AudioCommand,
}

namespace ifme
{
	public class Queue
	{
		public bool IsEnable;
		public Data Data = new Data();
		public Prop Prop = new Prop();

		public Picture Picture = new Picture();
		public Video Video = new Video();
		public Audio Audio = new Audio();

		public bool SubtitleEnable;
		public List<Subtitle> Subtitle = new List<Subtitle>();

		public bool AttachEnable;
		public List<Attachment> Attach = new List<Attachment>();
	}

	public class Data
	{
		public string File;

		public bool IsFileMkv;
		public bool IsFileAvs;

		public bool SaveAsMkv;
	}

	public class Prop
	{
		public bool IsVFR;
		public int Duration; // in ms
		public int FrameCount;
	}

	public class Picture
	{
		public string Resolution;
		public string FrameRate;
		public string BitDepth;
		public string Chroma;

		public bool YadifEnable;
		public int YadifMode;
		public int YadifField;
		public int YadifFlag;
	}

	public class Video
	{
		public string Preset;
		public string Tune;
		public int Type;
		public string Value;
		public string Command;
	}

	public class Audio
	{
		public string Encoder;
		public string BitRate;
		public string Frequency;
		public string Channel;
		public bool Merge;
		public string Command;
	}

	public class Subtitle
	{
		public string File;
		public string Lang;
	}

	public class Attachment
	{
		public string File;
		public string MIME;
		public string Comment = "No";
	}
}
