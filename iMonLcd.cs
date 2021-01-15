// Decompiled with JetBrains decompiler
// Type: iMon.DisplayApi.iMonLcd
// Assembly: iMonDisplayApiWrapperSharp, Version=0.1.0.7, Culture=neutral, PublicKeyToken=null
// MVID: A8FF370F-E095-466E-8933-1750BC929339
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\iMonDisplayApiWrapperSharp.dll

using System;

namespace iMon.DisplayApi
{
  public class iMonLcd
  {
    private iMonWrapperApi wrapper;
    private iMonLcdIconsControl icons;

    public event EventHandler ScrollFinished;

    public iMonLcdIconsControl Icons
    {
      get
      {
        return this.icons;
      }
    }

    internal iMonLcd(iMonWrapperApi wrapper)
    {
      if (wrapper == null)
        throw new ArgumentNullException(nameof (wrapper));
      this.wrapper = wrapper;
      this.icons = new iMonLcdIconsControl(wrapper);
    }

    public bool SetText(string text)
    {
      if (text == null)
        throw new ArgumentNullException(nameof (text));
      if (!this.wrapper.IsInitialized)
      {
        this.wrapper.OnLogError("LCD.SetText(): Not initialized");
        throw new InvalidOperationException("The display is not initialized");
      }
      this.wrapper.OnLog("IMON_Display_SetLcdText(" + text + ")");
      switch (iMonNativeApi.IMON_Display_SetLcdText(text))
      {
        case iMonNativeApi.iMonDisplayResult.Succeeded:
          return true;
        case iMonNativeApi.iMonDisplayResult.NotInitialized:
          this.wrapper.OnLogError("IMON_Display_SetLcdText() => Not initialized");
          throw new InvalidOperationException("The display is not initialized");
        case iMonNativeApi.iMonDisplayResult.InvalidPointer:
          this.wrapper.OnLogError("IMON_Display_SetLcdText() => Invalid pointer");
          throw new NullReferenceException();
        default:
          return false;
      }
    }

    public bool SetEqualizer(int[] leftChannelBandData, int[] rightChannelBandData)
    {
      if (leftChannelBandData == null)
        throw new ArgumentNullException(nameof (leftChannelBandData));
      if (rightChannelBandData == null)
        throw new ArgumentNullException(nameof (rightChannelBandData));
      if (leftChannelBandData.Length != 16)
        throw new ArgumentException("The equalizer's left channel band data must consist of 16 values between 0 and 100");
      if (rightChannelBandData.Length != 16)
        throw new ArgumentException("The equalizer's right channel band data must consist of 16 values between 0 and 100");
      if (!this.wrapper.IsInitialized)
      {
        this.wrapper.OnLogError("LCD.SetEqualizer(): Not initialized");
        throw new InvalidOperationException("The display is not initialized");
      }
      iMonNativeApi.iMonDisplayEqualizerData pEqDataL = new iMonNativeApi.iMonDisplayEqualizerData();
      pEqDataL.BandData = leftChannelBandData;
      iMonNativeApi.iMonDisplayEqualizerData pEqDataR = new iMonNativeApi.iMonDisplayEqualizerData();
      pEqDataR.BandData = rightChannelBandData;
      this.wrapper.OnLog("IMON_Display_SetLcdEqData()");
      switch (iMonNativeApi.IMON_Display_SetLcdEqData(ref pEqDataL, ref pEqDataR))
      {
        case iMonNativeApi.iMonDisplayResult.Succeeded:
          return true;
        case iMonNativeApi.iMonDisplayResult.NotInitialized:
          this.wrapper.OnLogError("IMON_Display_SetLcdEqData() => Not initialized");
          throw new InvalidOperationException("The display is not initialized");
        case iMonNativeApi.iMonDisplayResult.InvalidPointer:
          this.wrapper.OnLogError("IMON_Display_SetLcdEqData() => Invalid pointer");
          throw new NullReferenceException();
        default:
          return false;
      }
    }

    public bool SetProgress(int value, int total)
    {
      if (value < 0)
        throw new ArgumentException("The value of the progress can't be less than 0");
      if (total < 0)
        throw new ArgumentException("The maximum of the progress can't be less than 0");
      if (value > total)
        value = total;
      if (!this.wrapper.IsInitialized)
      {
        this.wrapper.OnLogError("LCD.SetProgress(): Not initialized");
        throw new InvalidOperationException("The display is not initialized");
      }
      this.wrapper.OnLog("IMON_Display_SetLcdProgress(" + (object) value + ", " + (object) total + ")");
      switch (iMonNativeApi.IMON_Display_SetLcdProgress(value, total))
      {
        case iMonNativeApi.iMonDisplayResult.Succeeded:
          return true;
        case iMonNativeApi.iMonDisplayResult.NotInitialized:
          this.wrapper.OnLogError("IMON_Display_SetLcdProgress() => Not initialized");
          throw new InvalidOperationException("The display is not initialized");
        default:
          return false;
      }
    }

    internal void Reset()
    {
      this.wrapper.OnLog("LCD.Reset()");
      if (!this.wrapper.IsInitialized)
        return;
      this.SetText(string.Empty);
      this.icons.Reset();
    }

    internal void OnScrollFinished()
    {
      if (this.ScrollFinished == null)
        return;
      this.ScrollFinished((object) this, new EventArgs());
    }
  }
}
