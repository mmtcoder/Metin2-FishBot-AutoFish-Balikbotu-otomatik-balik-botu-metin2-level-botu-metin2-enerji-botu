# Metin2-FishBot-AutoFish-Balikbotu-otomatik-balik-botu-metin2-level-botu-metin2-enerji-botu

## GENEL 

**Sadece 1 oyun hesabını destekler**. Program görüntü ekran kaydı alma mantığıyla yapılmıştır ve alınan görüntü işlenerek gerekli işlemler yapılmıştır. **Yani gerçek kişi balık tutuyormuş gibi program çalışır ve onun haricinde bilgisayarın başka bir program çalıştırması yada program açık iken sizin başka işlem yapmanız mümkün OLMAZ**.

> [!CAUTION]
> Makro Kullanmak **Ban** Sebebidir.Eğitim amaçlı yazılmıştır ve herhangi sorumluluk **Kabul edilmemektedir**


> [!TIP]
> Eğer program işinize yarayıp destekte bulunmak isterseniz gerekli bilgiler **Haberleşme ve Diğer** kısmında görebilirsin :+1:.

> [!TIP]
> Videolu anlatım için https://www.youtube.com/watch?v=MEqj1Hd0eiI  ziyaret edebilirsiniz

> [!WARNING]
> **800x600 pencere modunda oynanmalı** ve **Ekranın yeri asla kıpırdatılmamalı.**
> Windows Güncelleştirmelerinizde mutlaka **.Net Framework** güncelleştirmlerini yapın yada bilgisayarınız **en güncel** halde olsun.


> [!NOTE]
> Eğer karakter birileri ile yazışmış ise **ChatResources/ChatQuestionAnswer/ChatQuestAnswer.txt** ve
> **ChatResources/ChatQuestionAnswer/RecordWhisperChat.txt** dosyasından inceleyebilir
> ve çekilen screen shot dosyalarını **ScreenShot** dosyasından inceleyebilirsin.
> Birde **Ctrl + O** kombinasyonlarını yada **Telegram aktif ettikten** sonra
> **Durdur** mesajı ile programı durdurabilirsin



## PROGRAMIN ÇALIŞTIRILMASI
* Programı zip halden çıkartıp istediğiniz yere kurdukran sonra. Bin/Debug içerisindeki **Metin2AutoFishCSharp.exe dosyasını Yönetici olarak çalıştırın**

## PROGRAMI TEST ETMENİZ İÇİN BİLGİSAYARDA OLMASI GEREKEN İDEAL ÖZELLİKLER

En az 2.80 Ghz işlemci(Altı olsada tutar ama balık tutma sayısı işlemci hızına göre çok değişkenlik gösterir..)
Bilgisayarınızda aktif kullandığınız başka programlar yok ise sadece 2 Ghz Ram yeterli olacaktır.
İşletim sistemi Windows 8, Windows 10 ve Windows 11 DESTEKLER...

# PROGRAMIN YAPABİLDİKLERİ

## FISHING BÖLÜMÜ

### Balık Botunun Yapabildikleri

- Yabbie, Palamut, Altın Sudak ve Kurbağa Tutma ve yakma(kurbağa balığı yakma ekli değil)
- Hepsi tut seçeneği ile hepsini tutar ve envanterdeki hepsini yakma(Envanterdeki bütün objeleri ateşe sürükler sadece balıklar yanar :) 
- Karakter öldüğünde Kanal Değiştirme
- Haritada tespit edilen kişi var ise yavaş tutar
- Balıkçıdan kamp ataşi alıp envanterdeki balıkları yakar
- Solucan alıp slotlara dizme
- Oyundan atıldığı zaman heseba otomatik girme(gameforge simgesi sadece görev çubuğunda olmalı ve sadece balık tutan hesabın açık olması gerekir.)
- Fısıltı ve Chatte yazılan yazı tespit edip ona göre cevap verme
- Zamanlayıcı seçeneği ile belirli dakikalarda balık tutma ara verme ve tamamen durdurma
- Ara verdiğinde rastgele olarak ya karakter atıp bekler yada çıkış atıp bekler

 ### ZAMANLAYICILARIN DEĞER ATANMASI

 - Buradaki değerler **Dakika** cinsindendir.
 - **Min Aktiflik** ve **Max Aktiflik** balık tutma aralıklarıdır
 - **Min-Max Mola** aktiflik süresi bittikten sonra mola verme zamanıdır. Örnek olarak **3 6** gibi aralarında boşluk olacak şekilde iki sayı girmelisiniz ( Bu sayılar birden fazla basamaklı olabilir)
 - Eğer Zamanlayıcıyı Aktif ettiyseniz mutlaka Oyun Durdurma kısmını girmelisiniz. Yani Toplam geçirilecek süreyi ifade eder

   
  ### 'Check Chat' Butonu Hakkında

  - Butona basıldığında "Tespit Edilecek Kelimeler" ve "Edilen Tespite Göre Verilecek Cevaplar" kısmı en altta da kayıtlı olanları gösteren dialog var
  - Ekleme yaparken birden fazla kelime eklerken mutlaka "," (virgül) işareti ile ayırmalısınız
  - İki yeride doldurduktan sonra " verileri yükle " butonuna basınız.
  - Örneğin üst kısma = " **ne yapıyorsun,ne yaptın,ne yaparsın** " verilecek cevap kısmına ise "**iyidir senden ne haber,iyiyim balık tutuyorum,sağol çalışıyorum** " gibi yükleme yapabilirsin.

 ## Level And Farm Bölümü

 ### Level and Farm Kısmının Yapabildikleri

 - Belirlediğiniz oranlara göre hp ve sp potu basma (elbette mutlaka envanterinizde iki türdende pot olması lazım)
 - Karakter öldüğünde çıkış atıp rastgele kanal değiştirme
 - 60 levele kadar hangi statü değeri yüksek ise ona göre statü verir
 - Statu vermeye örnek vermek gerekir ise **HP=4, SP=3, STR =2,DEX =1** ise ilk öncelik hp sonra sp diye gider...
 - Eğer ETP görevini aldıysanız yakınınızda düşen etpyi toplar
 - Beceriler süre geçme mantığıyla çalışır. Yani süresi gelince yanında hedef olsun olmasın atanan tuşa basar
 - Yani o an boşada basmış olabilir bu yüzden pasif beceriler **Örneğin hava kılıcı** gibi beceriler daha işlevli olabilir yada uzaktan vuran beceriler
 - Eğer hızlı erişim tuşlarında pot yok ise envanterdeki tespit edilen potları koyar( XXL potları algılamaz)

   ### ENERJİ KRİSTALİ BÖLÜMÜ

   - Sadece **Kırmızı bayrakta ve hesabın 35 level üstü olup simyacıdan görevi almanız gerekir**
   - Artı olarak silahcıdaki **Yeni bir koku** görevini alın ama yapmayın yani **Marketi aç** kısmı ikinci yerde kalacak
   - eğer Telegram aktif ettiyseniz hesabınızda **yer yok veya para bittiyse size mesaj gönderir ve karakter çalışmaz**
   - Kendisi silah satıcısından alıp simyacıya gider sürükleyip bittikten sonra bu döngü devam eder
   - Herhangi bir takılma falan olduğunda otomatik ch atar
   
## Haberleşme ve Diğer Bölümü

- Eğer Telegram seçeneğini **aktif et** seçeneğine basarsanız mutlaka bağlantı kurmanız **gerekir**
- Telegramdan arama kısmına @metin2gamebot yazıp bota başla veya mesaj gönderin.
- Gönderdikten sonra **Test Et** Butonuna basın.
- Ardından size *** kullanıcı size mi ait diye soracak evete bastıktan sonra bağlantı kurulur.
- **DURDUR** mesajı gönderdiğinizde aktif olan botu durdurur.

# BALIK BOTU NASIL ÇALIŞTIRILMALI

![ı1ı1](https://github.com/user-attachments/assets/98d1c4ed-6728-4761-b873-b8842fe8c994)
-YENİ VERSİYON EKLENDİ
# V2 - EKLENEN ÖZELLİKLER
-Kadife ve denizkızı anahtarı tutma özelliği eklendi.
-Artık kurbağa ve kadifeyi de kızartıyor.
-Ayrıca ''Adapte tutma'' seçeneği ile etrafınızda oyuncu varsa normal hızda tutar(Bu seçeneği aktif etmez iseniz hızlı tutar.)
#**v2.1** 
-Hepsi seçeneği aktif edildiğinde artık ''Oltaya bir şey takıldı'' olduğunda geçiyor. 
-Tıklamalar eskisine göre daha iyi durumda. 
-8.08.2024



* **Ekranın yeri hiç kıpırdamamalı** ve **800x600** pencere modunda oynanmalı
* Balıkçı karakterin hemen **arka kısımında** olmalı. Yani karakter geriye doğru gittiği zaman **mutlaka** karakterin kamera açısından balıkçı **görülmeli**
* Haritalar veya herhangi karakterin chat veya isim kısmını kapatacak şeyler olmamalı
