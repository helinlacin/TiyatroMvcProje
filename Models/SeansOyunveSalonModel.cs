using EntityLayer;

namespace TiyatroProje.Models
{
    public class SeansOyunveSalonModel
    {
        public OyunSalon SeansModal { get; set; }
        public IEnumerable<Oyun> OyunModal { get; set; }
        public IEnumerable<Salon> SalonModal { get; set; }

    }
}
