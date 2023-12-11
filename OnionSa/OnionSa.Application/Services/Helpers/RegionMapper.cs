namespace OnionSa.Application.Services.Helpers
{

    public class RegionMapper
    {
        private readonly Dictionary<string, string> _stateToRegionMap = new Dictionary<string, string>
    {
        {"AC", "Norte"},
        {"AL", "Nordeste"},
        {"AP", "Norte"},
        {"AM", "Norte"},
        {"BA", "Nordeste"},
        {"CE", "Nordeste"},
        {"DF", "Centro-Oeste"},
        {"ES", "Sudeste"},
        {"GO", "Centro-Oeste"},
        {"MA", "Nordeste"},
        {"MT", "Centro-Oeste"},
        {"MS", "Centro-Oeste"},
        {"MG", "Sudeste"},
        {"PA", "Norte"},
        {"PB", "Nordeste"},
        {"PR", "Sul"},
        {"PE", "Nordeste"},
        {"PI", "Nordeste"},
        {"RJ", "Sudeste"},
        {"RN", "Nordeste"},
        {"RS", "Sul"},
        {"RO", "Norte"},
        {"RR", "Norte"},
        {"SC", "Sul"},
        {"SP", "Sudeste"},
        {"SE", "Nordeste"},
        {"TO", "Norte"}
    };

        public string GetRegionByState(string stateCode)
        {
            if (_stateToRegionMap.ContainsKey(stateCode))
            {
                return _stateToRegionMap[stateCode];
            }
            else
            {
                return "Não Encontrado";
            }
        }
    }

}
