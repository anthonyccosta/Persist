using Models;
using Newtonsoft.Json;
using Repositories;

// Leitura do JSON
var listaRadar = ReadFile.GetData(@"C:\5by5RADAR\dados_dos_radares.json");
var radarLista = new List<Radar>();

foreach (var jsonRadar in listaRadar)
{
    // Processamento dos dados
    var radar = JsonConvert.DeserializeObject<Radar>(jsonRadar);
    if (radar != null)
    {
        radarLista.Add(radar);
    }
}

// Armazenamento no SQL
SQL.InsertDBRadar(radarLista);

// Confirmando o sucesso
Console.WriteLine("Quantidade de Registros Lidos: " + radarLista.Count);
