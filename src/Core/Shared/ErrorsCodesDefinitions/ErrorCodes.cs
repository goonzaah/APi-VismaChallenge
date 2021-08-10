using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Shared.ErrorsCodesDefinitions
{
    public enum ErrorCodes
    {
        OK = 0,
        CantProcessAMessage = 1,
        CantFindPosition = 2,
        NotHaveSatelliteData = 3,
        NotHavePositionsData = 4
    }
}
