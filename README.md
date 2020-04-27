# SignalR-Mobil-Uygulama-Ile-Web-Giris-Ekrani
Geliştirilen uygulamanın web giriş panelinde üye numarasını giren kullanıcı edindiği kodu mobil uygulamaya girerek web üzerinden oturum açma işlemini gerçekleştirir.
1)LoginOperaitonHub class' ında GirisKayit metodunda ilk ekranda kullanıcıdan alınan üye numarası ile tabloya kullanıcının kayıt işlemi yapılır.
2)LoginRepository class' ında GirisDurum metodunda tabloda kullanıcı durumu ile ilgili değişiklik olup olmadığı dinlenir. Bu metod Home/KullaniciDurum' dan tetiklenir. Home controllerındaki KullaniciDurum metodu Index.cshtml viewindeki kullaniciDurumKontrol function' ı ile tetiklenir.
3)LoginRepository GirisDurum metodunda yapılan değişiklik DependencyOnChange metodu aracılığıyla LoginRepositoryHub class' ındaki SayfaDurumDegistir metodu kullanılır metodtaki updateDurum ile değişiklik istemci tarafına yansıtılır.
4)Yansıtılan bu bildirim.client.updateDurum ile istemci tarafında takip edilir ve kullaniciDegisimKontrol function' ında sunucudan gelen değere göre ekran durumu değişir.(Gelen kod:2 ise doğrulandı; gelen kod:3 ise hata mesjının olduğu uyarı gösterilir.)

NOT: Giriş işlemi yapıldıktan sonra aynı üye numarası ile giriş yapılmış mobil uygulamadaki ekrana SMS ile kullanıcıya gönderilen kod girilir. Giriş butonuna tıklayınca kod doğru ise web uygulama arayüzünde (Gelen kod:2 ise doğrulandı; gelen kod:3 ise hata mesjının olduğu uyarı gösterilir.) mesaj gösterilir.

NOT: Projede SignalR kullanımı öncelikli olduğu için SMS gönderimi yapılmadı. Bununla birlikte SMS gönderimi yapılmadığı için projede React Native ile geliştirilen uygulamanın App.Js dosyası eklendi ve mobil uygulamada oturum kısmı henüz oluşturulmadığından üye id mobil uygulama tarafında statik bir değer olarak gönderildi.

NOT:Models klasöründeki Kullanici classına karşılık gelecenk tablo veritabanında oluşturulur.
