// Decompiled with JetBrains decompiler
// Type: iMon.DisplayApi.iMonVfd
// Assembly: iMonDisplayApiWrapperSharp, Version=0.1.0.7, Culture=neutral, PublicKeyToken=null
// MVID: A8FF370F-E095-466E-8933-1750BC929339
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\iMonDisplayApiWrapperSharp.dll

using System;

namespace iMon.DisplayApi
{
  public class iMonVfd
  {
    private iMonWrapperApi wrapper;

    internal iMonVfd(iMonWrapperApi wrapper)
    {
      if (wrapper == null)
        throw new ArgumentNullException(nameof (wrapper));
      this.wrapper = wrapper;
    }

    public bool SetText(string firstLine, string secondLine)
    {
      if (firstLine == null)
        throw new ArgumentNullException(nameof (firstLine));
      if (secondLine == null)
        throw new ArgumentNullException(nameof (secondLine));
      if (!this.wrapper.IsInitialized)
      {
        this.wrapper.OnLogError("VFD.SetText(): Not initialized");
        throw new InvalidOperationException("The display is not initialized");
      }
      this.wrapper.OnLog("IMON_Display_SetVfdText(" + firstLine + ", " + secondLine + ")");
      switch (iMonNativeApi.IMON_Display_SetVfdText(firstLine, secondLine))
      {
        case iMonNativeApi.iMonDisplayResult.Succeeded:
          return true;
        case iMonNativeApi.iMonDisplayResult.NotInitialized:
          this.wrapper.OnLogError("IMON_Display_SetVfdText() => Not initialized");
          throw new InvalidOperationException("The display is not initialized");
        case iMonNativeApi.iMonDisplayResult.InvalidPointer:
          this.wrapper.OnLogError("IMON_Display_SetVfdText() => Invalid pointer");
          throw new NullReferenceException();
        default:
          return false;
      }
    }

    public bool SetEqualizer(int[] bandData)
    {
      if (bandData == null)
        throw new ArgumentNullException(nameof (bandData));
      if (bandData.Length != 16)
        throw new ArgumentException("The equalizer band data must consist of 16 values between 0 and 100");
      if (!this.wrapper.IsInitialized)
      {
        this.wrapper.OnLogError("VFD.SetEqualizer(): Not initialized");
        throw new InvalidOperationException("The display is not initialized");
      }
      iMonNativeApi.iMonDisplayEqualizerData pEqData = new iMonNativeApi.iMonDisplayEqualizerData();
      pEqData.BandData = bandData;
      this.wrapper.OnLog("IMON_Display_SetVfdEqData()");
      switch (iMonNativeApi.IMON_Display_SetVfdEqData(ref pEqData))
      {
        case iMonNativeApi.iMonDisplayResult.Succeeded:
          return true;
        case iMonNativeApi.iMonDisplayResult.NotInitialized:
          this.wrapper.OnLogError("IMON_Display_SetVfdEqData() => Not initialized");
          throw new InvalidOperationException("The display is not initialized");
        case iMonNativeApi.iMonDisplayResult.InvalidPointer:
          this.wrapper.OnLogError("IMON_Display_SetVfdEqData() => Invalid pointer");
          throw new NullReferenceException();
        default:
          return false;
      }
    }

    internal void Reset()
    {
      this.wrapper.OnLog("VFD.Reset()");
      if (!this.wrapper.IsInitialized)
        return;
      this.SetText(string.Empty, string.Empty);
    }
  }
}
