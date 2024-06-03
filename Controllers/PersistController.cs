using Models;
using Services;

namespace Controllers
{
    public class PersistController
    {
        private PersistService _service;

        public PersistController()
        {
            _service = new PersistService();
        }
        public bool Insert(Radar radar)
        {
            return true;
        }

        public bool Delete(int id)
        {
            return _service.Delete(id);
        }
        public Radar Get(int id)
        {
            return _service.Get(id);
        }

        public bool Update(Radar radar)
        {
            return _service.Update(radar);
        }
        public List<Radar> GetAll()
        {
            return _service.GetAll();
        }

        public static int getCountRecords(List<Radar> lista) => lista.Count;
        public static void printData(List<Radar> lst)
        {
            foreach (var p in lst)
            {
                Console.WriteLine(p);

            }
        }

    }
}