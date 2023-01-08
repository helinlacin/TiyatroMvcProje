using EntityLayer;

namespace TiyatroProje.Models
{
    public class BiletMusteriModel
    {
        public OyunSalonMusteri BiletModal { get; set; }    
        public IEnumerable<Musteri> MusteriModal { get; set; }
    }
}
