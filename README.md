# BuildingManagement

Projede hem api hem mvc bulunmaktadır.
İlk açtığımızda DB oluşturmak için seçili proje olarak api katmanı seçilmelidir.
Daha sonra mvc yi görebilmek için multiple chose yapılıp hem mvc hem web katmanı seçilmelidir.
Mvc ye giriş için otomatik oluşturulan user datası admin rolüne sahip olup bilgileri aşağıdadır.
UserName = admin
Password = 123456
Yeni user eklendiğinde senaryoyu ödevi ilk okuduğumda anladığım şekilde yapmıştım. O da şöyle; Admin userları oluşturur ve bu sırada otomatik bir şifre oluşur. Admin kullanıcı adı ve şifreyi site sakinine yüzyüze verir. 
Adminin her şeye erişimi vardır fakat tenant ve owner rolü sadece kendisine gelen aidatları, mesajları görür.
Mesajlarda okundu özelliği vardır detail kısmına girince çalışır.
Aidatlar type ına göre ayda sadece bir kez eklenir. Aynı typeta ikinci bir aidat eklenemez. Typelar elle string bi şekilde giriliyor "Elektrik", "Su", "Aidat", "Doğalgaz" gibi.
Aidatlar listelenirken unpaid diye bir buton var ve sadece ödenmemiş aidatların olduğu sayfayı gösteriyor.
Ödeme sistemi entegre değil fakat aidat listelenen sayfada pay butonuna basılınca onaylıyor musunuz alertinden sonra ispaid true ya çekiliyor. 
