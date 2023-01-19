namespace TiyatroProje.pager
{
    public class pager
    {

        public int baslangicSayfasi { get; set; }
        public int bitisSayfasi { get; set; }
        public int sayfaSayisi { get; set; }
        public int görüntülenenKayitSayisi { get; set; }
        public int toplamKayitSayisi { get; set; }
        public int aktifSayfasi { get; set; }
        public pager()
        {

        }
        public pager(int page, int pageSize, int itemCounts)
        {
            aktifSayfasi = page;
            görüntülenenKayitSayisi = pageSize;
            toplamKayitSayisi = itemCounts;

            sayfaSayisi = (int)Math.Ceiling((decimal)toplamKayitSayisi / (decimal)görüntülenenKayitSayisi);
            baslangicSayfasi = aktifSayfasi - 5;
            bitisSayfasi = aktifSayfasi + 4;
            if (baslangicSayfasi < 1)
            {
                bitisSayfasi = bitisSayfasi - (baslangicSayfasi - 1);
                baslangicSayfasi = 1;
            }
            if (bitisSayfasi > sayfaSayisi)
            {
                bitisSayfasi = sayfaSayisi;
                if (bitisSayfasi > 10)
                {
                    baslangicSayfasi = bitisSayfasi - 9;
                }
            }



        }
    }
}
