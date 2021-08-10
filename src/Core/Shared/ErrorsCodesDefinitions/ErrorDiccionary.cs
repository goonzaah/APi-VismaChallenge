using System.Collections.Generic;

namespace Core.Shared.ErrorsCodesDefinitions
{
    public static class ErrorDiccionary
    {
        private static Dictionary<ErrorCodes, string> _errorDescription;

        public static Dictionary<ErrorCodes, string> ErrorDescription
        {
            get
            {
                if (_errorDescription == null)
                    _errorDescription = new Dictionary<ErrorCodes, string>()
                    {
                        { ErrorCodes.OK, "Se ejecuto correctamente" },
                        { ErrorCodes.CantProcessAMessage,"No se pudo procesar el mensaje" },
                        { ErrorCodes.CantFindPosition,"No se pudo ubicar la posición" },
                        { ErrorCodes.NotHaveSatelliteData,"No se encontro la informacion suficiente de los satelites para procesar la informacion" },
                        { ErrorCodes.NotHavePositionsData,"No se encontro la informacion suficiente sobre la distancia a los satelite y el mensaje" }
                    };

                return _errorDescription;
            }
        }

        public static string GetErrorDescription(ErrorCodes code)
            => ErrorDescription[code];
    }
}
