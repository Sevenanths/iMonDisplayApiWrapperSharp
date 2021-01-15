// Decompiled with JetBrains decompiler
// Type: iMon.DisplayApi.iMonLcdIconsControl
// Assembly: iMonDisplayApiWrapperSharp, Version=0.1.0.7, Culture=neutral, PublicKeyToken=null
// MVID: A8FF370F-E095-466E-8933-1750BC929339
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\iMonDisplayApiWrapperSharp.dll

using System;
using System.Collections.Generic;

namespace iMon.DisplayApi
{
  public class iMonLcdIconsControl
  {
    private static Dictionary<iMonLcdIcons, byte> iconMasks = new Dictionary<iMonLcdIcons, byte>(Enum.GetValues(typeof (iMonLcdIcons)).Length);
    private iMonWrapperApi wrapper;
    private Dictionary<iMonLcdIcons, bool> icons;

    public bool this[iMonLcdIcons icon]
    {
      get
      {
        if (!this.wrapper.IsInitialized)
          return false;
        return this.icons[icon];
      }
      set
      {
        if (!this.wrapper.IsInitialized)
        {
          this.wrapper.OnLogError("Cannot set icon when the display has not been initialized");
          throw new InvalidOperationException("The display is not initialized");
        }
        this.Set(icon, value);
      }
    }

    static iMonLcdIconsControl()
    {
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.DiscTopCenter, (byte) 128);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.DiscTopLeft, (byte) 64);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.DiscMiddleLeft, (byte) 32);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.DiscBottomLeft, (byte) 16);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.DiscBottomCenter, (byte) 8);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.DiscBottomRight, (byte) 4);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.DiscMiddleRight, (byte) 2);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.DiscTopRight, (byte) 1);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.DiscCircle, (byte) 128);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.Music, (byte) 128);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.Movie, (byte) 64);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.Photo, (byte) 32);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.CdDvd, (byte) 16);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.Tv, (byte) 8);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.Webcast, (byte) 4);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.NewsWeather, (byte) 2);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.SpeakerFrontLeft, (byte) 128);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.SpeakerCenter, (byte) 64);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.SpeakerFrontRight, (byte) 32);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.SpeakerSideLeft, (byte) 16);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.SpeakerLFE, (byte) 8);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.SpeakerSideRight, (byte) 4);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.SpeakerRearLeft, (byte) 2);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.SpeakerSPDIF, (byte) 1);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.SpeakerRearRight, (byte) 128);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.VideoMPG, (byte) 128);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.VideoDivX, (byte) 64);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.VideoXviD, (byte) 32);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.VideoWMV, (byte) 16);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.VideoMPGAudio, (byte) 8);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.VideoAC3, (byte) 4);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.VideoDTS, (byte) 2);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.VideoWMA, (byte) 1);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.AudioMP3, (byte) 128);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.AudioOGG, (byte) 64);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.AudioWMA, (byte) 32);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.AudioWAV, (byte) 16);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.AspectRatioSource, (byte) 128);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.AspectRatioFIT, (byte) 64);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.AspectRatioTv, (byte) 32);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.AspectRatioHDTV, (byte) 16);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.AspectRatioScreen1, (byte) 8);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.AspectRatioScreen2, (byte) 4);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.Repeat, (byte) 128);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.Shuffle, (byte) 64);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.Alarm, (byte) 32);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.Recording, (byte) 16);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.Volume, (byte) 8);
      iMonLcdIconsControl.iconMasks.Add(iMonLcdIcons.Time, (byte) 4);
    }

    internal iMonLcdIconsControl(iMonWrapperApi wrapper)
    {
      if (wrapper == null)
        throw new ArgumentNullException(nameof (wrapper));
      this.wrapper = wrapper;
      this.icons = new Dictionary<iMonLcdIcons, bool>(Enum.GetValues(typeof (iMonLcdIcons)).Length);
      foreach (iMonLcdIcons key in Enum.GetValues(typeof (iMonLcdIcons)))
        this.icons.Add(key, false);
    }

    public bool ShowAll()
    {
      return this.SetAll(true);
    }

    public bool HideAll()
    {
      return this.SetAll(false);
    }

    public bool SetAll(bool show)
    {
      if (!this.wrapper.IsInitialized)
      {
        this.wrapper.OnLogError("LCD.Icons.SetAll(): Not initialized");
        throw new InvalidOperationException("The display is not initialized");
      }
      this.wrapper.OnLog("IMON_Display_SetLcdAllIcons(" + (object) show + ")");
      switch (iMonNativeApi.IMON_Display_SetLcdAllIcons(show))
      {
        case iMonNativeApi.iMonDisplayResult.Succeeded:
          iMonLcdIcons[] array = new iMonLcdIcons[this.icons.Count];
          this.icons.Keys.CopyTo(array, 0);
          foreach (iMonLcdIcons index in array)
            this.icons[index] = show;
          return true;
        case iMonNativeApi.iMonDisplayResult.NotInitialized:
          this.wrapper.OnLogError("IMON_Display_SetLcdAllIcons() => not initialized");
          throw new InvalidOperationException("The display is not initialized");
        default:
          return false;
      }
    }

    public bool Show(iMonLcdIcons icon)
    {
      return this.Set(icon, true);
    }

    public bool Show(IEnumerable<iMonLcdIcons> iconList)
    {
      return this.Set(iconList, true);
    }

    public bool Hide(iMonLcdIcons icon)
    {
      return this.Set(icon, false);
    }

    public bool Hide(IEnumerable<iMonLcdIcons> iconList)
    {
      return this.Set(iconList, false);
    }

    public bool Set(iMonLcdIcons icon, bool show)
    {
      if (!this.wrapper.IsInitialized)
      {
        this.wrapper.OnLogError("LCD.Icons.Set(): Not initialized");
        throw new InvalidOperationException("The display is not initialized");
      }
      if (this.icons[icon] == show)
        return true;
      switch (this.set(icon, show))
      {
        case iMonNativeApi.iMonDisplayResult.Succeeded:
          this.icons[icon] = show;
          return true;
        case iMonNativeApi.iMonDisplayResult.NotInitialized:
          this.wrapper.OnLogError("Cannot set icon when the display has not been initialized");
          throw new InvalidOperationException("The display is not initialized");
        default:
          return false;
      }
    }

    public bool Set(IEnumerable<iMonLcdIcons> iconList, bool show)
    {
      bool flag = true;
      foreach (iMonLcdIcons icon in iconList)
      {
        if (!this.Set(icon, show))
          flag = false;
      }
      return flag;
    }

    internal void Reset()
    {
      this.wrapper.OnLog("LCD.Icons.Reset()");
      if (this.wrapper.IsInitialized)
      {
        this.SetAll(false);
      }
      else
      {
        iMonLcdIcons[] array = new iMonLcdIcons[this.icons.Count];
        this.icons.Keys.CopyTo(array, 0);
        foreach (iMonLcdIcons index in array)
          this.icons[index] = false;
      }
    }

    private iMonNativeApi.iMonDisplayResult set(iMonLcdIcons icon, bool show)
    {
      byte[] numArray = new byte[2];
      switch (icon)
      {
        case iMonLcdIcons.SpeakerFrontLeft:
        case iMonLcdIcons.SpeakerRearRight:
        case iMonLcdIcons.SpeakerSPDIF:
        case iMonLcdIcons.SpeakerRearLeft:
        case iMonLcdIcons.SpeakerSideRight:
        case iMonLcdIcons.SpeakerLFE:
        case iMonLcdIcons.SpeakerSideLeft:
        case iMonLcdIcons.SpeakerFrontRight:
        case iMonLcdIcons.SpeakerCenter:
          if (this.icons[iMonLcdIcons.SpeakerCenter])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.SpeakerCenter];
          if (this.icons[iMonLcdIcons.SpeakerFrontLeft])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.SpeakerFrontLeft];
          if (this.icons[iMonLcdIcons.SpeakerFrontRight])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.SpeakerFrontRight];
          if (this.icons[iMonLcdIcons.SpeakerLFE])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.SpeakerLFE];
          if (this.icons[iMonLcdIcons.SpeakerRearLeft])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.SpeakerRearLeft];
          if (this.icons[iMonLcdIcons.SpeakerRearRight])
            numArray[1] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.SpeakerRearRight];
          if (this.icons[iMonLcdIcons.SpeakerSideLeft])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.SpeakerSideLeft];
          if (this.icons[iMonLcdIcons.SpeakerSideRight])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.SpeakerSideRight];
          if (this.icons[iMonLcdIcons.SpeakerSPDIF])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.SpeakerSPDIF];
          int index1 = 0;
          if (icon == iMonLcdIcons.SpeakerRearRight)
            index1 = 1;
          if (show)
            numArray[index1] |= iMonLcdIconsControl.iconMasks[icon];
          else
            numArray[index1] &= (byte)~iMonLcdIconsControl.iconMasks[icon];
          this.wrapper.OnLog("IMON_Display_SetLcdSpeakerIcon(" + Convert.ToString(numArray[0], 2) + ", " + Convert.ToString(numArray[0], 2) + ")");
          return iMonNativeApi.IMON_Display_SetLcdSpeakerIcon(numArray[0], numArray[1]);
        case iMonLcdIcons.NewsWeather:
        case iMonLcdIcons.Webcast:
        case iMonLcdIcons.Tv:
        case iMonLcdIcons.CdDvd:
        case iMonLcdIcons.Photo:
        case iMonLcdIcons.Movie:
        case iMonLcdIcons.Music:
          if (this.icons[iMonLcdIcons.Music])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.Music];
          if (this.icons[iMonLcdIcons.Movie])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.Movie];
          if (this.icons[iMonLcdIcons.Photo])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.Photo];
          if (this.icons[iMonLcdIcons.CdDvd])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.CdDvd];
          if (this.icons[iMonLcdIcons.Tv])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.Tv];
          if (this.icons[iMonLcdIcons.Webcast])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.Webcast];
          if (this.icons[iMonLcdIcons.NewsWeather])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.NewsWeather];
          if (show)
            numArray[0] |= iMonLcdIconsControl.iconMasks[icon];
          else
            numArray[0] &= (byte)~iMonLcdIconsControl.iconMasks[icon];
          this.wrapper.OnLog("IMON_Display_SetLcdMediaTypeIcon(" + Convert.ToString(numArray[0], 2) + ")");
          return iMonNativeApi.IMON_Display_SetLcdMediaTypeIcon(numArray[0]);
        case iMonLcdIcons.AspectRatioScreen2:
        case iMonLcdIcons.AspectRatioScreen1:
        case iMonLcdIcons.AspectRatioHDTV:
        case iMonLcdIcons.AspectRatioTv:
        case iMonLcdIcons.AspectRatioFIT:
        case iMonLcdIcons.AspectRatioSource:
          if (this.icons[iMonLcdIcons.AspectRatioSource])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.AspectRatioSource];
          if (this.icons[iMonLcdIcons.AspectRatioFIT])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.AspectRatioFIT];
          if (this.icons[iMonLcdIcons.AspectRatioTv])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.AspectRatioTv];
          if (this.icons[iMonLcdIcons.AspectRatioHDTV])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.AspectRatioHDTV];
          if (this.icons[iMonLcdIcons.AspectRatioScreen1])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.AspectRatioScreen1];
          if (this.icons[iMonLcdIcons.AspectRatioScreen2])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.AspectRatioScreen2];
          if (show)
            numArray[0] |= iMonLcdIconsControl.iconMasks[icon];
          else
            numArray[0] &= (byte)~iMonLcdIconsControl.iconMasks[icon];
          this.wrapper.OnLog("IMON_Display_SetLcdAspectRatioIcon(" + Convert.ToString(numArray[0], 2) + ")");
          return iMonNativeApi.IMON_Display_SetLcdAspectRatioIcon(numArray[0]);
        case iMonLcdIcons.VideoDTS:
        case iMonLcdIcons.VideoAC3:
        case iMonLcdIcons.VideoMPGAudio:
        case iMonLcdIcons.VideoWMV:
        case iMonLcdIcons.VideoXviD:
        case iMonLcdIcons.VideoWMA:
        case iMonLcdIcons.VideoDivX:
        case iMonLcdIcons.VideoMPG:
          if (this.icons[iMonLcdIcons.VideoMPG])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.VideoMPG];
          if (this.icons[iMonLcdIcons.VideoDivX])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.VideoDivX];
          if (this.icons[iMonLcdIcons.VideoXviD])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.VideoXviD];
          if (this.icons[iMonLcdIcons.VideoWMV])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.VideoWMV];
          if (this.icons[iMonLcdIcons.VideoMPGAudio])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.VideoMPGAudio];
          if (this.icons[iMonLcdIcons.VideoAC3])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.VideoAC3];
          if (this.icons[iMonLcdIcons.VideoDTS])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.VideoDTS];
          if (this.icons[iMonLcdIcons.VideoWMA])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.VideoWMA];
          if (show)
            numArray[0] |= iMonLcdIconsControl.iconMasks[icon];
          else
            numArray[0] &= (byte)~iMonLcdIconsControl.iconMasks[icon];
          this.wrapper.OnLog("IMON_Display_SetLcdVideoCodecIcon(" + Convert.ToString(numArray[0], 2) + ")");
          return iMonNativeApi.IMON_Display_SetLcdVideoCodecIcon(numArray[0]);
        case iMonLcdIcons.AudioOGG:
        case iMonLcdIcons.AudioMP3:
        case iMonLcdIcons.AudioWMA:
        case iMonLcdIcons.AudioWAV:
          if (this.icons[iMonLcdIcons.AudioMP3])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.AudioMP3];
          if (this.icons[iMonLcdIcons.AudioOGG])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.AudioOGG];
          if (this.icons[iMonLcdIcons.AudioWMA])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.AudioWMA];
          if (this.icons[iMonLcdIcons.AudioWAV])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.AudioWAV];
          if (show)
            numArray[0] |= iMonLcdIconsControl.iconMasks[icon];
          else
            numArray[0] &= (byte)~iMonLcdIconsControl.iconMasks[icon];
          this.wrapper.OnLog("IMON_Display_SetLcdAudioCodecIcon(" + Convert.ToString(numArray[0], 2) + ")");
          return iMonNativeApi.IMON_Display_SetLcdAudioCodecIcon(numArray[0]);
        case iMonLcdIcons.Time:
        case iMonLcdIcons.Volume:
        case iMonLcdIcons.Recording:
        case iMonLcdIcons.Alarm:
        case iMonLcdIcons.Shuffle:
        case iMonLcdIcons.Repeat:
          if (this.icons[iMonLcdIcons.Repeat])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.Repeat];
          if (this.icons[iMonLcdIcons.Shuffle])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.Shuffle];
          if (this.icons[iMonLcdIcons.Alarm])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.Alarm];
          if (this.icons[iMonLcdIcons.Recording])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.Recording];
          if (this.icons[iMonLcdIcons.Volume])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.Volume];
          if (this.icons[iMonLcdIcons.Time])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.Time];
          if (show)
            numArray[0] |= iMonLcdIconsControl.iconMasks[icon];
          else
            numArray[0] &= (byte)~iMonLcdIconsControl.iconMasks[icon];
          this.wrapper.OnLog("IMON_Display_SetLcdEtcIcon(" + Convert.ToString(numArray[0], 2) + ")");
          return iMonNativeApi.IMON_Display_SetLcdEtcIcon(numArray[0]);
        case iMonLcdIcons.DiscTopRight:
        case iMonLcdIcons.DiscMiddleRight:
        case iMonLcdIcons.DiscBottomRight:
        case iMonLcdIcons.DiscBottomCenter:
        case iMonLcdIcons.DiscBottomLeft:
        case iMonLcdIcons.DiscMiddleLeft:
        case iMonLcdIcons.DiscTopLeft:
        case iMonLcdIcons.DiscTopCenter:
        case iMonLcdIcons.DiscCircle:
          if (this.icons[iMonLcdIcons.DiscTopCenter])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.DiscTopCenter];
          if (this.icons[iMonLcdIcons.DiscTopLeft])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.DiscTopLeft];
          if (this.icons[iMonLcdIcons.DiscMiddleLeft])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.DiscMiddleLeft];
          if (this.icons[iMonLcdIcons.DiscBottomLeft])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.DiscBottomLeft];
          if (this.icons[iMonLcdIcons.DiscBottomCenter])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.DiscBottomCenter];
          if (this.icons[iMonLcdIcons.DiscBottomRight])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.DiscBottomRight];
          if (this.icons[iMonLcdIcons.DiscMiddleRight])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.DiscMiddleRight];
          if (this.icons[iMonLcdIcons.DiscTopRight])
            numArray[0] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.DiscTopRight];
          if (this.icons[iMonLcdIcons.DiscCircle])
            numArray[1] |= iMonLcdIconsControl.iconMasks[iMonLcdIcons.DiscCircle];
          int index2 = 0;
          if (icon == iMonLcdIcons.DiscCircle)
            index2 = 1;
          if (show)
            numArray[index2] |= iMonLcdIconsControl.iconMasks[icon];
          else
            numArray[index2] &= (byte)~iMonLcdIconsControl.iconMasks[icon];
          this.wrapper.OnLog("IMON_Display_SetLcdOrangeIcon(" + Convert.ToString(numArray[0], 2) + ", " + Convert.ToString(numArray[0], 2) + ")");
          return iMonNativeApi.IMON_Display_SetLcdOrangeIcon(numArray[0], numArray[1]);
        default:
          return iMonNativeApi.iMonDisplayResult.Failed;
      }
    }
  }
}
