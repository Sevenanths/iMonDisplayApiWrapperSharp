// Decompiled with JetBrains decompiler
// Type: iMon.DisplayApi.iMonDisplayType
// Assembly: iMonDisplayApiWrapperSharp, Version=0.1.0.7, Culture=neutral, PublicKeyToken=null
// MVID: A8FF370F-E095-466E-8933-1750BC929339
// Assembly location: Z:\Beast\xbmc-on-imon\XbmcOnImonVFD-frodo.v1.0.4ddd\XbmcOnImonVFD\iMonDisplayApiWrapperSharp.dll

using System;

namespace iMon.DisplayApi
{
  [Flags]
  public enum iMonDisplayType
  {
    None = 0,
    VFD = 1,
    LCD = 2,
    Unknown = 4,
  }
}
