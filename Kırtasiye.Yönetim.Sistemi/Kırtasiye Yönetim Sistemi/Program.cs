using NesneyeYönelikProgramlamaProjesi;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NesneyeYönelikProgramlamaProjesi
{


    // Kitap interface'i oluşturuyoruz
    interface IKitap
    {
        string Yazar { get; set; }
        int SayfaSayisi { get; set; }



    }

    // Elektronik ürün interface'i oluşturuyoruz
    interface IElektronikUrun
    {
        string Marka { get; set; }
        int GarantiSuresi { get; set; }

    }


}

//Partial urun classı oluşturuyoruz içinde UrunID tutacağız
public partial class Urun
{
    private int urunid;

    public int UrunID
    {
        //properties oluşturuyoruz
        get { return urunid; }
        set { urunid = value; }

    }


}
//Partial urun classı oluşturuyoruz içinde kalan özellikleri tutacağız
public partial class Urun
{
    public string Yorum { get; set; }
    public int yorumkk { get; set; } // bunu yorum kontrol için kullanacağız, yorum listelerken eğer ürüne yorum yapıldıysa vb.
    public string Ad { get; set; }
    public decimal Fiyat { get; set; }
    public int Stok { get; set; }

    private int Miktar; // kaç adet ürün satın aldığımızı gösterecek değişken
    public int miktar { get { return Miktar; } set { Miktar = value; } }

    //Listeleme metodu için virtual bir metod oluşturuyoruz
    public virtual void Listele(UrunYonetimSistemi ur)
    { }
    //Ürün silmek için virtual bir metod oluşturuyoruz
    public virtual void UrunSil(UrunYonetimSistemi ur)
    {

    }
    //Ürün güncellemek için virtual bir metod oluşturuyoruz
    public virtual void UrunGuncelle(UrunYonetimSistemi ur)
    {

    }
    //Ürün incelemesi için virtual bir metod oluşturuyoruz
    public virtual void İncele(UrunYonetimSistemi ur) { }
    //Ürün satın alma için virtual bir metod oluşturuyoruz
    public virtual void SatınAlma(UrunYonetimSistemi ur)
    {

    }
    // satın alınan ürünler için virtual bir metod oluşturuyoruz
    public virtual void satinalinanlar(UrunYonetimSistemi ur)
    {

    }
    // Ürün yorumlama işlemi yapan metod
    public virtual void UrunYorumla(UrunYonetimSistemi ur)
    {

    }
    // Ürün yorumlarını gösterecek metod
    public virtual void UrunYorumlari(UrunYonetimSistemi ur)
    {

    }
}
//Elektronik ürün sınıfı oluşturuyoruz ve ürün classından ve IElektronikUrun interface'inden kalıtım alıyoruz
public class ElektronikUrun : Urun, IElektronikUrun
{
    public int GarantiSuresi { get; set; }
    public string Marka { get; set; }
    static int intkontrol()
    {
        int deger;
        bool isValidInput;

        do
        {
            isValidInput = int.TryParse(Console.ReadLine(), out deger);

            if (!isValidInput)
            {
                Console.WriteLine("Geçersiz giriş! Lütfen bir sayı girin.");
            }

        } while (!isValidInput);

        return deger;
    }

    //Listele metodunu override ediyoruz ve elektronik ürün listeleme işlemini yapıyoruz.
    public override void Listele(UrunYonetimSistemi ur)
    {

        if (ur.Elektronikler.Count == 0) // eğer elektronik ürün listesi boş ise uyarı ver
        {
            Console.Clear();
            Console.WriteLine("Elektronik ürün listesi boş!");
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Elektronik Ürün Listesi ");
            var siraliElektronikler = ur.Elektronikler.OrderBy(e => e.UrunID).ToList(); // burada ID ye göre küçükten büyüğe sıralama işlemi yapılıyor.

            foreach (ElektronikUrun e in siraliElektronikler)
            {
                Console.WriteLine($"ID:{e.UrunID}  Ad:{e.Ad}  Marka:{e.Marka}  Stok:{e.Stok}  Fiyat:{e.Fiyat} Garanti Süresi:{e.GarantiSuresi}" + " ay");
            }
        }
        Console.WriteLine("Devam etmek için bir tuşa basınız..");
        Console.ReadLine();
        Console.Clear();
    }

    //Ürün silme metodunu override ediyoruz ve elektronik ürün işlemini yapıyoruz.
    public override void UrunSil(UrunYonetimSistemi ur)
    {
        Console.Clear();
        Console.WriteLine("Silmek istenen ürünün ID'sini giriniz");
        int UrunId = intkontrol(); // alınan değer int mi kontrolü
        ElektronikUrun elektronik = ur.Elektronikler.FirstOrDefault(u => u.UrunID == UrunId) as ElektronikUrun;
        if (elektronik != null)
        {
            ur.Elektronikler.Remove(elektronik); // Ürün silme işlemi
            Console.WriteLine($"{elektronik.UrunID} ID li elektronik ürün başarıyla silindi..");
            Console.WriteLine("Lütfen bekleyin yönlendiriliyorsunuz...");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
        }
        else // eğer girilen ıd ile eşleşen ürün yoksa
        {
            Console.WriteLine("Bu ID ile herhangi bir ürün bulunamadı!");
            Console.WriteLine("Lütfen bekleyin yönlendiriliyorsunuz...");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
        }

    }
    //Ürün güncelleme metodunu override ediyoruz ve elektronik ürün güncelleme işlemini yapıyoruz
    public override void UrunGuncelle(UrunYonetimSistemi ur)
    {
        int secim3;
        do
        {
        MenüGüncelle:
            Console.Clear();
            Console.WriteLine("1-Fiyat güncelle\n2-Stok güncelle\n3-Ana menü");
            secim3 = intkontrol();
            switch (secim3)
            {
                case 1:
                    Console.Clear();
                    ur.EUrunFiyatGuncelle();
                    goto MenüGüncelle;
                case 2:
                    Console.Clear();
                    ur.ElektronikStokGuncelle();
                    goto MenüGüncelle;
                case 3:
                    Console.Clear();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("1-3 aralığında bir değer girin");
                    Console.WriteLine("Yönlendiriliyorsunuz...");
                    System.Threading.Thread.Sleep(1500);
                    goto MenüGüncelle;
            }
        } while (secim3 != 3);
    }

    //İncele metodunu override ediyoruz ve elektronik ürün inceleme işlemini yapıyoruz
    public override void İncele(UrunYonetimSistemi ur)
    {
        if (ur.Elektronikler.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("Elektronik ürün listesi boş!");
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Elektronik Ürün Listesi ");
            var siraliElektronikler = ur.Elektronikler.OrderBy(e => e.UrunID).ToList();

            foreach (ElektronikUrun e in siraliElektronikler)
            {
                Console.WriteLine($"ID:{e.UrunID}  Ad:{e.Ad}  Marka:{e.Marka}  Stok:{e.Stok}  Fiyat:{e.Fiyat} Garanti Süresi:{e.GarantiSuresi}" + " ay");
            }
        }
    }

    //Satınalma metodunu override ediyoruz ve elektronik ürün satın alma işlemini yapıyoruz
    public override void SatınAlma(UrunYonetimSistemi ur)
    {
        if (ur.Elektronikler.Count == 0) // liste boş ise ekrana yazdırıyor
        {
            Console.Clear();
            Console.WriteLine("Elektronik ürünler listesi boş! ");
            Console.WriteLine("Devam etmek için bir tuşa basınız..");
            Console.ReadLine();
            Console.Clear();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Elektronik Ürün Listesi ");
            var siraliElektronikler = ur.Elektronikler.OrderBy(e => e.UrunID).ToList(); // elektronik ürün listesini ID'ye göre sırala

            foreach (ElektronikUrun e in siraliElektronikler)
            {
                Console.WriteLine($"ID:{e.UrunID}  Ad:{e.Ad}  Marka:{e.Marka}  Stok:{e.Stok}  Fiyat:{e.Fiyat} Garanti Süresi:{e.GarantiSuresi}" + " ay");
            }

            Console.WriteLine("Satın almak istediğiniz ürünün ID'sini giriniz..");
            int urunid = intkontrol();
            ElektronikUrun eurun = ur.Elektronikler.FirstOrDefault(u => u.UrunID == urunid) as ElektronikUrun;
            if (eurun != null)
            {
                Console.WriteLine($"Ürünün fiyatı: {eurun.Fiyat}");
                int fiyat = Convert.ToInt32(eurun.Fiyat);
                if (ur.bakiye < fiyat) Console.WriteLine("Yetersiz bakiye!"); // bakiye yeterli değil ise uyarı versin
                else if (eurun.Stok == 0) Console.WriteLine("Ürünün yeterli stoğu yok! Lütfen yöneticiniz stok eklesin"); // ürün stoğu yoksa uyarı versin
                else
                {
                    Console.WriteLine("Ürün başarıyla satın alındı!");
                    // ürünün stok miktarını düşürüyoruz
                    eurun.Stok -= 1;
                    //satın alınan miktarı arttırıyoruz
                    eurun.miktar += 1;
                    //bakiye güncellemesi yapıyoruz
                    ur.bakiye = ur.bakiye - fiyat;
                    ur.satinalinanelektronikler.Add(eurun);
                }
                Console.WriteLine("Devam etmek için bir tuşa basınız..");
                Console.ReadKey();
            }




        }


    }

    // satınalınanlar metodunu override ediyoruz ve satın alınanlar elektronik ürünleri listeleme işlemini yapıyoruz
    public override void satinalinanlar(UrunYonetimSistemi ur)
    {

        if (ur.satinalinanelektronikler.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("Satın alınan elektronik ürün listesi boş!");
            Console.WriteLine("Devam etmek için bir tuşa basınız...");
            Console.ReadKey();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Satın alınan elektronik Ürün Listesi ");
            var siraliElektronikler = ur.satinalinanelektronikler.OrderBy(e => e.UrunID).ToList();

            foreach (ElektronikUrun e in siraliElektronikler)
            {
                Console.WriteLine($"ID:{e.UrunID}  Ad:{e.Ad}  Marka:{e.Marka}  Miktar:{e.miktar}  Fiyat:{e.Fiyat} Garanti Süresi:{e.GarantiSuresi}" + " ay");
            }

            Console.WriteLine("Devam etmek için bir tuşa basınız...");
            Console.ReadLine();
            Console.Clear();
        }
    }

    // urunyorumla metodunu override ediyoruz ve elektronik ürün yorumlama işlemini gerçekleştiriyoruz
    public override void UrunYorumla(UrunYonetimSistemi ur)
    {
        if (ur.satinalinanelektronikler.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("Satın alınan elektronik ürünler listesi boş! Ürün yorumlamak için önce satın almalısınız. ");


        }
        else
        {
        YorumID:
            Console.Clear();
            Console.WriteLine("Elektronik Ürün Listesi");
            var siraliElektronikler = ur.satinalinanelektronikler.OrderBy(e => e.UrunID).ToList();

            foreach (ElektronikUrun e in siraliElektronikler)
            {
                Console.WriteLine($"ID:{e.UrunID}  Ad:{e.Ad}  Marka:{e.Marka} Fiyat:{e.Fiyat} Garanti Süresi:{e.GarantiSuresi}" + " ay");
            }

            Console.WriteLine("Yorumlamak istediğiniz ürünün ID'sini giriniz:");
            int urunid = intkontrol();
            ElektronikUrun eurun = ur.satinalinanelektronikler.FirstOrDefault(u => u.UrunID == urunid) as ElektronikUrun;

            if (eurun != null)
            {
                // Kullanıcı daha önceden yorum yapıp yapmadığını kontrol ediyoruz
                if (eurun.yorumkk == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Lütfen yorumunuzu giriniz:");
                    eurun.Yorum = Console.ReadLine();
                    Console.WriteLine("Yorumunuz başarıyla eklendi.");
                    eurun.yorumkk = 1;
                    // Kullanıcıdan yorumu al



                    // E-posta gönderimi için gerekli bilgiler
                    string smtpServer = "smtp.gmail.com"; // SMTP sunucu adresi
                    int smtpPort = 587; // SMTP port numarası (genellikle 587 veya 465)
                    string smtpUsername = "kozdemir.usa@gmail.com"; // E-posta adresiniz
                    string smtpPassword = "ucuweptvmkxzgoeh"; // E-posta şifreniz

                    // Gönderilecek e-posta bilgileri
                    string toEmailAddress = "kozdemir.usa@gmail.com";
                    string subject = "Ürün yorumu";
                    string body = $"{eurun.UrunID} ID'li elektronik ürüne gelen yorum:{eurun.Yorum} ";

                    // MailMessage sınıfı ile e-posta oluşturma
                    MailMessage mail = new MailMessage(smtpUsername, toEmailAddress, subject, body);

                    // SmtpClient sınıfı ile e-posta gönderimi
                    using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                    {
                        smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                        smtpClient.EnableSsl = true; // SSL kullanılacaksa true, kullanılmayacaksa false

                        try
                        {
                            smtpClient.Send(mail);
                            Console.WriteLine("E-posta başarıyla gönderildi.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("E-posta gönderiminde hata oluştu: " + ex.ToString());
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Bu ürüne zaten yorum yapılmış!");
                }
            }
            else
            {
                Console.WriteLine("Lütfen doğru ID giriniz..");
                Console.WriteLine("Yönlendiriliyorsunuz...");
                System.Threading.Thread.Sleep(1500);
                goto YorumID;
            }
        }
        Console.WriteLine("Devam etmek için bir tuşa basınız..");
        Console.ReadKey();

    }
    //Yorumlanan elektronik ürünleri görüntüleyen metod
    public override void UrunYorumlari(UrunYonetimSistemi ur)
    {
        if (ur.Elektronikler.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("Elektronik ürünler listesi boş! ");

        }
        else
        {
            Console.Clear();
            Console.WriteLine("Elektronik Ürün Listesi");
            var siraliElektronikler = ur.Elektronikler.OrderBy(e => e.UrunID).ToList();

            foreach (ElektronikUrun e in siraliElektronikler)
            {
                if (e.yorumkk == 1)
                    Console.WriteLine($"ID:{e.UrunID}  Ad:{e.Ad} Fiyat:{e.Fiyat}   Yapılan Yorum:{e.Yorum}");
                else
                {
                    Console.WriteLine($"ID:{e.UrunID}  Ad:{e.Ad} Fiyat:{e.Fiyat} Bu ürüne yapılan bir yorum bulunamadı!");
                }
            }
        }
        Console.WriteLine("Devam etmek için bir tuşa basınız..");
        Console.ReadKey();
    }
}
//Kitap ürün sınıfı oluşturuyoruz
public class KitapUrun : Urun, IKitap
{

    public string Yazar { get; set; }
    public int SayfaSayisi
    {
        get;
        set;

    }
    static int intkontrol() // int kontrol metodu oluşturuluyor
    {
        int deger;
        bool isValidInput;

        do
        {
            isValidInput = int.TryParse(Console.ReadLine(), out deger);

            if (!isValidInput)
            {
                Console.WriteLine("Geçersiz giriş! Lütfen bir sayı girin.");
            }

        } while (!isValidInput);

        return deger;
    }

    //Listele metodunu override ediyoruz ve listeleme işlemini yapıyoruz.
    public override void Listele(UrunYonetimSistemi ur)
    {
        //Eğer kitap listesinin sayısı 0 ise kitap listesi boş uyarısı.
        if (ur.Kitaplar.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("Kitap listesi boş!");

        }
        else
        {
            Console.Clear();
            Console.WriteLine("Kitap Listesi");
            // Kitapları ID'ye göre sırala ve öyle yazdır
            var siraliKitaplar = ur.Kitaplar.OrderBy(e => e.UrunID).ToList();

            foreach (KitapUrun e in siraliKitaplar)
            {
                Console.WriteLine($"ID:{e.UrunID}  Ad:{e.Ad}  Stok:{e.Stok}  Fiyat:{e.Fiyat} Sayfa Sayısı:{e.SayfaSayisi} sayfa ");
            }
        }
        Console.WriteLine("Devam etmek için bir tuşa basınız..");
        Console.ReadLine();
        Console.Clear();
    }
    //UrunSil metodunu override ediyoruz ve kitap silme işlemini yapıyoruz
    public override void UrunSil(UrunYonetimSistemi ur)
    {
        Console.Clear();
        Console.WriteLine("Silmek istenen ürünün ID'sini giriniz");
        int UrunId = intkontrol();
        KitapUrun kitap = ur.Kitaplar.FirstOrDefault(u => u.UrunID == UrunId) as KitapUrun;
        if (kitap != null) // eğer girilen ID ile eşleşen ürün varsa aşşağıdaki işlemler yapılıcak
        {
            ur.Kitaplar.Remove(kitap);
            Console.WriteLine($"{kitap.UrunID} ID li kitap başarıyla silindi..");
            Console.WriteLine("Lütfen bekleyin yönlendiriliyorsunuz...");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
        }
        else
        {
            Console.WriteLine("Bu ID ile herhangi bir ürün bulunamadı!");
            Console.WriteLine("Lütfen bekleyin yönlendiriliyorsunuz...");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
        }
    }
    //Urun guncelle metodunu override ediyoruz ve kitap güncelleme metodunu oluşturuypruz
    public override void UrunGuncelle(UrunYonetimSistemi ur)
    {
        int secim2;
        do
        {
            Console.Clear();
            Console.WriteLine("1-Kitap Stoğu Güncelle\n2-Kitap Fiyatı Güncelle\n3-Ana Menü");

            secim2 = intkontrol();
            switch (secim2)
            {
                case 1:
                    Console.Clear();
                    ur.KitapStokGuncelle();
                    break;
                case 2:
                    Console.Clear();
                    ur.KitapFiyatGuncelle();
                    break;
                case 3:
                    Console.Clear();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("1-3 aralığında bir değer girin");
                    Console.WriteLine("Yönlendiriliyorsunuz...");
                    System.Threading.Thread.Sleep(1500);
                    break;
            }
        } while (secim2 != 3);
    }
    public override void SatınAlma(UrunYonetimSistemi ur)
    {
        if (ur.Kitaplar.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("Kitap listesi boş!");
            Console.WriteLine("Devam etmek için bir tuşa basınız...");
            Console.ReadKey();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Kitap Listesi");
            // Kitapları ID'ye göre sırala ve öyle yazdır
            var siraliKitaplar = ur.Kitaplar.OrderBy(e => e.UrunID).ToList();

            foreach (KitapUrun e in siraliKitaplar)
            {
                Console.WriteLine($"ID:{e.UrunID}  Ad:{e.Ad}  Stok:{e.Stok}  Fiyat:{e.Fiyat} ");
            }

            Console.WriteLine("Satın almak istediğiniz ürünün ID'sini giriniz..");
            int urunid = intkontrol();
            KitapUrun kitap = ur.Kitaplar.FirstOrDefault(u => u.UrunID == urunid) as KitapUrun;
            if (kitap != null)
            {
                Console.WriteLine($"Ürünün fiyatı: {kitap.Fiyat}");
                int fiyat = Convert.ToInt32(kitap.Fiyat);
                if (ur.bakiye < fiyat) Console.WriteLine("Yetersiz bakiye!");
                else if (kitap.Stok == 0) Console.WriteLine("Ürünün yeterli stoğu yok! Lütfen yöneticiniz stok eklesin"); // ürün stoğu yoksa uyarı versin
                else
                {
                    Console.WriteLine("Ürün başarıyla satın alındı!");
                    kitap.Stok -= 1;
                    kitap.miktar += 1;
                    ur.bakiye = ur.bakiye - fiyat;
                    ur.satinalinankitaplar.Add(kitap);
                }
                Console.WriteLine("Devam etmek için bir tuşa basınız..");
                Console.ReadKey();
            }




        }
    }
    public override void satinalinanlar(UrunYonetimSistemi ur)
    {

        if (ur.satinalinankitaplar.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("Satın alınan kitap listesi boş!");
            Console.WriteLine("Devam etmek için bir tuşa basınız...");
            Console.ReadKey();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Satın alınan kitap Listesi ");
            var siraliKitaplar = ur.satinalinankitaplar.OrderBy(e => e.UrunID).ToList();

            foreach (KitapUrun e in siraliKitaplar)
            {
                Console.WriteLine($"ID:{e.UrunID}  Ad:{e.Ad}  Miktar:{e.miktar}  Fiyat:{e.Fiyat} ");
            }

            Console.WriteLine("Devam etmek için bir tuşa basınız...");
            Console.ReadLine();
            Console.Clear();
        }
    }
    public override void UrunYorumla(UrunYonetimSistemi ur)
    {
        //Eğer kitap listesinin sayısı 0 ise kitap listesi boş uyarısı.
        if (ur.satinalinankitaplar.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("Satın alınan Kitap listesi boş! Ürün yorumlamak için önce satın almalısınız.");

        }
        else
        {
        YorumID:
            Console.Clear();
            Console.WriteLine("Kitap Listesi");
            // Kitapları ID'ye göre sırala ve öyle yazdır
            var siraliKitaplar = ur.satinalinankitaplar.OrderBy(e => e.UrunID).ToList();

            foreach (KitapUrun e in siraliKitaplar)
            {
                Console.WriteLine($"ID:{e.UrunID}  Ad:{e.Ad}  Stok:{e.Stok}  Fiyat:{e.Fiyat} ");
            }
            Console.WriteLine("Yorum yapmak istediğiniz ürünün ID'sini giriniz");
            int urunid = intkontrol();
            KitapUrun kitap = ur.satinalinankitaplar.FirstOrDefault(u => u.UrunID == urunid) as KitapUrun;
            if (kitap != null)
            {
                // Kullanıcı daha önceden yorum yapıp yapmadığını kontrol ediyoruz
                if (kitap.yorumkk == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Lütfen yorumunuzu giriniz:");
                    kitap.Yorum = Console.ReadLine();
                    Console.WriteLine("Yorumunuz başarıyla eklendi.");
                    kitap.yorumkk = 1;
                    // E-posta gönderimi için gerekli bilgiler
                    string smtpServer = "smtp.gmail.com"; // SMTP sunucu adresi
                    int smtpPort = 587; // SMTP port numarası (genellikle 587 veya 465)
                    string smtpUsername = "kozdemir.usa@gmail.com"; // E-posta adresiniz
                    string smtpPassword = "ucuweptvmkxzgoeh"; // E-posta şifreniz

                    // Gönderilecek e-posta bilgileri
                    string toEmailAddress = "kozdemir.usa@gmail.com";
                    string subject = "Ürün yorumu";
                    string body = $"{kitap.UrunID} ID'li kitaba gelen yorum:{kitap.Yorum} ";

                    // MailMessage sınıfı ile e-posta oluşturma
                    MailMessage mail = new MailMessage(smtpUsername, toEmailAddress, subject, body);

                    // SmtpClient sınıfı ile e-posta gönderimi
                    using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                    {
                        smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                        smtpClient.EnableSsl = true; // SSL kullanılacaksa true, kullanılmayacaksa false

                        try
                        {
                            smtpClient.Send(mail);
                            Console.WriteLine("E-posta başarıyla gönderildi.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("E-posta gönderiminde hata oluştu: " + ex.ToString());
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Bu ürüne zaten yorum yapılmış!");
                }
            }
            else
            {
                Console.WriteLine("Lütfen doğru ID giriniz..");
                Console.WriteLine("Yönlendiriliyorsunuz...");
                System.Threading.Thread.Sleep(1500);
                goto YorumID;
            }
        }
        Console.WriteLine("Devam etmek için bir tuşa basınız..");
        Console.ReadKey();
    }
    public override void UrunYorumlari(UrunYonetimSistemi ur)
    {
        if (ur.Kitaplar.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("Kitaplar listesi boş! ");

        }
        else
        {
            Console.Clear();
            Console.WriteLine("Kitap Listesi ");
            var siraliKitaplar = ur.Kitaplar.OrderBy(e => e.UrunID).ToList();

            foreach (KitapUrun e in siraliKitaplar)
            {
                if (e.yorumkk == 1)
                    Console.WriteLine($"ID:{e.UrunID}  Ad:{e.Ad} Fiyat:{e.Fiyat}     Yapılan Yorum:{e.Yorum}");
                else
                {
                    Console.WriteLine($"ID:{e.UrunID}  Ad:{e.Ad} Fiyat:{e.Fiyat}     Yapılan yorum bulunamadı!");
                }
            }
        }
        Console.WriteLine("Devam etmek için bir tuşa basınız..");
        Console.ReadKey();
    }
}


//Ürünleri yöneteceğimiz classı oluşturuyoruz
public class UrunYonetimSistemi

{
    public int bakiye = 0;
    // Listelerimizi oluşturuyoruz
    private List<Urun> kitaplar = new List<Urun>();
    private List<Urun> elektronikler = new List<Urun>();
    public List<Urun> Elektronikler // private liste erişme işlemi
    {
        get { return elektronikler; }
    }
    public List<Urun> Kitaplar // private liste erişme işlemi
    {
        get { return kitaplar; }
    }
    //bu listeler satın aldığımızda ürünlerin ekleneceği listeler
    public List<Urun> satinalinanelektronikler = new List<Urun>();
    public List<Urun> satinalinankitaplar = new List<Urun>();
    static int intkontrol() // int kontrol metodu oluşturuluyor
    {
        int deger;
        bool isValidInput;

        do
        {
            isValidInput = int.TryParse(Console.ReadLine(), out deger);

            if (!isValidInput)
            {
                Console.WriteLine("Geçersiz giriş! Lütfen bir sayı girin.");
            }

        } while (!isValidInput);

        return deger;
    }

    //Ürün eklerken kullanacağımız metod
    public bool UrunEkle(KitapUrun urun)
    {
        //Aynı ID ile ürün var mı diye kontrol
        if (kitaplar.Any(u => u.UrunID == urun.UrunID) || elektronikler.Any(u => u.UrunID == urun.UrunID))
        {
            return false;
        }
        kitaplar.Add(urun);
        return true;

    }
    //Overload Metod ürün ekleme
    public bool UrunEkle(ElektronikUrun urun)
    {
        //Aynı ID ile ürün var mı diye kontrol
        if (elektronikler.Any(u => u.UrunID == urun.UrunID) || kitaplar.Any(u => u.UrunID == urun.UrunID))
        {
            return false;
        }
        elektronikler.Add(urun);
        return true;

    }




    //Elektronik ürünlerin fiyatlarının güncelleneceği metod

    public void EUrunFiyatGuncelle()
    {
        Console.Clear();
        Console.WriteLine("Fiyat güncellemesi yapılmak istenen ürünün ID'sini giriniz");
        //Buradaki işlem ile girilen değerin sayı olmadığı durumlarda bize uyarı vermesini sağlıyoruz.
        int UrunId = intkontrol();
        // Eğer girilen ID ile eşleşen ürün varsa o ürünün oluşturulması işlemi.
        ElektronikUrun eurun = elektronikler.FirstOrDefault(u => u.UrunID == UrunId) as ElektronikUrun;
        
        
        
        //Eğer girilen ID bir elektronik ürün ise aşşağıdaki işlemler yapılıyor
        if (eurun != null)
        {
            Console.WriteLine($"Güncel Fiyat: {eurun.Fiyat}");
            Console.WriteLine("Yeni fiyat giriniz:  ");

            int yenifiyat = intkontrol();
            //Elektronik ürünün fiyatını güncelleme işlemi
            eurun.Fiyat = yenifiyat;
            Console.WriteLine($"ID:{eurun.UrunID} Marka:{eurun.Marka} ürününün fiyatı güncellendi. Yeni fiyat:{eurun.Fiyat}");
            Console.WriteLine("Lütfen bekleyin yönlendiriliyorsunuz...");
            System.Threading.Thread.Sleep(2500);
        }
        // Eğer ID hiçbir ürünle eşleşmezse kullanıcıya uyarı veriliyor
        else
        {
            Console.WriteLine("Bu ID'ye sahip herhangi bir elektronik ürün bulunamadı!");
            Console.WriteLine("Lütfen bekleyin yönlendiriliyorsunuz...");
            System.Threading.Thread.Sleep(2500);
        }

    }

    // Kitap fiyatlarının  Güncelleneceği Metod
    public void KitapFiyatGuncelle()
    {
        Console.Clear();
        Console.WriteLine("Fiyat güncellemesi yapılmak istenen ürünün ID'sini giriniz");
        //Buradaki işlem ile girilen değerin sayı olmadığı durumlarda bize uyarı vermesini sağlıyoruz.
        int UrunId = intkontrol();
        // Eğer girilen ID ile eşleşen ürün varsa o ürünün oluşturulması işlemi.
        
        KitapUrun kitap = kitaplar.FirstOrDefault(u => u.UrunID == UrunId) as KitapUrun;
        if (kitap != null)
        {
            Console.WriteLine($"Güncel Fiyat: {kitap.Fiyat}");
            Console.Write("Yeni fiyat giriniz: ");

            int yeniFiyat = intkontrol();
            // Kitabın fiyatını güncelle
            kitap.Fiyat = yeniFiyat;

            Console.WriteLine($"Kitap ID:{kitap.UrunID}--{kitap.Ad} kitabının fiyatı güncellendi. Yeni fiyat: {kitap.Fiyat}");
            Console.WriteLine("Lütfen bekleyin yönlendiriliyorsunuz...");
            System.Threading.Thread.Sleep(2500);
        }
        else
        {
            Console.WriteLine("Bu ID'ye sahip herhangi bir kitap bulunamadı!");
            Console.WriteLine("Lütfen bekleyin yönlendiriliyorsunuz...");
            System.Threading.Thread.Sleep(2500);
        }
    }
    public void KitapStokGuncelle()
    {
        Console.Clear();
        Console.WriteLine("Stok güncellemesi yapılmak istenen ürünün ID'sini giriniz");
        int UrunId = intkontrol();      
        
        KitapUrun kitap = kitaplar.FirstOrDefault(u => u.UrunID == UrunId) as KitapUrun;
        
         if (kitap != null)
        {
            Console.WriteLine($"Güncel stok miktarı: {kitap.Stok}");
            Console.Write("Yeni stok miktarını giriniz: ");

            int yeniStok = intkontrol();

            // Kitabın stok miktarını güncelle
            kitap.Stok = yeniStok;

            Console.WriteLine($"Kitap ID:{kitap.UrunID}- {kitap.Ad} kitabının stok miktarı güncellendi. Yeni stok: {kitap.Stok}");
            Console.WriteLine("Lütfen bekleyin yönlendiriliyorsunuz...");
            System.Threading.Thread.Sleep(2500);

            Console.Clear();
        }
        else
        {
            Console.WriteLine("Bu ID'ye sahip herhangi bir kitap bulunamadı!");
            Console.WriteLine("Lütfen bekleyin yönlendiriliyorsunuz...");
            System.Threading.Thread.Sleep(2000);
        }
    }
    public void ElektronikStokGuncelle()
    {
        Console.Clear();
        Console.WriteLine("Stok güncellemesi yapılmak istenen ürünün ID'sini giriniz");
        int UrunId = intkontrol();
        ElektronikUrun elektronik = elektronikler.FirstOrDefault(u => u.UrunID == UrunId) as ElektronikUrun;
        if (elektronik != null)
        {
            Console.WriteLine($"Güncel Stok: {elektronik.Stok}");
            Console.WriteLine("Yeni stok giriniz:  ");
            int yenistokk = intkontrol();
            //Elektronik ürünün stok güncelleme işlemi
            elektronik.Stok = yenistokk;
            Console.WriteLine($"ID:{elektronik.UrunID} Marka:{elektronik.Marka} ürününün stoğu güncellendi. Yeni stok:{elektronik.Stok}");
            Console.WriteLine("Lütfen bekleyin yönlendiriliyorsunuz...");
            System.Threading.Thread.Sleep(2500);
        }
        else
        {
            Console.WriteLine("Bu ID'ye sahip herhangi bir elektronik ürün bulunamadı!");
            Console.WriteLine("Lütfen bekleyin yönlendiriliyorsunuz...");
            System.Threading.Thread.Sleep(2000);
        }
    }



}

internal class Program
{
    static int secimm;

    static void Main(string[] args)
    {
        int secim;
        //Program içinde fonksiyonlara erişim için 3 adet örnek oluşturdum.
        UrunYonetimSistemi urunYonetimSistemi = new UrunYonetimSistemi();
        ElektronikUrun eurun = new ElektronikUrun();
        KitapUrun kitap = new KitapUrun();

        Hoşgeldin();
    İlkmenü:
        Console.Clear();
        Console.WriteLine("Giriş tipini seçiniz");
        Console.WriteLine("1-Yönetici Girişi\n2-Kullanıcı Girişi\n3-Çıkış");
        secimm = intkontrol();
        switch (secimm)
        {
            case 1:
                Console.Clear();
                bool girisbasarili = false; // Giriş sisteminde kullanacağımız bool değişken

                do  //Bu do-While döngüsünü eğer kullanıcı adı şifre hatalı ise tekrar kullanıcı adı ve şifre istemesi için kullanıyoruz.
                {
                    Console.WriteLine("Kullanıcı Adı:");
                    string kullaniciAdi = Console.ReadLine();

                    Console.WriteLine("Şifre:");
                    string sifre = SifreGizleme();

                    //Aşağıda tanımladığımız giriskontrol metodu true ise programımız başlar          
                    if (GirisKontrol(kullaniciAdi, sifre))
                    {
                        //Eğer kullanıcı adı ve şifre doğru ise girisbasarili değişkeni true olacak ve program başlar.
                        girisbasarili = true;
                        //Arayüzün tasarlanması
                        do
                        {
                        Menü1:
                            Console.Clear();
                            Console.WriteLine("YÖNETİCİ ANA MENÜ");
                            Console.WriteLine("1-Ürün Ekle");
                            Console.WriteLine("2-Ürün Güncelle");
                            Console.WriteLine("3-Ürün Listele");
                            Console.WriteLine("4-Ürün Sil");
                            Console.WriteLine("5-Ürün Yorumları");
                            Console.WriteLine("6-Giriş menüsüne dön");
                            Console.WriteLine("7-Çıkış");


                            //Kullanıcıdan alınan değer eğer rakam değilse uyarı verdiren kod (bu kodu projenin bir çok yerinde kullanacağız)
                            secim = intkontrol();


                            switch (secim)
                            {

                                case 1:
                                MenüEkle: // GOTO kullanacığımız için kullanıldı

                                    Console.Clear();
                                    Console.WriteLine("ÜRÜN İŞLEMLERİ");
                                    Console.WriteLine("1-Elektronik Ürün Ekle");
                                    Console.WriteLine("2-Kitap Ekle");
                                    Console.WriteLine("3-Ana Menü");
                                    int secim4 = intkontrol();
                                    switch (secim4)
                                    {
                                        case 1:
                                            Console.Clear();
                                            ElektronikUrunEkle(urunYonetimSistemi);
                                            goto MenüEkle;
                                        case 2:
                                            KitapEkle(urunYonetimSistemi);
                                            goto MenüEkle;
                                        case 3:
                                            goto Menü1;
                                        default:
                                            Console.Clear();
                                            Console.WriteLine("1-3 aralığında bir değer girin");
                                            Console.WriteLine("Yönlendiriliyorsunuz...");
                                            System.Threading.Thread.Sleep(1500);
                                            goto MenüEkle;

                                    }


                                case 2:
                                MenüGüncelle:
                                    Console.Clear();
                                    Console.WriteLine("GÜNCELLEME İŞLEMLERİ");
                                    Console.WriteLine("1-Elektronik Ürün Güncelle\n2-Kitap Güncelle\n3-Ana Menü");
                                    int secim5 = intkontrol();
                                    switch (secim5)
                                    {
                                        case 1:
                                            eurun.UrunGuncelle(urunYonetimSistemi);
                                            goto MenüGüncelle;
                                        case 2:
                                            kitap.UrunGuncelle(urunYonetimSistemi);
                                            goto MenüGüncelle;
                                        case 3:
                                            goto Menü1;
                                        default:
                                            Console.Clear();
                                            Console.WriteLine("1-3 aralığında bir değer girin");
                                            Console.WriteLine("Yönlendiriliyorsunuz...");
                                            System.Threading.Thread.Sleep(1500);
                                            goto MenüGüncelle;
                                    }

                                case 3:
                                MenüListele:
                                    Console.Clear();
                                    Console.WriteLine("LİSTELEME İŞLEMLERİ\n1-Elektronik Ürün Listele\n2-Kitap Listele\n3-Ana Menü");
                                    int secim6 = intkontrol();
                                    switch (secim6)
                                    {
                                        case 1:

                                            eurun.Listele(urunYonetimSistemi);
                                            goto MenüListele;
                                        case 2:

                                            kitap.Listele(urunYonetimSistemi);

                                            goto MenüListele;
                                        case 3:
                                            goto Menü1;
                                        default:
                                            Console.Clear();
                                            Console.WriteLine("1-3 aralığında bir değer girin");
                                            Console.WriteLine("Yönlendiriliyorsunuz...");
                                            System.Threading.Thread.Sleep(1500);
                                            goto MenüListele;
                                    }


                                case 4:
                                MenüSil:
                                    Console.Clear();
                                    Console.WriteLine("SİLME İŞLEMLERİ");
                                    Console.WriteLine("1-Elektronik Ürün Sil\n2-Kitap Sil\n3-Ana Menü");
                                    int secim8 = intkontrol();
                                    switch (secim8)
                                    {
                                        case 1:

                                            eurun.UrunSil(urunYonetimSistemi);

                                            goto MenüSil;


                                        case 2:

                                            kitap.UrunSil(urunYonetimSistemi);

                                            goto MenüSil;
                                        case 3:
                                            goto Menü1;

                                        default:
                                            Console.Clear();
                                            Console.WriteLine("1-3 aralığında bir değer girin");
                                            Console.WriteLine("Yönlendiriliyorsunuz...");
                                            System.Threading.Thread.Sleep(1500);
                                            goto MenüSil;

                                    }


                                case 5:
                                MenüYorum:
                                    Console.Clear();
                                    Console.WriteLine("YORUM GÖRÜNTÜLEME");
                                    Console.WriteLine("1-Elektronik ürün yorumları\n2-Kitap Yorumları\n3-Ana Menü");
                                    int secim22 = intkontrol();
                                    switch (secim22)
                                    {
                                        case 1:
                                            eurun.UrunYorumlari(urunYonetimSistemi);
                                            goto MenüYorum;
                                        case 2:
                                            kitap.UrunYorumlari(urunYonetimSistemi);
                                            goto MenüYorum;
                                        case 3:
                                            goto Menü1;
                                        default:
                                            Console.Clear();
                                            Console.WriteLine("1-3 aralığında bir değer girin");
                                            Console.WriteLine("Yönlendiriliyorsunuz...");
                                            System.Threading.Thread.Sleep(1500);
                                            goto MenüYorum;
                                    }

                                case 6:
                                    goto İlkmenü;
                                case 7:
                                    Console.Clear();
                                    Console.Write("Çıkış yapılıyor");
                                    //Çıkış yapma animasyonu
                                    for (int i = 0; i < 3; i++)
                                    {
                                        Thread.Sleep(500);
                                        Console.Write(".");
                                    }
                                    // Çıkış yapma işlemi
                                    Environment.Exit(0);
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("1-5 aralığında bir değer girin");
                                    Console.WriteLine("Yönlendiriliyorsunuz...");
                                    System.Threading.Thread.Sleep(1500);
                                    goto Menü1;

                            }
                        } while (secim != 7);
                    }
                    else
                    {
                        Console.WriteLine("\nHatalı kullanıcı adı veya şifre!");
                    }
                } while (!girisbasarili);
                break;
            case 2:
            Menü2:
                Console.Clear();
                Console.WriteLine("KULLANICI ANA MENÜ");
                Console.WriteLine("Bakiyeniz=" + urunYonetimSistemi.bakiye);
                Console.WriteLine("1-Bakiye Ekle\n2-Ürün Satın Al\n3-Ürünlerim\n4-Ürün Yorumla\n5-Giriş ekranına dön\n6-Çıkış ");
                int secim12 = intkontrol();
                switch (secim12)
                {
                    case 1:
                        Console.Clear();
                        
                        Console.WriteLine("Eklemek istediğiniz bakiye miktarı:");
                        bool d13 = int.TryParse(Console.ReadLine(), out int eklenecekbakiye);

                        while (!d13)
                        {
                            Console.WriteLine("Geçersiz giriş! Lütfen bir sayı girin:");
                            d13 = int.TryParse(Console.ReadLine(), out eklenecekbakiye);
                        }
                        urunYonetimSistemi.bakiye = urunYonetimSistemi.bakiye + eklenecekbakiye;
                        Console.WriteLine("Bakiye başarıyla eklendi!");
                        Console.WriteLine("Yönlendiriliyorsunuz...");
                        System.Threading.Thread.Sleep(1500);
                        goto Menü2;

                    case 2:
                    MenüSatınAl:
                        Console.Clear();
                        Console.WriteLine("ALIŞVERİŞ MENÜSÜ");
                        Console.WriteLine("1-Elektronik Ürün Satın Al\n2-Kitap Satın al\n3-Ana Menü");
                        int secim9 = intkontrol();
                        switch (secim9)
                        {
                            case 1:
                                eurun.SatınAlma(urunYonetimSistemi);
                                goto MenüSatınAl;
                            case 2:
                                kitap.SatınAlma(urunYonetimSistemi);
                                goto MenüSatınAl;
                            case 3:
                                goto Menü2;
                            default:
                                Console.WriteLine("1-3 aralığında bir değer girin");
                                Console.WriteLine("Yönlendiriliyorsunuz...");
                                System.Threading.Thread.Sleep(1500);
                                goto MenüSatınAl;

                        }

                    case 3:
                    MenüÜrünler:
                        Console.Clear();
                        Console.WriteLine("SATIN ALINAN ÜRÜNLER");
                        Console.WriteLine("1-Elektronik Ürünlerim\n2-Kitaplarım\n3-Ana Menü");
                        int secim15 = intkontrol();
                        switch (secim15)
                        {
                            case 1:
                                eurun.satinalinanlar(urunYonetimSistemi);
                                goto MenüÜrünler;
                            case 2:
                                kitap.satinalinanlar(urunYonetimSistemi);
                                goto MenüÜrünler;
                            case 3:
                                goto Menü2;
                            default:
                                Console.WriteLine("1-3 aralığında bir değer girin");
                                Console.WriteLine("Yönlendiriliyorsunuz...");
                                System.Threading.Thread.Sleep(1500);
                                goto MenüÜrünler;

                        }

                    case 4:
                    MenüYorumla:
                        Console.Clear();
                        Console.WriteLine("YORUM MENÜSÜ");
                        Console.WriteLine("1-Elektronik ürün yorumla\n2-Kitap yorumla\n3-Ana menü");
                        int secim17 = intkontrol();
                        switch (secim17)
                        {
                            case 1:

                                eurun.UrunYorumla(urunYonetimSistemi);
                                goto MenüYorumla;
                            case 2:

                                kitap.UrunYorumla(urunYonetimSistemi);
                                goto MenüYorumla;
                            case 3:
                                goto Menü2;
                            default:
                                Console.Clear();
                                Console.WriteLine("1-3 aralığında bir değer girin");
                                Console.WriteLine("Yönlendiriliyorsunuz...");
                                System.Threading.Thread.Sleep(1500);
                                goto MenüYorumla;
                        }

                    case 5:
                        goto İlkmenü;
                    case 6:
                        System.Environment.Exit(0);
                        break;

                }
                System.Threading.Thread.Sleep(3000);
                break;
            case 3:

                Console.Clear();
                Console.Write("Çıkış yapılıyor");
                //Çıkış yapma animasyonu
                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(500);
                    Console.Write(".");
                }


                // Çıkış yapma işlemi
                Environment.Exit(0);
                break;

            default:
                Console.Clear();
                Console.WriteLine("1-3 aralığında bir değer girin");
                Console.WriteLine("Yönlendiriliyorsunuz...");
                System.Threading.Thread.Sleep(1500);
                goto İlkmenü;

        }







        //Hoşgeldin ekranı tasarımı
        void Hoşgeldin()
        {

            for (int i = 0; i < 280; i++) { Console.Write(" * "); }

            Console.WriteLine("\n\n\n\n\n\n\n\n\t\t\t\t     ÜRÜN YÖNETİM & TAKİP SİSTEMİNE HOŞGELDİNİZ");
            Console.WriteLine("\n\n\n\n\n\n\n");
            for (int i = 0; i < 280; i++) { Console.Write(" * "); }
            System.Threading.Thread.Sleep(1800);
            Console.Clear();
        }

    }




    //Elektronik ürünleri ekleyen metod
    static void ElektronikUrunEkle(UrunYonetimSistemi urunYonetimSistemi)
    {
        ElektronikUrun elektronikurun = new ElektronikUrun();
        UrunBilgileriniGir(elektronikurun);
        ElektronikBilgileriniGir(elektronikurun);
        if (urunYonetimSistemi.UrunEkle(elektronikurun))
        {
            Console.WriteLine("Ürün başarıyla eklendi");
            Console.WriteLine("Lütfen bekleyin yönlendiriliyorsunuz...");

            System.Threading.Thread.Sleep(1000); // konsol hemen clearlanmasın diye bir bekleme komutu
            Console.Clear();

        }
        else
        {
            Console.WriteLine("HATA! Bu ID ile daha önce eklenmiş bir ürün bulunmaktadır. Lütfen farklı bir ID girin");
            System.Threading.Thread.Sleep(1000);


        }
    }
    //Kitapları ekleyen metod
    static void KitapEkle(UrunYonetimSistemi urunYonetimSistemi)
    {
        Console.Clear();
        KitapUrun kitap = new KitapUrun();
        UrunBilgileriniGir(kitap);
        KitapBilgileriniGir(kitap);

        if (urunYonetimSistemi.UrunEkle(kitap))
        {
            Console.WriteLine("Ürün başarıyla eklendi");
            Console.WriteLine("Lütfen bekleyiniz yönlendiriliyorsunuz...");
            System.Threading.Thread.Sleep(1000); // konsol hemen clearlanmasın diye bir bekleme komutu
            Console.Clear();
        }
        else
        {
            Console.WriteLine("HATA! Bu ID ile daha önce eklenmiş bir ürün bulunmaktadır. Lütfen farklı bir ID girin");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
        }
    }

    //Ürün bilgilerini girmek için metod

    static void UrunBilgileriniGir(Urun urun)
    {
        bool gecerliId = false;

        do
        {
            Console.Write("Ürün ID'sini giriniz (sadece rakam):");

            // Kullanıcının girdiği ID'nin geçerli bir tam sayı olup olmadığını kontrol et
            if (int.TryParse(Console.ReadLine(), out int girilenId))
            {
                urun.UrunID = girilenId;
                gecerliId = true;
            }
            else
            {
                Console.WriteLine("Geçersiz giriş! Lütfen sadece rakam giriniz.");
            }

        } while (!gecerliId);

        Console.Write("Ürün Adı:");
        urun.Ad = Console.ReadLine();

        bool gecerliFiyat = false;
        do
        {
            Console.Write("Fiyatı giriniz (sadece rakam):");

            // Kullanıcının girdiği fiyatın geçerli bir ondalık sayı olup olmadığını kontrol et
            if (decimal.TryParse(Console.ReadLine(), out decimal girilenFiyat))
            {
                urun.Fiyat = girilenFiyat;
                gecerliFiyat = true;
            }
            else
            {
                Console.WriteLine("Geçersiz giriş! Lütfen sadece rakam giriniz.");
            }

        } while (!gecerliFiyat);

        bool gecerliStok = false;
        do
        {
            Console.Write("Stok Miktarını giriniz (sadece rakam):");

            // Kullanıcının girdiği stok miktarının geçerli bir tam sayı olup olmadığını kontrol et
            if (int.TryParse(Console.ReadLine(), out int girilenStok))
            {
                urun.Stok = girilenStok;
                gecerliStok = true;
            }
            else
            {
                Console.WriteLine("Geçersiz giriş! Lütfen sadece rakam giriniz.");
            }

        } while (!gecerliStok);
    }

    //Elektronik ürünün bilgilerinin girileceği yer
    static void ElektronikBilgileriniGir(ElektronikUrun urun)
    {
        Console.Write("Marka:");
        urun.Marka = Console.ReadLine();

        bool gecerliGaranti = false;
        do
        {
            Console.Write("Garanti süresini giriniz (sadece rakam):");

            // Kullanıcının girdiği garanti süresinin geçerli bir tam sayı olup olmadığını kontrol et
            if (int.TryParse(Console.ReadLine(), out int girilenGaranti))
            {
                urun.GarantiSuresi = girilenGaranti;
                gecerliGaranti = true;
            }
            else
            {
                Console.WriteLine("Geçersiz giriş! Lütfen sadece rakam giriniz.");
            }

        } while (!gecerliGaranti);
    }
    static void KitapBilgileriniGir(KitapUrun kitap)
    {
        Console.Write("Kitap Yazarı:");
        kitap.Yazar = Console.ReadLine();
        bool gecerliSayfa = false;
        do
        {
            Console.Write("Sayfa sayısını giriniz (sadece rakam):");

            // Kullanıcının girdiği sayfa sayısının geçerli bir tam sayı olup olmadığını kontrol et
            if (int.TryParse(Console.ReadLine(), out int girilensayfa))
            {
                kitap.SayfaSayisi = girilensayfa;
                gecerliSayfa = true;
            }
            else
            {
                Console.WriteLine("Geçersiz giriş! Lütfen sadece rakam giriniz.");
            }

        } while (!gecerliSayfa);
    }
    //girilen kullanıcı adı ve şifre , programda tanımlanan kullanıcı adı ve şifreye eşit mi kontrolü yapan metod
    static bool GirisKontrol(string kullaniciAdi, string sifre)
    {
        string dogruKullaniciAdi = "yusufkaan";
        string dogruSifre = "1234";

        return kullaniciAdi == dogruKullaniciAdi && sifre == dogruSifre;
    }

    // şifre girerken girdiğimizde *** olarak gösterecek metod
    static string SifreGizleme()
    {
        string sifre = "";
        ConsoleKeyInfo key;

        do
        {
            key = Console.ReadKey(true);

            // Eğer kullanıcı bir karakter girdiyse, '*' karakteri yazdır ve şifreye ekle
            if (!char.IsControl(key.KeyChar))
            {
                Console.Write("*");
                sifre += key.KeyChar;
            }
            // Kullanıcı Backspace tuşuna bastıysa, bir önceki karakteri sil
            else if (key.Key == ConsoleKey.Backspace && sifre.Length > 0)
            {
                Console.Write("\b \b");
                sifre = sifre.Substring(0, sifre.Length - 1);
            }

        } while (key.Key != ConsoleKey.Enter);

        return sifre;
    }
    // girilen değerlerin int olup olmadığını kontrol eden metod
    static int intkontrol()
    {
        int secimm;
        bool isValidInput;

        do
        {
            isValidInput = int.TryParse(Console.ReadLine(), out secimm);

            if (!isValidInput)
            {
                Console.WriteLine("Geçersiz giriş! Lütfen bir sayı girin.");
            }

        } while (!isValidInput);

        return secimm;
    }

}