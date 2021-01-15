// Decompiled with JetBrains decompiler
// Type: iMon.DisplayApi.iMonWrapperApi
// Assembly: iMonDisplayApiWrapperSharp, Version=0.1.0.7, Culture=neutral, PublicKeyToken=null
// MVID: A8FF370F-E095-466E-8933-1750BC929339
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\iMonDisplayApiWrapperSharp.dll

using System;
using System.Windows.Forms;

namespace iMon.DisplayApi
{
  public class iMonWrapperApi : Control
  {
    private const int WM_IMON_NOTIFICATION = 37428;
    private bool disposed;
    private bool initialized;
    private iMonDisplayType displayType;
    private iMonVfd vfd;
    private iMonLcd lcd;

    public event EventHandler<iMonStateChangedEventArgs> StateChanged;

    public event EventHandler<iMonErrorEventArgs> Error;

    public event EventHandler<iMonLogErrorEventArgs> LogError;

    public event EventHandler<iMonLogEventArgs> Log;

    public bool IsInitialized
    {
      get
      {
        return this.initialized;
      }
    }

    public bool IsPluginModeEnabled
    {
      get
      {
        this.OnLog("IMON_Display_IsPluginModeEnabled()");
        return iMonNativeApi.IMON_Display_IsPluginModeEnabled() == iMonNativeApi.iMonDisplayResult.InPluginMode;
      }
    }

    public iMonDisplayType DisplayType
    {
      get
      {
        return this.displayType;
      }
    }

    public iMonVfd VFD
    {
      get
      {
        if ((this.displayType & iMonDisplayType.VFD) == iMonDisplayType.VFD)
          return this.vfd;
        this.OnLogError("VFD is not available");
        return (iMonVfd) null;
      }
    }

    public iMonLcd LCD
    {
      get
      {
        if ((this.displayType & iMonDisplayType.LCD) == iMonDisplayType.LCD)
          return this.lcd;
        this.OnLogError("LCD is not available");
        return (iMonLcd) null;
      }
    }

    public iMonWrapperApi()
    {
      this.displayType = iMonDisplayType.Unknown;
      this.vfd = new iMonVfd(this);
      this.lcd = new iMonLcd(this);
    }

    public void Initialize()
    {
      if (this.IsInitialized)
        return;
      this.OnLog("IMON_Display_Init(" + (object) this.Handle + ", " + (object) 37428 + ")");
      iMonNativeApi.iMonDisplayResult result = iMonNativeApi.IMON_Display_Init(this.Handle, 37428U);
      if (result == iMonNativeApi.iMonDisplayResult.Succeeded)
        return;
      this.onError(this.getErrorType(result));
    }

    public void Uninitialize()
    {
      if (!this.IsInitialized)
        return;
      if ((this.displayType & iMonDisplayType.VFD) == iMonDisplayType.VFD)
        this.vfd.Reset();
      if ((this.displayType & iMonDisplayType.LCD) == iMonDisplayType.LCD)
        this.lcd.Reset();
      this.OnLog("IMON_Display_Uninit()");
      int num = (int) iMonNativeApi.IMON_Display_Uninit();
      this.onStateChanged(false);
    }

    public new void Dispose()
    {
      if (this.disposed)
        return;
      this.Uninitialize();
      this.Dispose(true);
      this.disposed = true;
      GC.SuppressFinalize((object) this);
    }

    protected override void WndProc(ref Message msg)
    {
      if (msg.Msg == 37428)
        this.onMessage((iMonNativeApi.iMonDisplayNotifyCode) (int) msg.WParam, msg.LParam);
      base.WndProc(ref msg);
    }

    internal void OnLog(string message)
    {
      if (this.Log == null || string.IsNullOrEmpty(message))
        return;
      this.Log((object) this, new iMonLogEventArgs(message));
    }

    internal void OnLogError(string message)
    {
      this.OnLogError(message, (Exception) null);
    }

    internal void OnLogError(string message, Exception exception)
    {
      if (this.LogError == null || string.IsNullOrEmpty(message))
        return;
      this.LogError((object) this, new iMonLogErrorEventArgs(message, exception));
    }

    private void onMessage(iMonNativeApi.iMonDisplayNotifyCode code, IntPtr data)
    {
      this.OnLog("Message received: " + (object) code + "(" + (object) data + ")");
      switch (code)
      {
        case iMonNativeApi.iMonDisplayNotifyCode.PluginSuccess:
        case iMonNativeApi.iMonDisplayNotifyCode.iMonRestarted:
        case iMonNativeApi.iMonDisplayNotifyCode.HardwareConnected:
          this.displayType = (iMonDisplayType) (int) data;
          this.onStateChanged(true);
          break;
        case iMonNativeApi.iMonDisplayNotifyCode.PluginFailed:
          this.onError(this.getErrorType((iMonNativeApi.iMonDisplayInitResult) (int) data));
          break;
        case iMonNativeApi.iMonDisplayNotifyCode.iMonClosed:
        case iMonNativeApi.iMonDisplayNotifyCode.HardwareDisconnected:
          this.onError(this.getErrorType(code));
          break;
        case iMonNativeApi.iMonDisplayNotifyCode.LCDTextScrollDone:
          this.lcd.OnScrollFinished();
          break;
      }
    }

    private void onStateChanged(bool isInitialized)
    {
      if (this.initialized == isInitialized)
        return;
      this.OnLog("State changed");
      this.initialized = isInitialized;
      if (this.StateChanged == null)
        return;
      this.StateChanged((object) this, new iMonStateChangedEventArgs(isInitialized));
    }

    private void onError(iMonErrorType error)
    {
      this.OnLogError("Error received: " + (object) error);
      if (this.Error != null)
        this.Error((object) this, new iMonErrorEventArgs(error));
      this.onStateChanged(false);
    }

    private iMonErrorType getErrorType(iMonNativeApi.iMonDisplayResult result)
    {
      this.OnLogError("Interpreting result error type: " + (object) result);
      switch (result)
      {
        case iMonNativeApi.iMonDisplayResult.Failed:
          return iMonErrorType.Unknown;
        case iMonNativeApi.iMonDisplayResult.OutOfMemory:
          return iMonErrorType.OutOfMemory;
        case iMonNativeApi.iMonDisplayResult.InvalidArguments:
          return iMonErrorType.InvalidArguments;
        case iMonNativeApi.iMonDisplayResult.NotInitialized:
          return iMonErrorType.NotInitialized;
        case iMonNativeApi.iMonDisplayResult.InvalidPointer:
          return iMonErrorType.InvalidPointer;
        case iMonNativeApi.iMonDisplayResult.ApiNotInitialized:
          return iMonErrorType.ApiNotInitialized;
        case iMonNativeApi.iMonDisplayResult.NotInPluginMode:
          return iMonErrorType.NotInPluginMode;
        default:
          return iMonErrorType.Unknown;
      }
    }

    private iMonErrorType getErrorType(iMonNativeApi.iMonDisplayNotifyCode notifyCode)
    {
      this.OnLogError("Interpreting notify error type: " + (object) notifyCode);
      switch (notifyCode)
      {
        case iMonNativeApi.iMonDisplayNotifyCode.PluginFailed:
          return iMonErrorType.Unknown;
        case iMonNativeApi.iMonDisplayNotifyCode.iMonClosed:
          return iMonErrorType.iMonClosed;
        case iMonNativeApi.iMonDisplayNotifyCode.HardwareDisconnected:
          return iMonErrorType.HardwareDisconnected;
        default:
          return iMonErrorType.Unknown;
      }
    }

    private iMonErrorType getErrorType(iMonNativeApi.iMonDisplayInitResult code)
    {
      this.OnLogError("Interpreting init result error type: " + (object) code);
      switch (code)
      {
        case iMonNativeApi.iMonDisplayInitResult.PluginModeAlreadyInUse:
          return iMonErrorType.PluginModeAlreadyInUse;
        case iMonNativeApi.iMonDisplayInitResult.HardwareNotConnected:
          return iMonErrorType.HardwareNotConnected;
        case iMonNativeApi.iMonDisplayInitResult.HardwareNotSupported:
          return iMonErrorType.HardwareNotSupported;
        case iMonNativeApi.iMonDisplayInitResult.PluginModeDisabled:
          return iMonErrorType.PluginModeDisabled;
        case iMonNativeApi.iMonDisplayInitResult.iMonNotResponding:
          return iMonErrorType.iMonNotResponding;
        default:
          return iMonErrorType.Unknown;
      }
    }
  }
}
