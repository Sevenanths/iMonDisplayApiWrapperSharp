// Decompiled with JetBrains decompiler
// Type: iMon.DisplayApi.iMonErrorEventArgs
// Assembly: iMonDisplayApiWrapperSharp, Version=0.1.0.7, Culture=neutral, PublicKeyToken=null
// MVID: A8FF370F-E095-466E-8933-1750BC929339
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\iMonDisplayApiWrapperSharp.dll

using System;

namespace iMon.DisplayApi
{
  public class iMonErrorEventArgs : EventArgs
  {
    private iMonErrorType type;

    public iMonErrorType Type
    {
      get
      {
        return this.type;
      }
    }

    public iMonErrorEventArgs(iMonErrorType errorType)
    {
      this.type = errorType;
    }
  }
}
