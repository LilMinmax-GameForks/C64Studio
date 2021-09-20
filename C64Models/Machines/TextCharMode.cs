﻿using System.ComponentModel;

namespace RetroDevStudioModels
{
  public enum TextCharMode
  {
    [Description( "Unknown" )]
    UNKNOWN = -1,
    [Description( "Commodore HiRes" )]
    COMMODORE_HIRES = 0,
    [Description( "Commodore Multicolor" )]
    COMMODORE_MULTICOLOR,
    [Description( "Commodore ECM" )]
    COMMODORE_ECM,
    [Description( "Mega65 Full Color Mode" )]
    MEGA65_FCM,
    [Description( "Mega65 Full Color Mode 16bit" )]
    MEGA65_FCM_16BIT,
    [Description( "VC20 Text Mode" )]
    VC20
  }

}
