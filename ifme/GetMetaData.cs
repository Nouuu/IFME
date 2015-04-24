﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
// Asset
using MediaInfoDotNet;
using IniParser.Model;
using IniParser;

namespace ifme
{
	class GetMetaData
	{
		public static string[] MediaData(string file)
		{
			string[] GFI = new string[7];

			if (file.Contains(".avs"))
				if (!Addons.Installed.AviSynth)
					return GFI;

			bool IsAVS = file.Contains(".avs");
			bool IsIFS = file.Contains(".ifs");
			
			MediaFile AviFile = new MediaFile(IsAVS ? AviSynthReader(file) : IsIFS ? IfsReader(file) : file);

			if (AviFile.Video.Count == 0)
				if (AviFile.Image.Count == 0)
					return GFI;

			GFI[0] = Path.GetFileNameWithoutExtension(file);
			GFI[1] = Path.GetExtension(file).ToLower();

			try
			{
				// Images
				if (AviFile.Image.Count > 0)
				{
					var i = AviFile.Image[0];

					GFI[2] = i.format;
					GFI[3] = String.Format("{0}x{1}", i.width, i.height);
					GFI[4] = "-";
					GFI[5] = i.bitDepth.ToString();
					GFI[6] = file;

					return GFI;
				}

				// Video
				var v = AviFile.Video[0];
				var st = v.scanType;
				var fm = v.frameRateMode;

				// Scan Type
				if (st == "" || st == null)
					st = "p";
				else
					st = v.scanType.Remove(1).ToLower();

				// Frame Rate Mode
				if (fm == "" || fm == null)
					fm = null;
				else
					fm = "[" + fm + "] ";

				GFI[2] = v.format;
				GFI[3] = v.width.ToString() + "x" + v.height.ToString() + st;
				GFI[4] = (v.frameRate == 0 ? "-" : fm + v.frameRate.ToString() + " (" + v.frameCount.ToString() + " frames)");
				GFI[5] = (v.bitDepth == 0 ? "-" : v.bitDepth.ToString() + " bits");
				GFI[6] = file;

				return GFI;
			}
			catch (Exception ex)
			{
				GFI[2] = ex.Message;
				return GFI;
			}
		}

		public static string IfsReader(string file)
		{
			var parser = new FileIniDataParser();
			IniData data = parser.ReadFile(file);
			return data["script"]["samp"];
		}

		public static int AvsGetFrame(string file)
		{
			string test = "";
			string[] result = AvsPrintInfo(file);

			foreach (var item in result)
			{
				if (item.Contains("frames"))
				{
					foreach (var thing in item)
					{
						int x;
						if (int.TryParse(thing.ToString(), out x))
						{
							test += x.ToString();
						}
					}
				}
			}

			return int.Parse(test);
		}

		static string[] AvsPrintInfo(string file)
		{
			string dir = Properties.Settings.Default.TemporaryFolder;
			string avs = Addons.BuildIn.AVI2PIPE;
			string pathmap = Path.Combine(dir, "fubuki.if");

			Process P = new Process();
			var SI = P.StartInfo;

			SI.FileName = OS.IsWindows ? "cmd" : "bash";
			SI.Arguments = String.Format((OS.IsWindows ? "/c START \"FUBUKI\" /WAIT /B \"{0}\" info \"{1}\" > \"{2}\"" : "-c '\"{0}\" info \"{1}\" > \"{2}\"'"), avs, file, pathmap);
			SI.WorkingDirectory = dir;
			SI.CreateNoWindow = true;
			SI.UseShellExecute = false;

			P.Start();
			P.WaitForExit();
			P.Close();

			while (true)
			{
				try
				{
					return File.ReadAllLines(pathmap);
				}
				catch
				{
					// Keep trying to read file until process release
				}
			}
		}

		static string[] AvsFilter = { "DirectShowSource", "FFVideoSource", "ImageSource" };
		public static string AviSynthReader(string file)
		{
			if (String.Equals(Path.GetExtension(file), ".avs", StringComparison.InvariantCultureIgnoreCase))
			{
				foreach (var item in System.IO.File.ReadAllLines(file))
				{
					foreach (var af in AvsFilter)
					{
						if (item.Contains(af))
						{
							for (int i = 0; i < item.Length; i++)
							{
								if (item[i] == '(' && item[i + 1] == '"')
								{
									i += 2;
									file = "";
									while (i < item.Length)
									{
										if (item[i] == '"' && (item[i + 1] == ',' || item[i + 1] == ')'))
										{
											break;
										}
										else
										{
											file += item[i];
										}
										i++;
									}
								}
							}
						}
					}
				}
				return file.Contains('%') ? ParsePlaceholder(file) : file;
			}
			return file;
		}

		static string ParsePlaceholder(string file)
		{
			var input = file; //"img_sequence_%06d.tiff";
			int location = input.IndexOf('%');
			var placeholder = "";
			var placenumber = "";

			for (int i = location; i < input.Length; i++)
			{
				if (input[i] == '%')
					placeholder += input[i];

				if (input[i] == 'd')
					placeholder += input[i];

				int temp;
				if (int.TryParse(input[i].ToString(), out temp))
				{
					placeholder += input[i];
					placenumber += input[i];
				}
			}

			if (String.IsNullOrEmpty(placenumber))
				placenumber = "01";

			int num;
			var lop = "";
			if (int.TryParse(placenumber, out num))
			{
				for (int i = 0; i < num; i++)
				{
					lop += "0";
				}
			}

			var gg = input.Replace(placeholder, lop);

			return gg;
		}

		public static string[] SubtitleData(string file)
		{
			string[] SB = new string[4];

			SB[0] = Path.GetFileNameWithoutExtension(file);
			SB[1] = Path.GetExtension(file);

			foreach (var item in File.ReadAllLines(Globals.Files.ISO))
			{
				if (SB[0].Length > 3)
				{
					if (SB[0].Substring(SB[0].Length - 3) == item.Substring(0, 3))
					{
						SB[2] = item;
						break;
					}
					else
					{
						SB[2] = "und (Undetermined)";
					}
				}
			}

			SB[3] = file;
			return SB;
		}

		// This block detect subtitle file, due performance issue detect via file extension,
		// there will consume process to check file is binary or plain text
		public static bool SubtitleValid(string file)
		{
			var ext = Path.GetExtension(file);

			if (ext == ".ass")
				return true;

			if (ext == ".ssa")
				return true;

			if (ext == ".srt")
				return true;

			return false;
		}

		public static string[] AttachmentData(string file)
		{
			string[] AD = new string[4];

			AD[0] = Path.GetFileNameWithoutExtension(file);
			AD[1] = Path.GetExtension(file);

			if (AD[1] == ".ttf")
			{
				AD[2] = "application/x-truetype-font";
			}
			else if (AD[1] == ".otf")
			{
				AD[2] = "application/vnd.ms-opentype";
			}
			else if (AD[1] == ".woff")
			{
				AD[2] = "application/font-woff";
			}
			else
			{
				AD[2] = "application/octet-stream";
			}

			AD[3] = file;
			return AD;
		}

		// This block will detect font magic number, much more better then detect file extension
		// useful for binary file
		public static bool AttachmentValid(string file)
		{
			FileInfo f = new FileInfo(file);
			if (f.Length >= 1073741824)							// Detect 1GiB file enough, no font that large
				return false;

			byte[] data = System.IO.File.ReadAllBytes(file);
			byte[] MagicTTF = { 0x00, 0x01, 0x00, 0x00, 0x00 };
			byte[] MagicOTF = { 0x4F, 0x54, 0x54, 0x4F, 0x00 };
			byte[] MagicWOFF = { 0x77, 0x4F, 0x46, 0x46, 0x00 };
			byte[] check = new byte[5];

			Buffer.BlockCopy(data, 0, check, 0, 5);

			if (MagicTTF.SequenceEqual(check))
				return true;

			if (MagicOTF.SequenceEqual(check))
				return true;

			if (MagicWOFF.SequenceEqual(check))
				return true;

			return false;
		}
	}
}
