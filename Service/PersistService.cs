using Model;
using Repositories;

namespace Services
{
    public class PersistService
    {
        private RadarRepository radarRepository;
        public PersistService()
        {
            radarRepository = new RadarRepository();
        }
        public bool Insert(Radar radar)
        {
            Console.WriteLine("Camada Services");
            return radarRepository.Insert(radar);
        }
        public bool Update(Radar radar)
        {
            return radarRepository.Update(radar);
        }
        public bool Delete(int id)
        {
            return radarRepository.Delete(id);
        }
        public Radar Get(int id)
        {
            return radarRepository.Get(id);
        }
        public List<Radar> GetAll()
        {
            return radarRepository.GetAll();
        }

    }
}